using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.App.Models;
using ChainTicker.App.Services;
using ChainTicker.DataSource.Coins;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ChainTicker.App.ViewModel
{
    public class MainBarViewModel : ViewModelBase
    {
        private readonly ICoinInfoService _coinInfoService;
        private readonly ExchangeModelsFactory _exchangeModelsFactory;
        private readonly IMarketSubscriptionService _marketSubscriptionService;

        public RelayCommand InitDataCommand { get; }
        public RelayCommand ClosingCommand { get; }


        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }



        private ExchangeCollectionModel _availableExchanges;
        public ExchangeCollectionModel AvailableExchanges
        {
            get => _availableExchanges;
            set => Set(ref _availableExchanges, value);
        }




        public MainBarViewModel(ICoinInfoService coinInfoService, ExchangeModelsFactory exchangeModelsFactory, IMarketSubscriptionService marketSubscriptionService)
        {
            _coinInfoService = coinInfoService;
            _exchangeModelsFactory = exchangeModelsFactory;
            _marketSubscriptionService = marketSubscriptionService;

            InitDataCommand = GetInitDataCommand();
            ClosingCommand = GetClosingCommand();


        }

        private RelayCommand GetClosingCommand()
        {
            return new RelayCommand(async () =>
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


        private RelayCommand GetInitDataCommand()
        {
            return new RelayCommand(async () =>
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
