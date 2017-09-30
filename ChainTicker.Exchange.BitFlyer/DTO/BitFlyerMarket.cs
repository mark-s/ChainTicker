using RestSharp.Deserializers;

namespace ChainTicker.Exchange.BitFlyer.DTO
{
    public class BitFlyerMarket
    {
        [DeserializeAs(Name = "alias")]
        public string Alias { get; set; }

        [DeserializeAs(Name = "product_code")]
        public string ProductCode { get; set; }
    }
}