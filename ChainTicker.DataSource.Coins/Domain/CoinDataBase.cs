using System.Collections.Generic;
using System.Linq;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    internal class CoinDataBase
    {
        protected readonly Dictionary<string, ICoin> CoinDetails = new Dictionary<string, ICoin>(2000);

        public IEnumerable<ICoin> GetAllCoins()
            => CoinDetails.Values;

        public IEnumerable<string> GetAllCoinCodes()
            => CoinDetails.Values.Select(c => c.Code);

        public ICoin GetCoinInfo(string coinCode)
            => CoinDetails[coinCode];

    }
}