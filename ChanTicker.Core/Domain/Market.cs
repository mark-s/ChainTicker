using System;
using System.Diagnostics;

namespace ChanTicker.Core.Domain
{
    [DebuggerDisplay("Name: {" + nameof(DisplayName) + "}")]
    public class Market 
    {
        public string ProductCode { get; }
        public string BaseCurrency { get; }
        public string CounterCurrency { get; }
        public string DisplayName { get;  }
        public decimal MidMarketPriceSnapshot { get; private set; }

        public bool HasRealTimeUpdates { get; }

        public Market(string productCode, string baseCurrency, string counterCurrency, string displayName, decimal midMarketPriceSnapshot, bool hasRealTimeUpdates)
        {
            ProductCode = productCode;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            DisplayName = displayName;
            MidMarketPriceSnapshot = midMarketPriceSnapshot;
            HasRealTimeUpdates = hasRealTimeUpdates;
        }

        public void ClearMidMarketPriceSnapshot() 
            => MidMarketPriceSnapshot = decimal.Zero;
    }
}