using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;

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


        public ObservableCollection<MarketModel> Markets { get; } = new ObservableCollection<MarketModel>();

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
                                                                            _exchange.UnsubscribeFromTicks,
                                                                            _exchange.Info.Name));
            }

            Markets.AddRange(displayMarkets);
        }


    }
}
