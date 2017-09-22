using ChanTicker.Core.Entities;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer.Converters
{
    public static class CoinExtensions
    {
        public static ICoin ConvertToCoinpar(this string coinString)
        {
            // "product_code":"BTC_JPY",

            if(coinString.Contains("_") == false)
                return Coin.UnknownCoin();

            var parts = coinString.Split('_');
            return new Coin(  code: parts[0],
                                         name: parts[1]);
        }

    }
}
