using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ChainTicker.DataSource.Coins.DTO;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.DataSource.Coins.Domain
{
    public class CoinData
    {
        private readonly Dictionary<string, ICoin> _coinDetails = new Dictionary<string, ICoin>(2000);

        public CoinData(AllCoinsResponse allCoinsResponse)
        {
            foreach (var item in allCoinsResponse.Data)
            {
                _coinDetails.Add(item.Key, new Coin(item.Value, allCoinsResponse.BaseImageUrl, allCoinsResponse.BaseLinkUrl));
                Debug.WriteLine("Added coin: " + item.Key);
            }
        }

        public IEnumerable<string> GetAllCoinCodes()
            => _coinDetails.Values.Select(c => c.Code);

        public ICoin GetCoinInfo(string coinCode)
            => _coinDetails[coinCode];


    }
}
