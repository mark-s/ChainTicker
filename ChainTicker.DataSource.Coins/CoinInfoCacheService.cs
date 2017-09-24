using System;
using System.Threading.Tasks;
using ChanTicker.Core.IO;

namespace ChainTicker.DataSource.Coins
{
    public class CoinInfoCacheService : ICoinInfoCacheService
    {
        private readonly IFileIOService _fileIOService;

        public CoinInfoCacheService(IFileIOService fileIOService)
        {
            _fileIOService = fileIOService;
        }


        public bool IsStale(CoinInfoServiceConfig config)
        {
            // check there's actually some saved data to load from 
            if (_fileIOService.FileExists(config.CacheFileName) == false)
                return true;

            var maxAge = DateTime.Today.AddDays(-config.MaxCacheAgeDays);
            var cacheSavedDate = _fileIOService.GetFileSaveDate(config.CacheFileName);

            return DateTime.Compare(cacheSavedDate, maxAge) < 0;
        }

        public async Task<T> LoadAsync<T>(string fileName)
        {
            if (_fileIOService.FileExists(fileName))
                return await _fileIOService.LoadAsync<T>(fileName);
            else
                return default(T);
        }

        public async Task SaveAsync<T>(string fileName, T data)
            => await _fileIOService.SaveAsync(fileName, data);

    }
}
