using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Responses
{
    public class GdaxChannel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_ids")]
        public List<string> ProductIds { get; set; }
    }
}