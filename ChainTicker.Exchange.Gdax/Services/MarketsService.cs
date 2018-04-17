using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.Gdax.DTO.Responses;
using ChainTicker.Transport.Rest;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;

namespace ChainTicker.Exchange.Gdax.Services
{
    public class MarketsService : IMarketsService
    {

        private readonly IJsonSerializer _serialiser;

        private readonly ApiEndpointCollection _apiEndpoints;
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _fileService;

        private const string CACHE_FILE_NAME = "GdaxAvailableMarkets.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromHours(3);

        public MarketsService(ApiEndpointCollection apiEndpoints,
                                                IRestService restService,
                                                IChainTickerFileService fileService)
        {
            _serialiser = new ChainTickerJsonSerializer();

            _apiEndpoints = apiEndpoints;
            _restService = restService;
            _fileService = fileService;
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

            var getPricesQuery = new RestQuery(_apiEndpoints[ApiEndpointType.Rest], "/products");
            var getPricesResponse = await _restService.GetAsync(getPricesQuery.GetAddress(), s => _serialiser.Deserialize<List<GdaxMarket>>(s));

             if (getPricesResponse.IsSuccess)
            {
                availableMarkets.AddRange(getPricesResponse.Data.Select(m => new Market(m.Id, m.BaseCurrency, m.QuoteCurrency, m.Id, decimal.Zero, true)));

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
            var markets = await _fileService.LoadAndDeserializeAsync<List<Market>>(ChainTickerFolder.Cache, CACHE_FILE_NAME);

            // if we're restoring from the cached file the prices will be stale, reset them here
            foreach (var market in markets)
                market.ClearMidMarketPriceSnapshot();

            return markets;
        }
    }

}

