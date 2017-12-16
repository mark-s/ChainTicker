using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Exchange.Gdax.Services;
using ChainTicker.Transport.Rest;
using ChainTicker.Transport.WebSocket;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.Gdax
{
    public class GdaxExchange : IExchange, IDisposable
    {
        private readonly GdaxMarketDataService _marketDataService;
        private readonly GdaxMarketsService _gdaxMarketsService;

        public ExchangeInfo Info { get; } = new ExchangeInfo("Gdax",
                                                             "https://gdax.com",
                                                             "Global Digital Asset Exchange",
                                                             true,
                                                             new ApiEndpointCollection
                                                             {
                                                                 [ApiEndpointType.WebSocket] = "wss://ws-feed.gdax.com",
                                                                 [ApiEndpointType.Rest] = "https://api.gdax.com"
                                                             });

        public GdaxExchange(IRestService restService, IChainTickerFileService fileService, ISerialize jsonSerializer)
        {
            var webSocketTransport = new WebSocketTransport(Info.ApiEndpoints[ApiEndpointType.WebSocket]);
            var notRealTimeService = new NotRealTimePriceService(restService, Info.ApiEndpoints, jsonSerializer);

            _marketDataService = new GdaxMarketDataService(webSocketTransport, notRealTimeService, jsonSerializer);
            _gdaxMarketsService = new GdaxMarketsService(Info.ApiEndpoints, restService, fileService);
        }

        public Task<List<Market>> GetAvailableMarketsAsync()
            => _gdaxMarketsService.GetAvailableMarketsAsync();

        public Task<ITick> GetCurrentPriceAsync(Market market)
            => _marketDataService.GetCurrentPriceAsync(market);

        public bool IsSubscribedToTicks(Market market)
            => _marketDataService.IsSubscribedToTicks(market);

        public IObservable<ITick> SubscribeToTicks(Market market) 
            => _marketDataService.SubscribeToTicks(market);

        public void UnsubscribeFromTicks(Market market)
            => _marketDataService.UnsubscribeFromTicks(market);

        public void Dispose()
        {
            
        }
    }
}
