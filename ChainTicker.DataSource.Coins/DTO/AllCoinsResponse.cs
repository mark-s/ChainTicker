using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainTicker.DataSource.Coins.DTO
{
    internal class AllCoinsResponse
    {
        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("BaseImageUrl")]
        public string BaseImageUrl { get; set; }

        [JsonProperty("BaseLinkUrl")]
        public string BaseLinkUrl { get; set; }

        [JsonProperty("DefaultWatchlist")]
        public DefaultWatchlist DefaultWatchlist { get; set; }

        [JsonProperty("Data")]
        public Dictionary<string, CoinInfo> Data { get; set; }

        [JsonProperty("Type")]
        public long Type { get; set; }
    }
}