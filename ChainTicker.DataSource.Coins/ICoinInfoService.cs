using System.Collections.Generic;
using System.Threading.Tasks;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins
{
    public interface ICoinInfoService
    {
        Task PopulateAvailableCoinsAsync();

        IEnumerable<string> GetAllCoinCodes();

        IEnumerable<ICoin> GetAllCoins();

        ICoin GetCoinInfo(string coinCode);
    }
}