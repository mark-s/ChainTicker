namespace ChainTicker.MarketEnvironment
{
    public interface IMarketConfig
    {
        string BaseCurrency { get; }

        string CounterCurrency { get; }

        string ProductCode { get; }
    }
}