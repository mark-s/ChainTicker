using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.Shell.Models;
using ChainTicker.Shell.Services;
using Prism.Commands;
using Prism.Mvvm;


namespace ChainTicker.Shell.ViewModels
{
    public class MainBarViewModel : BindableBase
    {
        private readonly ICoinInfoService _coinInfoService;
        private readonly ExchangeModelsService _exchangeModelsService;
        private readonly IMarketSubscriptionService _marketSubscriptionService;

        public DelegateCommand InitDataCommand { get; }
        public DelegateCommand ClosingCommand { get; }


        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }



        private ExchangeCollectionModel _availableExchanges;
        public ExchangeCollectionModel AvailableExchanges
        {
            get => _availableExchanges;
            set => SetProperty(ref _availableExchanges, value);
        }




        public MainBarViewModel(ICoinInfoService coinInfoService, ExchangeModelsService exchangeModelsService, IMarketSubscriptionService marketSubscriptionService)
        {
            _coinInfoService = coinInfoService;
            _exchangeModelsService = exchangeModelsService;
            _marketSubscriptionService = marketSubscriptionService;

            InitDataCommand = GetInitDataCommand();
            ClosingCommand = GetClosingCommand();


        }

        private DelegateCommand GetClosingCommand()
        {
            return new DelegateCommand(async () =>
            {

                // Save subscriptions for next time
                await _marketSubscriptionService.SaveSubscribedMarketsAsync(AvailableExchanges);

                // Send Unsubscribe to the server
                foreach (var exchange in AvailableExchanges.Exchanges)
                    foreach (var market in exchange.Markets.Where(m => m.IsSubscribed))
                        market.IsSubscribed = false;
            });
        }


        private async Task GetCoinsAsync()
            => await _coinInfoService.GetAvailableCoinsAsync();


        private async Task GetExchangesAsync()
        {
            var exchanges = await _exchangeModelsService.GetExchangesAsync();

            AvailableExchanges = new ExchangeCollectionModel("AvailableExchanges", new ObservableCollection<ExchangeModel>(exchanges));

            foreach (var exchange in AvailableExchanges.Exchanges)
            {
                foreach (var market in exchange.Markets)
                {
                    if (await _marketSubscriptionService.WasSubscribedToAsync(exchange.Name, market.DisplayName))
                        market.IsSubscribed = true;
                }
            }

        }


        private DelegateCommand GetInitDataCommand()
        {
            return new DelegateCommand(async () =>
                                    {
                                        IsLoading = true;

                                        try
                                        {
                                            await GetCoinsAsync();

                                            await GetExchangesAsync();
                                        }
                                        catch (System.Exception ex)
                                        {
                                            Debug.WriteLine(ex.Message);
                                        }
                                        finally
                                        {
                                            IsLoading = false;
                                        }
                                    });
        }

    }



}
