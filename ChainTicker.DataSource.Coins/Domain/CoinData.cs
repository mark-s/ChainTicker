using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Domain
{
    public class CoinData
    {
        private readonly AllCoins _allCoins;
        private string _baseImageUrl;
        private string _baseLinkUrl;


        public CoinData(AllCoins allCoins, string baseImageUrl, string baseLinkUrl)
        {
            _allCoins = allCoins;
            _baseImageUrl = baseImageUrl;
            _baseLinkUrl = baseLinkUrl;
        }

    }
}
