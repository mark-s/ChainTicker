using System;

namespace ChanTicker.Core.IO
{
    public interface IDiskCache
    {
        bool IsStale(ChainTickerFolder cache, string cacheFileName, TimeSpan cacheAgeTimeSpan);
    }
}