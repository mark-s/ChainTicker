using System.Collections.Generic;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins
{
    public interface ICoinInfoService
    {
        Task GetAvailableCoinsAsync();

        IEnumerable<ICoin> GetAllCoins();

        ICoin GetCoinInfo(string coinCode);
    }
}