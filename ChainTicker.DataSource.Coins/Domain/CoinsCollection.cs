using System.Collections.Generic;
using System.Linq;
using ChainTicker.DataSource.Coins.DTO;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    internal sealed class CoinsCollection 
    {
        private readonly Dictionary<string, ICoin> _coins = new Dictionary<string, ICoin>(2000);

        public IEnumerable<ICoin> GetAllCoins()
            => _coins.Values;

        public IEnumerable<string> GetAllCoinCodes()
            => _coins.Values.Select(c => c.Code);

        public ICoin GetCoin(string coinCode)
        {
            if (_coins.TryGetValue(coinCode, out var coin))
                return coin;
            else
                return new UnknownCoin(coinCode);
        }

        internal CoinsCollection(AllCoinsResponse allCoinsResponse)
        {
            foreach (var item in allCoinsResponse.Data)
            {
                _coins.Add(item.Key, new Coin(item.Value, allCoinsResponse.BaseImageUrl, allCoinsResponse.BaseLinkUrl));
            }
        }

    }
}

