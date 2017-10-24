using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO
{
    public class GdaxSubscribeRequest
    {
        [JsonProperty("channels")]
        public List<Channel> Channels { get; }

        [JsonProperty("type")]
        public string Type { get; }


        public GdaxSubscribeRequest(string marketCode)
        {
            Type = "subscribe";

            Channels = new List<Channel>
                           {
                               new Channel
                                   {
                                       Name = "ticker",
                                       ProductIds = new List<string> {marketCode}
                                   }
                           };
        }

    }
}