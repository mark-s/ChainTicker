using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Shell.Models
{
    public class ExchangeModel : INotifyPropertyChanged
    {
        private readonly IExchange _exchange;
        private readonly Func<string, ICoin> _coinInfoFunc;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name => _exchange.Info.Name;

        public string Description => _exchange.Info.Description;

        public string HomePage => _exchange.Info.HomePageUrl;


        public ObservableCollection<MarketModel> Markets { get; private set; }

        public ExchangeModel(IExchange exchange, Func<string, ICoin> coinInfoFunc)
        {
            _exchange = exchange;
            _coinInfoFunc = coinInfoFunc;
        }

        public async Task GetAvailableMarketsAsync()
        {

            var displayMarkets = new List<MarketModel>();

            foreach (var market in await _exchange.GetAvailableMarketsAsync())
            {
                displayMarkets.Add(new MarketModel(market,
                                                       _coinInfoFunc(market.BaseCurrency),
                                                       _coinInfoFunc(market.CounterCurrency),
                                                       _exchange.SubscribeToTicks,
                                                       _exchange.UnsubscribeFromTicks));
            }

            Markets = new ObservableCollection<MarketModel>(displayMarkets);

        }


    }
}
