using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public class BitFlyerMarketsService
    {
        private readonly ISerialize _jsonSerialiser;
        private readonly IPriceDataService _priceDataService;

        private readonly ApiEndpointCollection _apiEndpoints;
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _fileService;

        private const string CACHE_FILE_NAME = "BitFlyerAvailableMarkets.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromHours(3);

        public BitFlyerMarketsService(ApiEndpointCollection apiEndpoints,
                                                        IRestService restService,
                                                        IChainTickerFileService fileService,
                                                        ISerialize jsonSerialiser,
                                                        IPriceDataService priceDataService)
        {
            _apiEndpoints = apiEndpoints;
            _restService = restService;
            _fileService = fileService;
            _jsonSerialiser = jsonSerialiser;
            _priceDataService = priceDataService;
        }

        public async Task<List<Market>> GetAvailableMarketsAsync()
        {
            if (_fileService.IsCacheStale(new CachedFile(CACHE_FILE_NAME, _maxCacheAge)))
                return await GetFromWebServiceAsync();
            else
                return await GetFromCacheAsync();
        }

        private async Task<List<Market>> GetFromWebServiceAsync()
        {
            var availableMarkets = new List<Market>();

            // note: Using 'getprices' here as it returns nicer data than 'getmarkets'
            var getPricesQuery = new RestQuery(_apiEndpoints[ApiEndpointType.Rest], "/v1/getprices");
            var getPricesResponse = await _restService.GetAsync(getPricesQuery.GetAddress(), s => _jsonSerialiser.Deserialize<List<BitFlyerMarket>>(s));

            // but it returns All markets - even ones that can't be subscribed to... So need to flag them
            var getMarketsQuery = new RestQuery(_apiEndpoints[ApiEndpointType.Rest], "/v1/getmarkets");
            var getMarketsResponse = await _restService.GetAsync(getMarketsQuery.GetAddress(), s => _jsonSerialiser.Deserialize<List<PlainMarket>>(s));

            if (getPricesResponse.IsSuccess && getMarketsResponse.IsSuccess)
            {
                var marketsWithLivePrices = getMarketsResponse.Data;
                availableMarkets.AddRange(getPricesResponse.Data.Select(m => 
                                                                new Market(m.ProductCode,
                                                                    m.MainCurrency, 
                                                                    m.SubCurrency,
                                                                    m.ProductCode, 
                                                                    m.CurrentPrice, 
                                                                    marketsWithLivePrices.Any(p => p.ProductCode == m.ProductCode))));

                foreach (var market in availableMarkets)
                    market.SetPriceDataService(_priceDataService);

                await _fileService.SaveAndSerializeAsync(ChainTickerFolder.Cache, CACHE_FILE_NAME, availableMarkets);
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + getPricesResponse.ErrorMessage);
            }

            return availableMarkets;
        }

        private async Task<List<Market>> GetFromCacheAsync()
        {
            var marketsFromCache = await  _fileService.LoadAndDeserializeAsync<List<Market>>(ChainTickerFolder.Cache, CACHE_FILE_NAME);

            // if we're restoring from the cached file the prices will be stale, reset them here
            foreach (var market in marketsFromCache)
            {
                market.ClearMidMarketPriceSnapshot();
                market.SetPriceDataService(_priceDataService);
            }

            return marketsFromCache;
        }
    }
}
