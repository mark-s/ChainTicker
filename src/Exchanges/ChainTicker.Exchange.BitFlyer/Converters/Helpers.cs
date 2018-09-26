using ChainTicker.Core.Domain;
using ChainTicker.Exchange.BitFlyer.DTO;
using EnsureThat;

namespace ChainTicker.Exchange.BitFlyer.Converters
{
    internal static class Helpers
    {

        public static IMarket ConvertToMarket(BitFlyerMarketDTO marketDTO, bool isLive)
        {
                EnsureArg.IsNotNull(marketDTO, nameof(marketDTO));

                return new Market(marketDTO.ProductCode,
                    marketDTO.MainCurrency,
                    marketDTO.SubCurrency,
                    marketDTO.ProductCode,
                    isLive);
        }

    }
}
