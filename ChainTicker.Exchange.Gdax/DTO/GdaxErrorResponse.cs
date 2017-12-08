using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO
{
    public class GdaxErrorResponse
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}