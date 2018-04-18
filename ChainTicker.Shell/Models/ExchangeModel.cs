﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChainTicker.Core.Interfaces;

using Prism.Mvvm;

namespace ChainTicker.Shell.Models
{
    public class ExchangeModel : BindableBase
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

            var displayMarkets = new List<MarketModel>();

            foreach (var market in _exchange.Markets)
            {
                displayMarkets.Add(new MarketModel(market,
                                                                            getCoinInfoFunc(market.BaseCurrency),
                                                                            getCoinInfoFunc(market.CounterCurrency),
                                                                            _exchange.Info.Name));
            }

            Markets.AddRange(displayMarkets);
        }


    }
}
