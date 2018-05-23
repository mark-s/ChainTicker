using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
{
    public class GdaxNonRealtimeTick
    {
        [JsonProperty("size")]
        public decimal Size { get; set; }

        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        [JsonProperty("time", ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }
    }
}