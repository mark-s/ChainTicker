using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer
{
    public class MessageParser
    {
        private readonly IJsonSerializer _jsonSerializer;

        public MessageParser(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public ITick ConvertToTick(PubnubMessage message)
        {
            var bitFlyerTick = _jsonSerializer.Deserialize<BitFlyerTick>(message.Content);
            return new Tick(bitFlyerTick.LastTradedPrice, 
                                  bitFlyerTick.TickTimeStamp,
                                  bitFlyerTick.BestAsk,
                                  bitFlyerTick.BestBid,
                                  bitFlyerTick.Volume);
        }

    }
}