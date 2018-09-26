using ChainTicker.Exchange.BitFlyer.DTO;
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

        public ITick ConvertToTick(string messageContent)
        {
            EnsureArg.IsNotNull(messageContent, nameof(messageContent));

            var bitFlyerTick = _jsonSerializer.Deserialize<BitFlyerTickDTO>(messageContent);
            return new Tick(bitFlyerTick.LastTradedPrice, 
                                  bitFlyerTick.TickTimeStamp,
                                  bitFlyerTick.BestAsk,
                                  bitFlyerTick.BestBid,
                                  bitFlyerTick.Volume);
        }

    }
}