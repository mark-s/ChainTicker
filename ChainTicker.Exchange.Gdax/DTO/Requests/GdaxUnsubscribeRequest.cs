using System.Collections.Generic;
using ChainTicker.Exchange.Gdax.DTO.Responses;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Requests
{
    public class GdaxUnsubscribeRequest : GdaxTypedMessageBase
    {
        [JsonProperty("channels")]
        public List<Channel> Channels { get; }

        
        public GdaxUnsubscribeRequest(string productCode)
        {
            Type = GdaxMessageType.Unsubscribe.ToString().ToLowerInvariant();

            Channels = new List<Channel>(1)
                           {
                               new Channel
                                   {
                                       Name = "ticker",
                                       ProductIds = new List<string> {productCode}
                                   }
                           };
        }

    }


}
