using Newtonsoft.Json;  

namespace ChainTicker.Exchange.Gdax.DTO
{

    //{
    //"type": "subscribe",
    //"channels": [
    //{
    //    "name": "ticker",
    //    "product_ids": [
    //    "ETH-USD",
    //    "ETH-EUR",
    //    "ETH-BTC",
    //    "BTC-GBP"
    //        ]

    //}
    //]


    public class GdaxTick
    {
        [JsonProperty("low_24h")]
        public string Low24h { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("best_bid")]
        public string BestBid { get; set; }

        [JsonProperty("best_ask")]
        public string BestAsk { get; set; }

        [JsonProperty("high_24h")]
        public string High24h { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("open_24h")]
        public string Open24h { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("volume_24h")]
        public string Volume24h { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("volume_30d")]
        public string Volume30d { get; set; }
    }
}
