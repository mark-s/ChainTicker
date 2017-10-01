using System.Collections.Generic;
using System.Threading.Tasks;
using ChanTicker.Core.Domain;

namespace ChanTicker.Core.Interfaces
{
    public interface IExchange
    {

        ExchangeInfo Info { get; }

        Task<List<Market>> GetAvailableMarketsAsync();

    }
}
