using System;
using System.Threading.Tasks;
using ChainTicker.Transport.WebSocket;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.Gdax.Services
{
    public class GdaxMarketDataService : IMarketDataService
    {
        public GdaxMarketDataService(IWebSocketTransport webSocketTransport, ApiEndpointCollection apiEndpoints)
        {
            //throw new NotImplementedException();
        }

        public Task<ITick> GetCurrentPriceAsync(Market market)
        {
            return null;
            //throw new NotImplementedException();
        }

        public IObservable<ITick> SubscribeToTicks(Market market)
        {
            return null;
            //throw new NotImplementedException();
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