using System.Threading.Tasks;

namespace ChanTicker.Core.Interfaces
{
    public interface ICoinMarketInfoProvider
    {
        IExchangeInfo ExchangeInfo { get; }

        Task<ICoinMarketInfo[]> GetMarketOverviewAsync();

        Task<ICoinMarketInfo> GetMarketOverviewForCoinAsync(ICoin coin);
    }
}