using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Exchange.Gdax.Services;

namespace ChainTicker.Exchange.Gdax
{
    public class GdaxMarketFactory
    {
        private readonly IPriceService _priceService;

        public GdaxMarketFactory(IPriceService priceService)
        {
            _priceService = priceService;
        }


        internal IMarket GetMarket(string productCode, string baseCurrency, string argQuoteCurrency, string displayName, bool hasRealTimeUpdates)
        {
            return new Market(productCode, baseCurrency, argQuoteCurrency, displayName, hasRealTimeUpdates, _priceService);
        }

        internal IMarket GetMarket(CachedMarket market)
        {
            return new Market(market, _priceService);
        }
    }
}