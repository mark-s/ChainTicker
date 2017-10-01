﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange, IMarketDataService
    {
        private PubnubTransport _pubnubTransport;
        private readonly IMarketDataService _marketDataService;
        private readonly BitFlyerMarketsService _bitFlyerMarketsService;

        public ExchangeInfo Info { get; } = new ExchangeInfo("BitFlyer", 
                                                                                 "https://bitflyer.jp", 
                                                                                 "BitFlyer Japan", 
                                                                                 true, 
                                                                                 "https://api.bitflyer.jp");

        private const string SUBSCRIBE_KEY = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f";


        public BitFlyerExchange(IRestService restService,
                                        ISerialize serialiser,
                                        IDiskCache diskCache,
                                        IFileIOService fileIOService)
        {
            _pubnubTransport = new PubnubTransport(SUBSCRIBE_KEY, new DebugLogger());
            _marketDataService = new BitFlyerMarketDataService(Info.ApiBaseUrl, restService, serialiser);
            _bitFlyerMarketsService = new BitFlyerMarketsService(Info.ApiBaseUrl, restService, serialiser, fileIOService, diskCache);
        }


        public Task<ITick> GetCurrentPriceAsync(Market market)
            => _marketDataService.GetCurrentPriceAsync(market);

        public Task<List<Market>> GetAvailableMarketsAsync()
            => _bitFlyerMarketsService.GetAvailableMarketsAsync();

    }
}
