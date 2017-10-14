using System;
using ChanTicker.Core.IO;

namespace ChanTicker.Core.Interfaces
{
    public interface IDiskCache
    {
        bool IsStale(ChainTickerFolder cache, string cacheFileName, TimeSpan cacheAgeTimeSpan);
    }
}