using System;

namespace ChainTicker.Core.IO
{
    public class CachedFile
    {
        public string FileName { get;  }

        public TimeSpan CacheAge { get; }

        public CachedFile(string fileName, TimeSpan cacheAge)
        {
            FileName = fileName;
            CacheAge = cacheAge;
        }
    }
}