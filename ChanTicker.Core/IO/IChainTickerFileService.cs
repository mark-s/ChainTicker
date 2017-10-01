using System.Threading.Tasks;

namespace ChanTicker.Core.IO
{
    public interface IChainTickerFileService
    {
        bool IsCacheStale(CachedFile cachedFile);

        Task<T> LoadAndDeserializeAsync<T>(string fileName);

        Task SaveAndSerializeAsync<T>(string fileName, T data);
    }
}