using System;

namespace ChanTicker.Core.IO
{
    public interface IDiskCache
    {
        bool IsStale(string cacheFileName, TimeSpan cacheAgeTimeSpan);

    }
}