using System.Diagnostics;
using ChainTicker.Core.Domain;

namespace ChainTicker.Exchange.BitFlyer
{
    [DebuggerDisplay("Name: {" + nameof(DisplayName) + "}")]
    public class Market : IMarket
    {
        public string ProductCode { get; }

        public string BaseCurrency { get; }

        public string CounterCurrency { get; }

        public string DisplayName { get; }

        public bool HasRealTimeUpdates { get; }

        internal Market(string productCode,
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



    }
}