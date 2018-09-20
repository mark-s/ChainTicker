using System;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Services
{
    public interface ICacheSource
    {
        Task GetFromCacheAsync(Action<AllCoinsResponse> onSuccess, Action<string> onFailure);

        bool IsCacheStale();

        Task UpdateCachedDataAsync(AllCoinsResponse response);
    }
}