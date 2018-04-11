using Newtonsoft.Json;

namespace ChainTicker.DataSource.Coins.DTO
{
    internal class CoinInfo
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("ImageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("CoinName")]
        public string CoinName { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("ProofType")]
        public string ProofType { get; set; }

        [JsonProperty("FullyPremined")]
        public string FullyPremined { get; set; }

        [JsonProperty("TotalCoinSupply")]
        public string TotalCoinSupply { get; set; }

        [JsonProperty("PreMinedValue")]
        public string PreMinedValue { get; set; }

        [JsonProperty("TotalCoinsFreeFloat")]
        public string TotalCoinsFreeFloat { get; set; }

        [JsonProperty("SortOrder")]
        public string SortOrder { get; set; }

        [JsonProperty("Sponsored")]
        public bool Sponsored { get; set; }

        [JsonProperty("IsTrading")]
        public bool IsTrading { get; set; }
    }
}