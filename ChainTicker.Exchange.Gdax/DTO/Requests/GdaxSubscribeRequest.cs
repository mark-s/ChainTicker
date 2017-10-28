using System.Collections.Generic;
using ChainTicker.Exchange.Gdax.DTO.Responses;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Requests
{
    public class GdaxSubscribeRequest : GdaxTypedMessageBase
    {
        [JsonProperty("channels")]
        public List<Channel> Channels { get; }


        public GdaxSubscribeRequest(string productCode)
        {
            Type = GdaxMessageType.Subscribe.ToString().ToLowerInvariant();

            Channels = new List<Channel>
                           {
                               new Channel
                                   {
                                       Name = "ticker",
                                       ProductIds = new List<string> { productCode }
                                   }
                           };
        }

    }
}