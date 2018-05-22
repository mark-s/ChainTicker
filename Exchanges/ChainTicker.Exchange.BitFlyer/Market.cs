using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer
{
    [DebuggerDisplay("Name: {" + nameof(DisplayName) + "}")]
    public class Market : IMarket
    {
        private readonly IPriceService _priceService;

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

        internal Market(IMarket market, IPriceService priceService)
            : this(market.ProductCode, 
                     market.BaseCurrency,
                     market.CounterCurrency,
                     market.DisplayName,
                     market.HasRealTimeUpdates)
        {
            _priceService = priceService;
        }

        internal Market(string productCode,
                                string baseCurrency,
                                string counterCurrency,
                                string displayName,
                                bool hasRealTimeUpdates,
                                IPriceService priceService)
        {
            _priceService = priceService;
            ProductCode = productCode;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            DisplayName = displayName;
            HasRealTimeUpdates = hasRealTimeUpdates;
        }


        public Task<ITick> GetCurrentPriceAsync()
            => _priceService.GetCurrentPriceAsync(this);

        public bool IsSubscribedToTicks()
            => _priceService.IsSubscribedToTicks(this);

        public IObservable<ITick> SubscribeToTicks()
            => _priceService.SubscribeToTicks(this);

        public void UnsubscribeFromTicks()
            => _priceService.UnsubscribeFromTicks(this);


    }
}