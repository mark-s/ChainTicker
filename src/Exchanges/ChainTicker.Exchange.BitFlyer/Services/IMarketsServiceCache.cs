using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public interface IMarketsServiceCache
    {

        bool IsCacheStale();

        Task WriteCacheAsync(MarketCollection marketCollection);

        Task<MarketCollection> ReadCacheAsync();
    }
}