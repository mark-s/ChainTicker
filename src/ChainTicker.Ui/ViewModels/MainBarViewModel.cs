using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.Ui.Models;
using ChainTicker.Ui.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace ChainTicker.Ui.ViewModels
{
    public class MainBarViewModel : BindableBase
    {
        private readonly ICoinInfoService _coinInfoService;
        private readonly ExchangeModelsFactory _exchangeModelsFactory;
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




        public MainBarViewModel(ICoinInfoService coinInfoService, ExchangeModelsFactory exchangeModelsFactory, IMarketSubscriptionService marketSubscriptionService)
        {
            _coinInfoService = coinInfoService;
            _exchangeModelsFactory = exchangeModelsFactory;
            _marketSubscriptionService = marketSubscriptionService;

            InitDataCommand = GetInitDataCommand();
            ClosingCommand = GetClosingCommand();


        }

        private DelegateCommand GetClosingCommand()
        {
            return new DelegateCommand(async () =>
            {

                // Save subscriptions for next time
                await _marketSubscriptionService.SaveSubscribedMarketsAsync();

                // Send Unsubscribe to the server
                foreach (var exchange in AvailableExchanges.Exchanges)
                    foreach (var market in exchange.Markets.Where(m => m.Subscribed))
                        market.Subscribed = false;
            });
        }


        private async Task GetCoinsAsync()
            => await _coinInfoService.GetAvailableCoinsAsync();


        private async Task GetExchangesAsync()
        {
            AvailableExchanges = await _exchangeModelsFactory.GetExchangesAsync();

            // load and apply previous subscribed status
            foreach (var exchange in AvailableExchanges.Exchanges)
                foreach (var market in exchange.Markets)
                    if (await _marketSubscriptionService.WasSubscribedToAsync(exchange.Name, market.DisplayName))
                        market.Subscribed = true;
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
