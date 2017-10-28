using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
{
    public class GdaxError : GdaxTypedMessageBase
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
        
    }
}