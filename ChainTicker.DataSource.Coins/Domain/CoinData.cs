using System.Collections.Generic;
using System.Linq;
using ChainTicker.DataSource.Coins.DTO;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    internal class CoinData 
    {
        protected readonly Dictionary<string, ICoin> CoinDetails = new Dictionary<string, ICoin>(2000);

        public IEnumerable<ICoin> GetAllCoins()
            => CoinDetails.Values;

        public IEnumerable<string> GetAllCoinCodes()
            => CoinDetails.Values.Select(c => c.Code);

        public ICoin GetCoinInfo(string coinCode)
        {
            if (CoinDetails.TryGetValue(coinCode, out ICoin coin))
                return coin;
            else
                return new UnknownCoin(coinCode);
        }

        public CoinData(AllCoinsResponse allCoinsResponse)
        {

            foreach (var item in allCoinsResponse.Data)
            {
                CoinDetails.Add(item.Key, new Coin(item.Value, allCoinsResponse.BaseImageUrl, allCoinsResponse.BaseLinkUrl));
            }
        }
    }
}

