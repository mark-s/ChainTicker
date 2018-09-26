using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Exchange.Gdax
{
    [DebuggerDisplay("Name: {" + nameof(DisplayName) + "}")]
    public class Market : IMarket
    {
        private readonly IPriceTicker _priceTicker;

        public string ProductCode { get; }

        public string BaseCurrency { get; }

        public string CounterCurrency { get; }

        public string DisplayName { get; }

        public bool HasRealTimeUpdates { get; }

        private Market(string productCode,
            string baseCurrency,
            string counterCurrency,
            string displayName,
            bool hasRealTimeUpdates)
        {
            ProductCode = productCode;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            DisplayName = displayName;
            HasRealTimeUpdates = hasRealTimeUpdates;
        }

        internal Market(IMarket market, IPriceTicker priceTicker)
            : this(market.ProductCode,
                market.BaseCurrency,
                market.CounterCurrency,
                market.DisplayName,
                market.HasRealTimeUpdates)
        {
            _priceTicker = priceTicker;
        }

        internal Market(string productCode,
            string baseCurrency,
            string counterCurrency,
            string displayName,
            bool hasRealTimeUpdates,
            IPriceTicker priceTicker)
        {
            _priceTicker = priceTicker;
            ProductCode = productCode;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            DisplayName = displayName;
            HasRealTimeUpdates = hasRealTimeUpdates;
        }


        public Task<ITick> GetCurrentPriceAsync()
            => _priceTicker.GetCurrentPriceAsync(this);

        public bool IsSubscribedToTicks()
            => _priceTicker.IsSubscribedToTicks(this);

        public IObservable<ITick> SubscribeToTicks()
            => _priceTicker.SubscribeToTicks(this);

        public void UnsubscribeFromTicks()
            => _priceTicker.UnsubscribeFromTicks(this);


    }
}