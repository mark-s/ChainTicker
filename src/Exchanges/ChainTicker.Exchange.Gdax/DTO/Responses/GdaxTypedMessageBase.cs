using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
{
    public abstract class GdaxTypedMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

    }


}
