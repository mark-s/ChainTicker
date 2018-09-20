using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins
{
    public interface ICoinsService
    {
        Task<IEnumerable<string>> GetAllCoinCodesAsync();

        Task<IEnumerable<ICoin>> GetAllCoinsAsync();

        Task<ICoin> GetCoinInfoAsync(string coinCode);
    }
}