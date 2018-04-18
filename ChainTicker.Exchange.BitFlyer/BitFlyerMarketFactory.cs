using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Exchange.BitFlyer.DTO;

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerMarketFactory
    {
        private readonly IPriceService _priceService;

        internal BitFlyerMarketFactory(IPriceService priceService)
        {
            _priceService = priceService;
        }

        internal IMarket GetMarket(CachedMarket market)
        {
            return new Market(market, _priceService);
        }

        internal IMarket GetMarket(BitFlyerMarket market, bool isLive)
        {
            return new Market(market.ProductCode,
                market.MainCurrency,
                market.SubCurrency,
                market.ProductCode,
                isLive,
                _priceService);
        }

    }
}