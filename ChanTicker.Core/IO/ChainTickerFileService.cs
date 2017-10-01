using System.Threading.Tasks;

namespace ChanTicker.Core.IO
{
    public class ChainTickerFileService : IChainTickerFileService
    {
        private readonly IDiskCache _cache;
        private readonly IFileIOService _fileIOService;
        private readonly ISerialize _serializer;

        public ChainTickerFileService(IDiskCache cache, IFileIOService fileIOService, ISerialize serializer)
        {
            _cache = cache;
            _fileIOService = fileIOService;
            _serializer = serializer;
        }

        public bool IsCacheStale(CachedFile cachedFile)
            => _cache.IsStale(cachedFile.FileName, cachedFile.CacheAge);

        
        public async Task<T> LoadAndDeserializeAsync<T>(string fileName)
        {
            var cachedRaw = await _fileIOService.LoadAsync(fileName);
            return _serializer.Deserialize<T>(cachedRaw);
        }

        public async Task SaveAndSerializeAsync<T>(string fileName, T data)
        {
            var serialized = _serializer.Serialize(data);
            await _fileIOService.SaveAsync(fileName,serialized).ConfigureAwait(false);
        }


    }
}
