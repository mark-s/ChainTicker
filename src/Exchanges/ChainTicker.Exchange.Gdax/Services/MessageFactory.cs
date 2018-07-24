using ChainTicker.Exchange.Gdax.DTO.Requests;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Exchange.Gdax.Services
{
    internal class MessageFactory
    {
        private readonly IJsonSerializer _jsonSerializer;

        public MessageFactory(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string CreateSubscribeMessage(string marketProductCode)
        {
            var gdaxSubscribeRequest = new GdaxSubscribeRequest(marketProductCode);
            return _jsonSerializer.Serialize(gdaxSubscribeRequest);
        }


        public string CreateUnsubscribeMessage(string marketProductCode)
        {
            var gdaxSubscribeRequest = new GdaxUnsubscribeRequest(marketProductCode);
            return _jsonSerializer.Serialize(gdaxSubscribeRequest);
        }
    }
}