using Newtonsoft.Json;

namespace ChainTicker.Exchange.BitFlyer.DTO
{
    public class PlainMarket
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("product_code")]
        public string ProductCode { get; set; }
    }
}