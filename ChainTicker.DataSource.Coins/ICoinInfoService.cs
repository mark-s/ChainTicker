using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Domain;

namespace ChainTicker.DataSource.Coins
{
    public interface ICoinInfoService
    {
        Task<CoinData> GetAllCoinsAsync();
    }
}