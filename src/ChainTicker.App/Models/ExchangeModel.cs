using System;
using System.Collections.ObjectModel;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.App.Models
{
    public class ExchangeModel
    {

        private readonly IExchange _exchange;

        public string Name => _exchange.Info.Name;

        public string Description => _exchange.Info.Description;

        public string HomePage => _exchange.Info.HomePageUrl;


        public ObservableCollection<MarketModel> Markets { get; } = new ObservableCollection<MarketModel>();


        public ExchangeModel(IExchange exchange, Func<string, ICoin> getCoinInfoFunc)
        {
            _exchange = exchange;

            Populate(getCoinInfoFunc);
        }

        private void Populate(Func<string, ICoin> getCoinInfoFunc)
        {
            foreach (var market in _exchange.Markets)
                Markets.Add(new MarketModel(market,
                                                                getCoinInfoFunc(market.BaseCurrency),
                                                                getCoinInfoFunc(market.CounterCurrency),
                                                                _exchange.Info.Name));
        }


    }
}
