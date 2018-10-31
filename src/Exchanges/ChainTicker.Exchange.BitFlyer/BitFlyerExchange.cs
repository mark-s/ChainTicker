using System;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using ChainTicker.Exchange.BitFlyer.Services;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using EnsureThat;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
        private readonly IChainTickerFileService _chainTickerFileService;
        private readonly IRestService _restService;
        private readonly BitFlyerPriceTicker _bitFlyerPriceTicker;

        public ExchangeInfo Info { get; } = new ExchangeInfo("bitFlyer", "https://bitflyer.jp", "bitFlyer Japan", true,
                                                                                    new ApiEndpointCollection
                                                                                    {
                                                                                        [ApiEndpointType.Rest] = "https://api.bitflyer.jp",
                                                                                        [ApiEndpointType.Pubnub] = "sub-c-52a9ab50-29b-e5-baaa-069f8945a4f"
                                                                                    });

        public BitFlyerExchange(IRestService restService,
                                        IChainTickerFileService chainTickerFileService,
                                        IPubnubTransport pubnubTransport,
                                        IPollingPriceService pollingPriceService,
                                        IJsonSerializer jsonSerializer)
        {
            _restService = EnsureArg.IsNotNull(restService, nameof(restService));

            _chainTickerFileService = EnsureArg.IsNotNull(chainTickerFileService, nameof(chainTickerFileService));

            _bitFlyerPriceTicker = new BitFlyerPriceTicker(pubnubTransport, pollingPriceService, new MessageParser(jsonSerializer));
        }

        public async Task<MarketCollection> GetAvailableMarketsAsync()
        {
            var cachedFile = new CachedFile("BitFlyerAvailableMarkets.json", TimeSpan.FromHours(3));
            var marketsServiceCache = new MarketsServiceCache(_chainTickerFileService, cachedFile);

            var marketsService = new MarketsService(Info.ApiEndpoints, _restService, marketsServiceCache);

            return await marketsService.GetAvailableMarketsAsync();
        }

        public Task<ITick> GetCurrentPriceAsync(IMarket market)
            => _bitFlyerPriceTicker.GetCurrentPriceAsync(market);

        public IObservable<ITick> SubscribeToTicks(IMarket market)
            => _bitFlyerPriceTicker.SubscribeToTicks(market);

        public void UnsubscribeFromTicks(IMarket market) 
            => _bitFlyerPriceTicker.UnsubscribeFromTicks(market);

        public bool IsSubscribedToTicks(IMarket market) 
            => _bitFlyerPriceTicker.IsSubscribedToTicks(market);

        
    }


}
