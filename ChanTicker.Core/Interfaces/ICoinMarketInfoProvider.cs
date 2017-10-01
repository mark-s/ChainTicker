using System.Threading.Tasks;
using ChanTicker.Core.Domain;

namespace ChanTicker.Core.Interfaces
{
    public interface ICoinMarketInfoProvider
    {
        ExchangeInfo ExchangeInfo { get; }

        Task<ICoinMarketInfo[]> GetMarketOverviewAsync();

        Task<ICoinMarketInfo> GetMarketOverviewForCoinAsync(ICoin coin);
    }
}