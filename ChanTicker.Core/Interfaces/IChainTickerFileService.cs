using System.Threading.Tasks;
using ChanTicker.Core.IO;

namespace ChanTicker.Core.Interfaces
{
    public interface IChainTickerFileService
    {
        bool IsCacheStale(CachedFile cachedFile);

        Task<T> LoadAndDeserializeAsync<T>(ChainTickerFolder folder, string fileName);

        Task SaveAndSerializeAsync<T>(ChainTickerFolder folder, string fileName, T data);
    }
}