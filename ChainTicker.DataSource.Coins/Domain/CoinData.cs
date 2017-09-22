using System;
using System.Collections.Generic;
using System.Linq;
using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Domain
{
    public class CoinData
    {
        private readonly AllCoinsResponse _allCoinsResponse;

        public CoinData(AllCoinsResponse allCoinsResponse)
        {
            _allCoinsResponse = allCoinsResponse;
        }

        public IEnumerable<string> GetAllCoinCodes()
            => _allCoinsResponse.Coins.Values.Select(c => c.Name);
        
        public CoinInfo GetCoinInfo(string coinCode)
            => _allCoinsResponse.Coins[coinCode];


    }
}
