using ChanTicker.Core.Domain;

namespace ChainTicker.Exchange.BitFlyer
{
    public static class Config
    {
        public static ExchangeInfo Info { get; } = new ExchangeInfo("bitFlyer",
            "https://bitflyer.jp",
            "bitFlyer Japan",
            true,
            new ApiEndpointCollection
            {
                [ApiEndpointType.Rest] = "https://api.bitflyer.jp",
                [ApiEndpointType.Pubnub] = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f"
            });
    }
}
