using System.Threading.Tasks;

namespace ChainTicker.DataSource.Coins
{
    public interface ICoinInfoCacheService
    {
        bool IsStale(CoinInfoServiceConfig config);

        Task<T> LoadAsync<T>(string fileName);

        Task SaveAsync<T>(string fileName, T data);
    }
}