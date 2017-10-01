namespace ChanTicker.Core.Domain
{
    public class MarketConfig 
    {
        public string ProductCode { get; }

        public string BaseCurrency { get;  }

        public string CounterCurrency { get; }

        public MarketConfig(string productCode, string baseCurrency, string counterCurrency)
        {
            ProductCode = productCode;
            BaseCurrency = baseCurrency;
            CounterCurrency = counterCurrency;
        }

    }
}