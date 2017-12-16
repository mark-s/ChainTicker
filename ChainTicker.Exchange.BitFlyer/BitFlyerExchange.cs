using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.Services;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange, IDisposable
    {
        private readonly BitFlyerMarketDataService _marketDataService;
        private readonly BitFlyerMarketsService _bitFlyerMarketsService;


        public ExchangeInfo Info { get; } = new ExchangeInfo("bitFlyer",
                                                             "https://bitflyer.jp",
                                                             "bitFlyer Japan",
                                                             true,
                                                             new ApiEndpointCollection
                                                                 {
                                                                     [ApiEndpointType.WebSocket] = "wss://ws-feed.gdax.com",
                                                                     [ApiEndpointType.Rest] = "https://api.bitflyer.jp",
                                                                     [ApiEndpointType.Pubnub] = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f"
                                                             });
        

        public BitFlyerExchange(IRestService restService, IChainTickerFileService chainTickerFileService, ISerialize jsonSerializer)
        {

            var pubnubTransport = new PubnubTransport(Info.ApiEndpoints[ApiEndpointType.Pubnub], new DebugLogger());
            _marketDataService = new BitFlyerMarketDataService(pubnubTransport, new NotRealTimePriceService(restService, Info.ApiEndpoints, TimeSpan.FromSeconds(3), jsonSerializer), jsonSerializer);

            _bitFlyerMarketsService = new BitFlyerMarketsService(Info.ApiEndpoints, restService, chainTickerFileService, jsonSerializer);
        }


        public Task<ITick> GetCurrentPriceAsync(Market market)
            => _marketDataService.GetCurrentPriceAsync(market);

        public bool IsSubscribedToTicks(Market market)
            => _marketDataService.IsSubscribedToTicks(market);

        public IObservable<ITick> SubscribeToTicks(Market market)
            => _marketDataService.SubscribeToTicks(market);

        public void UnsubscribeFromTicks(Market market) 
            => _marketDataService.UnsubscribeFromTicks(market);

        public Task<List<Market>> GetAvailableMarketsAsync()
            => _bitFlyerMarketsService.GetAvailableMarketsAsync();

        public void Dispose()
        {
            _marketDataService?.Dispose();
        }
    }
}
