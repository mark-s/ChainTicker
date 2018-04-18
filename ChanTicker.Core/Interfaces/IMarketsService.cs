using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;

namespace ChainTicker.Core.Interfaces
{
    public interface IMarketsService
    {
        Task<List<IMarket>> GetAvailableMarketsAsync();
    }
}