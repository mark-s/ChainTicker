using Newtonsoft.Json;

namespace ChainTicker.DataSource.Coins.DTO
{
    internal class DefaultWatchlist
    {
        [JsonProperty("CoinIs")]
        public string CoinIs { get; set; }

        [JsonProperty("Sponsored")]
        public string Sponsored { get; set; }
    }
}