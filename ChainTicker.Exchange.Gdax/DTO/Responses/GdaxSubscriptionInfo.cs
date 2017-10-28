using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
{
    public class GdaxSubscription : GdaxTypedMessageBase
    {
        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }
        
    }

    public class Channel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_ids")]
        public List<string> ProductIds { get; set; }
    }
}
