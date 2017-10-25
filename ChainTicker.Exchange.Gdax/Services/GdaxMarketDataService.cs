using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.Gdax.DTO;
using ChainTicker.Transport.WebSocket;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.Gdax.Services
{
    public class GdaxMarketDataService : IMarketDataService
    {
        private readonly IWebSocketTransport _webSocketTransport;
        private readonly ApiEndpointCollection _apiEndpoints;
        private readonly ISerialize _serializer;

        public GdaxMarketDataService(IWebSocketTransport webSocketTransport, ApiEndpointCollection apiEndpoints, ISerialize serializer)
        {
            _webSocketTransport = webSocketTransport;
            _apiEndpoints = apiEndpoints;
            _serializer = serializer;
        }

        public Task<ITick> GetCurrentPriceAsync(Market market)
        {
            return null;
            //throw new NotImplementedException();
        }

        public IObservable<ITick> SubscribeToTicks(Market market)
        {

            var subMsg = new GdaxSubscribeRequest(market.ProductCode);
            _webSocketTransport.Send(_serializer.Serialize(subMsg));
            return _webSocketTransport.RecievedMessagesObservable
                                      .Where(m => JsonHelpers.GetType(m) == "ticker")
                                      .Select(t => _serializer.Deserialize<GdaxTick>(t))
                                      .Where(t => t.ProductId == market.ProductCode)
                                      .Select(GetAsTick);
        }

        private ITick GetAsTick(GdaxTick gdaxTick)
        {
            return new Tick(decimal.Parse(gdaxTick.Price), DateTimeOffset.Now, Convert.ToDecimal(gdaxTick.BestAsk), Convert.ToDecimal(gdaxTick.BestBid), Convert.ToDouble(gdaxTick.Volume24h));
        }

        public void UnsubscribeFromTicks(Market market)
        {
            //throw new NotImplementedException();
        }

        public bool IsSubscribedToTicks(Market market)
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}