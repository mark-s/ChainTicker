using System;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Exchange.BitFlyer.Services;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchangeFactory : IExchangeFactory
    {
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _chainTickerFileService;
        private readonly IJsonSerializer _jsonSerializer;

        private readonly ExchangeInfo _exchangeInfo = new ExchangeInfo("bitFlyer", "https://bitflyer.jp", "bitFlyer Japan", true,
            new ApiEndpointCollection
            {
                [ApiEndpointType.WebSocket] = "wss://ws-feed.gdax.com",
                [ApiEndpointType.Rest] = "https://api.bitflyer.jp",
                [ApiEndpointType.Pubnub] = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f"
            });

        public BitFlyerExchangeFactory(IRestService restService, IChainTickerFileService chainTickerFileService, IJsonSerializer jsonSerializer)
        {
            _restService = restService;
            _chainTickerFileService = chainTickerFileService;
            _jsonSerializer = jsonSerializer;
        }


        public async Task<IExchange> GetExchangeAsync()
        {
            var pubnubTransport = new PubnubTransport(_exchangeInfo.ApiEndpoints[ApiEndpointType.Pubnub], new DebugLogger());
            var pollingPriceService = new PollingPriceService(_restService, _exchangeInfo.ApiEndpoints[ApiEndpointType.Rest], TimeSpan.FromSeconds(3));
            var messageParser = new MessageParser(_jsonSerializer);
            var priceService = new PriceService(pubnubTransport, pollingPriceService, messageParser);
            var marketFactory = new BitFlyerMarketFactory(priceService);
            var marketsService = new MarketsService(_exchangeInfo.ApiEndpoints, _restService, _chainTickerFileService, marketFactory);

            var markets = await marketsService.GetAvailableMarketsAsync();

            return new BitFlyerExchange(_exchangeInfo, markets);
        }

    }



}