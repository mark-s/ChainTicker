using System;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.IO
{
    public class DiskCache : IDiskCache
    {
        private readonly IFileIOService _fileIOService;

        public DiskCache(IFileIOService fileIOService)
        {
            _fileIOService = fileIOService;
        }

        public bool IsStale(ChainTickerFolder folder, string cacheFileName, TimeSpan cacheAgeTimeSpan)
        {
            // check there's actually some saved data to load from 
            if (_fileIOService.FileExists(folder, cacheFileName) == false)
                return true;

            var saveTime = _fileIOService.GetFileSaveTime(folder, cacheFileName);

            var difference = saveTime.CompareTo(DateTime.Now.Subtract(cacheAgeTimeSpan));

            return difference < 0;

        }

    }
}