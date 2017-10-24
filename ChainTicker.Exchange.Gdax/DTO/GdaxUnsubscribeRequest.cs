using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO
{
    public class GdaxUnsubscribeRequest
    {
        [JsonProperty("channels")]
        public List<Channel> Channels { get; }

        [JsonProperty("type")]
        public string Type { get; }


        public GdaxUnsubscribeRequest(List<string> products)
        {
            Type = "subscribe";

            Channels = new List<Channel>(products.Count)
                           {
                               new Channel
                                   {
                                       Name = "ticker",
                                       ProductIds = products
                                   }
                           };
        }

    }


}
