using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    [DebuggerDisplay("Name: {" + nameof(DisplayName) + "}")]
    public class Market 
    {
        private  IPriceDataService _priceDataService;

        public string ProductCode { get; }

        public string BaseCurrency { get; }

        public string CounterCurrency { get; }

        public string DisplayName { get;  }

        public decimal MidMarketPriceSnapshot { get; private set; }

        public bool HasRealTimeUpdates { get; }

        public Market(string productCode, 
                                string baseCurrency,
                                string counterCurrency, 
                                string displayName, 
                                decimal midMarketPriceSnapshot,
                                bool hasRealTimeUpdates)
        {
            ProductCode = productCode;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            DisplayName = displayName;
            MidMarketPriceSnapshot = midMarketPriceSnapshot;
            HasRealTimeUpdates = hasRealTimeUpdates;
        }


        public void SetPriceDataService(IPriceDataService priceDataService)
            => _priceDataService = priceDataService;

        public void ClearMidMarketPriceSnapshot() 
            => MidMarketPriceSnapshot = decimal.Zero;

        public Task<ITick> GetCurrentPriceAsync()
            => _priceDataService.GetCurrentPriceAsync(this);

        public bool IsSubscribedToTicks()
            => _priceDataService.IsSubscribedToTicks(this);

        public IObservable<ITick> SubscribeToTicks()
            => _priceDataService.SubscribeToTicks(this);

        public void UnsubscribeFromTicks()
            => _priceDataService.UnsubscribeFromTicks(this);

    }
}