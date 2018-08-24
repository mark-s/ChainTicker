using Newtonsoft.Json;


namespace ChainTicker.Exchange.BitFlyer.DTO
{
    internal class BitFlyerMarket
    {
        [JsonProperty("product_code")]
        public string ProductCode { get; set; }

        [JsonProperty("main_currency")]
        public string MainCurrency { get; set; }

        [JsonProperty("rate")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("sub_currency")]
        public string SubCurrency { get; set; }
    }
}