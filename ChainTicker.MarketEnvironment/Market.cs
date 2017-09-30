namespace ChainTicker.MarketEnvironment
{

    public interface IMarket
    {
        IMarketConfig MarketConfig { get; }

        IMarketSubscription Subscribe();

        void UnSubscribe();
    }


    




}
