using ChainTicker.DataSource.Coins.DTO;

namespace ChainTicker.DataSource.Coins.Domain
{
    internal static class ConvertAllCoinsResponse
    {
        internal static CoinsCollection ToCoinsCollection(AllCoinsResponse allCoinsResponse)
        {
            var coinsCollection = new CoinsCollection();

            foreach (var item in allCoinsResponse.Data)
                coinsCollection.AddCoin(item.Key, new Coin(item.Value, allCoinsResponse.BaseImageUrl, allCoinsResponse.BaseLinkUrl));

            return coinsCollection;
        }
    }
}