using System.Collections.Generic;
using System.Linq;
using ChainTicker.DataSource.Coins.DTO;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    internal class CoinData 
    {
        private readonly Dictionary<string, ICoin> _coinDetails = new Dictionary<string, ICoin>(2000);

        public IEnumerable<ICoin> GetAllCoins()
            => _coinDetails.Values;

        public IEnumerable<string> GetAllCoinCodes()
            => _coinDetails.Values.Select(c => c.Code);

        public ICoin GetCoinInfo(string coinCode)
        {
            if (_coinDetails.TryGetValue(coinCode, out ICoin coin))
                return coin;
            else
                return new UnknownCoin(coinCode);
        }

        internal CoinData(AllCoinsResponse allCoinsResponse)
        {
            foreach (var item in allCoinsResponse.Data)
            {
                _coinDetails.Add(item.Key, new Coin(item.Value, allCoinsResponse.BaseImageUrl, allCoinsResponse.BaseLinkUrl));
            }
        }
    }
}

