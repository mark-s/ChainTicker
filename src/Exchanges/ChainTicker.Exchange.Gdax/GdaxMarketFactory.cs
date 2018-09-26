using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Exchange.Gdax.Services;

namespace ChainTicker.Exchange.Gdax
{
    public class GdaxMarketFactory
    {
        private readonly IPriceTicker _priceTicker;

        public GdaxMarketFactory(IPriceTicker priceTicker)
        {
            _priceTicker = priceTicker;
        }


        internal IMarket GetMarket(string productCode, string baseCurrency, string argQuoteCurrency, string displayName, bool hasRealTimeUpdates)
        {
            return new Market(productCode, baseCurrency, argQuoteCurrency, displayName, hasRealTimeUpdates, _priceTicker);
        }

        internal IMarket GetMarket(CachedMarket market)
        {
            return new Market(market, _priceTicker);
        }
    }
}