using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace ChainTicker.Shell.Models
{
    public class ExchangeModel : BindableBase
    {
        private readonly IExchange _exchange;
        private readonly Func<string, ICoin> _coinInfoFunc;

        public string Name => _exchange.Info.Name;

        public string Description => _exchange.Info.Description;

        public string HomePage => _exchange.Info.HomePageUrl;

        public DelegateCommand GetMarketsCommand { get; }

        private ObservableCollection<MarketModel> _markets;
        public ObservableCollection<MarketModel> Markets
        {
            get => _markets;
            private set => SetProperty(ref _markets, value);
        }




        public ExchangeModel(IExchange exchange, Func<string, ICoin> coinInfoFunc)
        {
            _exchange = exchange;
            _coinInfoFunc = coinInfoFunc;

            GetMarketsCommand = new DelegateCommand(async () => await GetAvailableMarketsAsync(), () => _exchange.Info.IsEnabled);
        }

        public async Task GetAvailableMarketsAsync()
        {

            var displayMarkets = new List<MarketModel>();

            var markets = await _exchange.GetAvailableMarketsAsync();
            foreach (var market in markets)
            {
                displayMarkets.Add(new MarketModel(market,
                                                       _coinInfoFunc(market.BaseCurrency),
                                                       _coinInfoFunc(market.CounterCurrency),
                                                       _exchange.SubscribeToTicks,
                                                       _exchange.UnsubscribeFromTicks));
            }

            Markets =  new ObservableCollection<MarketModel>(displayMarkets);
        }

        
    }
}
