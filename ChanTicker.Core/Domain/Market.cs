using System.Diagnostics;

namespace ChanTicker.Core.Domain
{
    [DebuggerDisplay("{" + nameof(DisplayName) + "}")]
    public class Market 
    {
        public string Id { get; }
        public string BaseCurrency { get; }
        public string CounterCurrency { get; }
        public string DisplayName { get;  }

        public Market(string id, string baseCurrency, string counterCurrency, string displayName)
        {
            Id = id;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
            DisplayName = displayName;
        }
    }
}