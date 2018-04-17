using System.Threading.Tasks;
using ChainTicker.Core.IO;

namespace ChainTicker.Core.Interfaces
{
    public interface IChainTickerFileService
    {
        bool IsCacheStale(CachedFile cachedFile);

        Task<T> LoadAndDeserializeAsync<T>(ChainTickerFolder folder, string fileName);

        Task SaveAndSerializeAsync<T>(ChainTickerFolder folder, string fileName, T data);
    }
}