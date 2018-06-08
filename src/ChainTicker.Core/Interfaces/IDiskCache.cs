using System;
using ChainTicker.Core.IO;

namespace ChainTicker.Core.Interfaces
{
    public interface IDiskCache
    {
        bool IsStale(ChainTickerFolder cache, string cacheFileName, TimeSpan cacheAgeTimeSpan);
    }
}