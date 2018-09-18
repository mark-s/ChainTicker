using ChainTicker.Core.IO;

namespace ChainTicker.Core.EventTypes
{
    public class MarketSubscribed
    {
        public MarketInfo MarketInfo { get; }

        public MarketSubscribed(MarketInfo marketInfo)
        {
            MarketInfo = marketInfo;
        }
    }
}
