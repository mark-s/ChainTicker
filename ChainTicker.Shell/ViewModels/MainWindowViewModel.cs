using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Shell.Models;
using ChanTicker.Core.Interfaces;
using Prism.Mvvm;

namespace ChainTicker.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ICoinInfoService _coinInfoService;
        private readonly IFiatCurrenciesService _fiatCurrenciesService;
        private readonly BitFlyerExchange _bitFlyerExchange;


        public ObservableCollection<ExchangeModel> Exchanges { get; set; } = new ObservableCollection<ExchangeModel>();


        public MainWindowViewModel(ICoinInfoService coinInfoService,
                                                IFiatCurrenciesService fiatCurrenciesService, 
                                                BitFlyerExchange bitFlyerExchange)
        {
            _coinInfoService = coinInfoService;
            _fiatCurrenciesService = fiatCurrenciesService;
            _bitFlyerExchange = bitFlyerExchange;

            Exchanges.Add( new ExchangeModel(_bitFlyerExchange, CoinInfoFunc));
            Exchanges.Add( new ExchangeModel(_bitFlyerExchange, CoinInfoFunc));
            
            InitAsync();
        }


        private async Task InitAsync()
        {
            await _coinInfoService.PopulateAvailableCoinsAsync();
        }



        private ICoin CoinInfoFunc(string coinOrCurrencyCode)
        {

            var coin = _coinInfoService.GetCoinInfo(coinOrCurrencyCode);
            if (coin.IsValid)
                return coin;
            else
                return _fiatCurrenciesService.GetCurrencyInfo(coinOrCurrencyCode);
        }



        //private void btnSubscribe_Click(object sender, RoutedEventArgs e)
        //{
        //    //var market = listBox.SelectedItems[0] as MarketModel;

        //    //market?.Subscribe();


        //}

        //private void button1_Copy_Click(object sender, RoutedEventArgs e)
        //{
        //    //var market = listBox.SelectedItems[0] as Market;
        //    //_bitFlyer.UnsubscribeFromTicks(market);
        //}

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    _bitFlyer.Dispose();
        //}



    }
}
