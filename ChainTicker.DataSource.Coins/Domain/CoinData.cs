using System.Diagnostics;
using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Domain
{
    internal class CoinData : CoinDataBase
    {
        public CoinData(AllCoinsResponse allCoinsResponse)
        {

            foreach (var item in allCoinsResponse.Data)
            {
                CoinDetails.Add(item.Key, new Coin(item.Value, allCoinsResponse.BaseImageUrl, allCoinsResponse.BaseLinkUrl));
                Debug.WriteLine("Added coin: " + item.Key);
            }
        }
    }
}

