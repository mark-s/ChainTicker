using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using EnsureThat;

namespace ChainTicker.Exchange.BitFlyer
{
    public class MessageParser
    {
        private readonly IJsonSerializer _jsonSerializer;

        public MessageParser(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = EnsureArg.IsNotNull(jsonSerializer, nameof(jsonSerializer));
        }

        public ITick ConvertToTick(PubnubMessage message)
        {
            EnsureArg.IsNotNull(message, nameof(message));

            var bitFlyerTick = _jsonSerializer.Deserialize<BitFlyerTick>(message.Content);
            return new Tick(bitFlyerTick.LastTradedPrice, 
                                  bitFlyerTick.TickTimeStamp,
                                  bitFlyerTick.BestAsk,
                                  bitFlyerTick.BestBid,
                                  bitFlyerTick.Volume);
        }

    }
}