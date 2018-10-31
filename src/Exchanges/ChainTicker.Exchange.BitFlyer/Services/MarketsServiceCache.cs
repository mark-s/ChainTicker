using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public class MarketsServiceCache : IMarketsServiceCache
    {
        private readonly IChainTickerFileService _fileService;
        private readonly CachedFile _cachedFile;

        public MarketsServiceCache(IChainTickerFileService fileService, CachedFile cachedFile)
        {
            _fileService = fileService;
            _cachedFile = cachedFile;
        }

        public bool IsCacheStale()
            => _fileService.IsCacheStale(_cachedFile);

        public Task WriteCacheAsync(MarketCollection marketCollection)
            => _fileService.SaveAndSerializeAsync(AppFolder.Cache, _cachedFile.FileName, marketCollection);

        public Task<MarketCollection> ReadCacheAsync()
            => _fileService.LoadAndDeserializeAsync<MarketCollection>(AppFolder.Cache, _cachedFile.FileName);
    }
}
