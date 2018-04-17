using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

using Prism.Mvvm;

namespace ChainTicker.Shell.Models
{
    public class ExchangeModel : BindableBase
    {

        private readonly IExchange _exchange;
        private readonly Func<string, ICoin> _getCoinInfoFunc;

        public string Name => _exchange.Info.Name;

        public string Description => _exchange.Info.Description;

        public string HomePage => _exchange.Info.HomePageUrl;


        public ObservableCollection<MarketModel> Markets { get; } = new ObservableCollection<MarketModel>();

        public ExchangeModel(IExchange exchange, Func<string, ICoin> getCoinInfoFunc)
        {
            _exchange = exchange;
            _getCoinInfoFunc = getCoinInfoFunc;

        }

        public async Task GetAvailableMarketsAsync()
        {

            var displayMarkets = new List<MarketModel>();

            foreach (var market in await _exchange.GetAvailableMarketsAsync())
            {
                displayMarkets.Add(new MarketModel(market,
                                                                            _getCoinInfoFunc(market.BaseCurrency),
                                                                            _getCoinInfoFunc(market.CounterCurrency),
                                                                            _exchange.SubscribeToTicks,
                                                                            _exchange.UnsubscribeFromTicks,
                                                                            _exchange.Info.Name));
            }

            Markets.AddRange(displayMarkets);
        }


    }
}
