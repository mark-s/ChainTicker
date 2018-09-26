namespace ChainTicker.Core.Domain
{
    public interface IMarket
    {
        string BaseCurrency { get; }

        string CounterCurrency { get; }

        string DisplayName { get; }

        bool HasRealTimeUpdates { get; }

        string ProductCode { get; }


    }
}