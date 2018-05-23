using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
{
    public class GdaxSubscription : GdaxTypedMessageBase
    {
        [JsonProperty("channels")]
        public List<GdaxChannel> Channels { get; set; }
        
    }
}
