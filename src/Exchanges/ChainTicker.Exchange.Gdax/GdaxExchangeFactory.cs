﻿using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Exchange.Gdax.Services;
using ChainTicker.Transport.Rest;
using ChainTicker.Transport.WebSocket;
using System.Threading.Tasks;

namespace ChainTicker.Exchange.Gdax
{
    public class GdaxExchangeFactory : IExchangeFactory
    {
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _chainTickerFileService;
        private readonly IJsonSerializer _jsonSerializer;

        private readonly ExchangeInfo _exchangeInfo = new ExchangeInfo("Gdax", "https://gdax.com", "Global Digital Asset Exchange", true,
            new ApiEndpointCollection
            {
                [ApiEndpointType.WebSocket] = "wss://ws-feed.gdax.com",
                [ApiEndpointType.Rest] = "https://api.gdax.com"
            });


        public GdaxExchangeFactory(IRestService restService, IChainTickerFileService chainTickerFileService, IJsonSerializer jsonSerializer)
        {
            _restService = restService;
            _chainTickerFileService = chainTickerFileService;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<IExchange> GetExchangeAsync()
        {
            var webSocketTransport = new WebSocketTransport(_exchangeInfo.ApiEndpoints[ApiEndpointType.WebSocket]);
            var notRealTimeService = new PollingPriceService(_restService, _exchangeInfo.ApiEndpoints);
            var priceService = new PriceTicker(webSocketTransport, notRealTimeService, _jsonSerializer);
            var marketFactory = new GdaxMarketFactory(priceService);

            var marketsService = new MarketsService(_exchangeInfo.ApiEndpoints, _restService, _chainTickerFileService, marketFactory);

            var markets = await marketsService.GetAvailableMarketsAsync();

            return new GdaxExchange(_exchangeInfo, markets);

        }
    }
}