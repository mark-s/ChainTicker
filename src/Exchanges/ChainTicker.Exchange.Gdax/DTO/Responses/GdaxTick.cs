using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
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


    public class GdaxTick : GdaxTypedMessageBase
    {

        [JsonProperty("low_24h")]
        public decimal Low24h { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("best_bid")]
        public decimal BestBid { get; set; }

        [JsonProperty("best_ask")]
        public decimal BestAsk { get; set; }

        [JsonProperty("high_24h")]
        public decimal High24h { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("open_24h")]
        public decimal Open24h { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("volume_24h")]
        public double Volume24h { get; set; }

        [JsonProperty("volume_30d")]
        public double Volume30d { get; set; }
    }
}
