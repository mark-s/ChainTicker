namespace ChainTicker.DataSource.Coins.Domain
{
    internal class CoinDataUnavailable : CoinDataBase
    {
        public CoinDataUnavailable()
        {
            var unknownCoin = new UnknownCoin();
            CoinDetails.Add(unknownCoin.Code, unknownCoin);
        }
    }
}