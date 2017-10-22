using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Exchange.Gdax.Services;
using ChainTicker.Transport.WebSocket;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.Gdax
{
    public class GdaxExchange : IExchange, IDisposable
    {
        private IWebSocketTransport _webSocketTransport;
        private GdaxMarketDataService _marketDataService;

        public ExchangeInfo Info { get; } = new ExchangeInfo("Gdax",
                                                             "https://gdax.com",
                                                             "Global Digital Asset Exchange",
                                                             true,
                                                             new ApiEndpointCollection
                                                                 {
                                                                     [ApiEndpointType.WebSocket] = "wss://ws-feed.gdax.com",
                                                                     [ApiEndpointType.Rest] = "https://api.gdax.com"
                                                                 });

        public GdaxExchange()
        {
            _webSocketTransport = new WebSocketTransport();
            _marketDataService = new GdaxMarketDataService(_webSocketTransport, Info.ApiEndpoints);
        }

        public Task<List<Market>> GetAvailableMarketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ITick> GetCurrentPriceAsync(Market market)
        {
            throw new NotImplementedException();
        }

        public bool IsSubscribedToTicks(Market market)
        {
            throw new NotImplementedException();
        }

        public IObservable<ITick> SubscribeToTicks(Market market)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeFromTicks(Market market)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
