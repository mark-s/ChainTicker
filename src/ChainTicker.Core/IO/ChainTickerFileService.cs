using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.IO
{
    public class ChainTickerFileService : IChainTickerFileService
    {
        private readonly IDiskCache _cache;
        private readonly IFileIOService _fileIOService;
        private readonly IJsonSerializer _serializer;

        public ChainTickerFileService(IDiskCache cache, IFileIOService fileIOService, IJsonSerializer serializer)
        {
            _cache = cache;
            _fileIOService = fileIOService;
            _serializer = serializer;
        }

        public bool IsCacheStale(CachedFile cachedFile)
            => _cache.IsStale(ChainTickerFolder.Cache, cachedFile.FileName, cachedFile.CacheAge);


        public async Task<T> LoadAndDeserializeAsync<T>(ChainTickerFolder folder, string fileName)
        {
            var cachedRaw = await _fileIOService.LoadTextAsync(folder, fileName).ConfigureAwait(false);
            return _serializer.Deserialize<T>(cachedRaw);
        }

        public async Task SaveAndSerializeAsync<T>(ChainTickerFolder folder, string fileName, T data)
        {
            var serialized = _serializer.Serialize(data);
            await _fileIOService.SaveTextAsync(folder, fileName, serialized).ConfigureAwait(false);
        }


    }
}
