using System.Threading.Tasks;

namespace ChanTicker.Core.Interfaces
{
    public interface ICoinMarketInfoProvider
    {
        ISourceInfo SourceInfo { get; }

        Task<ICoinMarketInfo[]> GetMarketOverviewAsync();

        Task<ICoinMarketInfo> GetMarketOverviewForCoinAsync(ICoin coin);
    }
}