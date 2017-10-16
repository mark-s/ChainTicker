using System.Diagnostics;

namespace ChanTicker.Core.Domain
{
    [DebuggerDisplay("Name: {" + nameof(DisplayName) + "}")]
    public class Market 
    {
        public string Id { get; }
        public string BaseCurrency { get; }
        public string CounterCurrency { get; }
        public string DisplayName { get;  }
        public decimal MidMarketPriceSnapshot { get; }

        public bool HasLivePricesAvailable { get; }

        public Market(string id, string baseCurrency, string counterCurrency, string displayName, decimal midMarketPriceSnapshot, bool hasLivePricesAvailable)
        {
            Id = id;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            DisplayName = displayName;
            MidMarketPriceSnapshot = midMarketPriceSnapshot;
            HasLivePricesAvailable = hasLivePricesAvailable;
        }
    }
}