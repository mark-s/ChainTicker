using System.Threading.Tasks;

namespace ChanTicker.Core.IO
{
    public interface IChainTickerFileService
    {
        bool IsCacheStale(CachedFile cachedFile);

        Task<T> LoadAndDeserializeAsync<T>(ChainTickerFolder folder, string fileName);

        Task SaveAndSerializeAsync<T>(ChainTickerFolder folder, string fileName, T data);
    }
}