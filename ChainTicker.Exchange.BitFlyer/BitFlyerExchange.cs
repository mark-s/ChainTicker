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
                                                                                 "https://api.bitflyer.jp");

        private const string SUBSCRIBE_KEY = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f";


        public BitFlyerExchange(IRestService restService, IChainTickerFileService chainTickerFileService)
        {

            var pubnubTransport = new PubnubTransport(SUBSCRIBE_KEY, new DebugLogger());
            _marketDataService = new BitFlyerMarketDataService(pubnubTransport, new CurrentPriceQueryService(restService, Info.ApiBaseUrl, TimeSpan.FromSeconds(3)));

            _bitFlyerMarketsService = new BitFlyerMarketsService(Info.ApiBaseUrl, restService, chainTickerFileService);
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
