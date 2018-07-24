using System.Collections.Generic;
using ChainTicker.Exchange.Gdax.DTO.Responses;
using Newtonsoft.Json;

namespace ChainTicker.Exchange.Gdax.DTO.Requests
{
    public class GdaxUnsubscribeRequest : GdaxTypedMessageBase
    {
        [JsonProperty("channels")]
        public List<GdaxChannel> Channels { get; }

        
        public GdaxUnsubscribeRequest(string productCode)
        {
            Type = GdaxMessageType.Unsubscribe.ToString().ToLowerInvariant();

            Channels = new List<GdaxChannel>(1)
                           {
                               new GdaxChannel
                                   {
                                       Name = "ticker",
                                       ProductIds = new List<string> {productCode}
                                   }
                           };
        }

    }


}
