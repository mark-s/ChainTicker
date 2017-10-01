using System.Diagnostics;

namespace ChanTicker.Core.Domain
{
    [DebuggerDisplay("{" + nameof(Id) + "}")]
    public class Market 
    {
        public string Id { get; }
        public string BaseCurrency { get; }
        public string CounterCurrency { get; }

        public Market(string id, string baseCurrency, string counterCurrency)
        {
            Id = id;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
        }
    }
}