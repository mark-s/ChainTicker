using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
{
    public class GdaxMarket
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("base_max_size")]
        public string BaseMaxSize { get; set; }

        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("base_min_size")]
        public string BaseMinSize { get; set; }

        [JsonProperty("margin_enabled")]
        public bool MarginEnabled { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("quote_increment")]
        public string QuoteIncrement { get; set; }
    }
}