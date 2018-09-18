using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.IO
{
    public class ChainTickerFileService : IChainTickerFileService
    {
        private readonly IDiskCache _cache;
        private readonly IDiskIOService _diskIOService;
        private readonly IJsonSerializer _serializer;

        public ChainTickerFileService(IDiskCache cache, IDiskIOService diskIOService, IJsonSerializer serializer)
        {
            _cache = cache;
            _diskIOService = diskIOService;
            _serializer = serializer;
        }

        public bool IsCacheStale(CachedFile cachedFile)
            => _cache.IsStale(ChainTickerFolder.Cache, cachedFile.FileName, cachedFile.CacheAge);


        public async Task<T> LoadAndDeserializeAsync<T>(ChainTickerFolder folder, string fileName)
        {
            var cachedRaw = await _diskIOService.LoadTextAsync(folder, fileName).ConfigureAwait(false);
            return _serializer.Deserialize<T>(cachedRaw);
        }

        public async Task SaveAndSerializeAsync<T>(ChainTickerFolder folder, string fileName, T data)
        {
            var serialized = _serializer.Serialize(data);
            await _diskIOService.SaveTextAsync(folder, fileName, serialized).ConfigureAwait(false);
        }


    }
}
