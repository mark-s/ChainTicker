using ChainTicker.Core.IO;

namespace ChainTicker.Core.EventTypes
{
    public class MarketUnsubscribed
    {
        public MarketInfo MarketInfo { get; }

        public MarketUnsubscribed(MarketInfo marketInfo)
        {
            MarketInfo = marketInfo;
        }

    }
}