using ChainTicker.Core.IO;

namespace ChanTicker.Core.EventTypes
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
