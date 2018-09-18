using System;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.IO
{
    public class DiskCache : IDiskCache
    {
        private readonly IDiskIOService _diskIOService;
        private readonly ITimeService _timeService;

        public DiskCache(IDiskIOService diskIOService, ITimeService timeService)
        {
            _diskIOService = diskIOService;
            _timeService = timeService;
        }

        public bool IsStale(ChainTickerFolder folder, string cacheFileName, TimeSpan cacheAgeTimeSpan)
        {
            // check there's actually some saved data to load from 
            if (_diskIOService.FileExists(folder, cacheFileName) == false)
                return true;

            var saveTime = _diskIOService.GetFileSaveTime(folder, cacheFileName);

            var difference = saveTime.CompareTo(_timeService.Now.Subtract(cacheAgeTimeSpan));

            return difference < 0;

        }

    }


}