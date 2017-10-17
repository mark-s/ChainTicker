using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Pubnub;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer
{
    public class MessageParser
    {
        private readonly ISerialize _jsonSerializer;

        public MessageParser(ISerialize jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public ITick ConvertToTick(PubnubMessage message)
        {
            var bitFlyerTick = _jsonSerializer.Deserialize<BitFlyerTick>(message.Content);
            return new Tick(bitFlyerTick.LastTradedPrice, bitFlyerTick.TickTimeStamp);
        }

    }
}