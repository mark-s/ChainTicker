using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChanTicker.Core.Interfaces
{
    public interface IMarketDataSource
    {
        Task<List<IMarket>> GetAvailableMarketsAsync();

        Task<ITick> GetCurrentPriceForMarketAsync(string marketId);
    }
}