using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ChainTicker.DataSource.Coins;
using ChainTicker.DataSource.FiatCurrencies;
using ChainTicker.Exchange.BitFlyer;
using ChainTicker.Transport.Rest;
using ChainTicker.UI.Shell.Models;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.UI.Shell.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly BitFlyerExchange _bitFlyer;
        private readonly CoinInfoService _coinInfoService;
        private readonly FiatCurrenciesService _fiatInfoService;

        public ExchangeModel TheExchange { get; }



        public MainWindowViewModel()
        {


            var restService = new RestService();
            var folderService = new FolderService();
            var fileIoService = new FileIOService(folderService);
            var diskCache = new DiskCache(fileIoService);
            var fileService = new ChainTickerFileService(diskCache, fileIoService, new ChainTickerJsonSerializer());
            _coinInfoService = new CoinInfoService(restService, fileService);
            _fiatInfoService = new FiatCurrenciesService();

            _bitFlyer = new BitFlyerExchange(restService, fileService);



            TheExchange = new ExchangeModel(_bitFlyer, CoinInfoFunc);
        }

        private ICoin CoinInfoFunc(string coinOrCurrencyCode)
        {

            var coin = _coinInfoService.GetCoinInfo(coinOrCurrencyCode);
            if (coin.IsValid)
                return coin;
            else
                return _fiatInfoService.GetCurrencyInfo(coinOrCurrencyCode);
        }


        private async void btnGetMarkets_Click(object sender, RoutedEventArgs e)
        {
            await _coinInfoService.PopulateAvailableCoinsAsync();

            await TheExchange.GetAvailableMarketsAsync();
        }



        private void btnSubscribe_Click(object sender, RoutedEventArgs e)
        {
            //var market = listBox.SelectedItems[0] as MarketModel;

            //market?.StartSubscribe();


        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            //var market = listBox.SelectedItems[0] as Market;
            //_bitFlyer.UnsubscribeFromTicks(market);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _bitFlyer.Dispose();
        }

    }
}
