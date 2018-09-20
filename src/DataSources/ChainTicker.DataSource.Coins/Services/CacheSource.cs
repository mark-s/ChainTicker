using System;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Services
{
    public class CacheSource : ICacheSource
    {
        private readonly IChainTickerFileService _fileService;
        private readonly CachedFile _cacheFile = new CachedFile("coins.json", TimeSpan.FromDays(5));

        public CacheSource(IChainTickerFileService fileService)
        {
            _fileService = fileService;
        }

        public bool IsCacheStale() =>
            _fileService.IsCacheStale(_cacheFile);

        public async Task GetFromCacheAsync(Action<AllCoinsResponse> onSuccess, Action<string> onFailure)
        {
            try
            {
                var cachedAllCoinsResponse = await _fileService.LoadAndDeserializeAsync<AllCoinsResponse>(AppFolder.Cache, _cacheFile.FileName);
                onSuccess(cachedAllCoinsResponse);
            }
            catch (Exception ex)
            {
                onFailure(ex.Message);
            }
        }

        public async Task UpdateCachedDataAsync(AllCoinsResponse response)
        {
            // Save to the cache so we don't need to do this expensive call all the time
            await _fileService.SaveAndSerializeAsync(AppFolder.Cache, _cacheFile.FileName, response).ConfigureAwait(false);
        }

    }
}