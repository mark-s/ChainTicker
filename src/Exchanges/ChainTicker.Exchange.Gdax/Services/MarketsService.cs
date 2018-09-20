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

        private readonly ApiEndpointCollection _apiEndpoints;
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _fileService;
        private readonly GdaxMarketFactory _marketFactory;

        private const string CACHE_FILE_NAME = "GdaxAvailableMarkets.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromHours(3);

        public MarketsService(ApiEndpointCollection apiEndpoints,
                                                IRestService restService,
                                                IChainTickerFileService fileService,
                                                GdaxMarketFactory marketFactory)
        {
            _apiEndpoints = apiEndpoints;
            _restService = restService;
            _fileService = fileService;
            _marketFactory = marketFactory;
        }

        public async Task<List<IMarket>> GetAvailableMarketsAsync()
        {
            if (_fileService.IsCacheStale(new CachedFile(CACHE_FILE_NAME, _maxCacheAge)))
                return await GetFromWebServiceAsync();
            else
                return await GetFromCacheAsync();
        }

        private async Task<List<IMarket>> GetFromWebServiceAsync()
        {
            var availableMarkets = new List<IMarket>();

            var getPricesQuery = new RestQueryUri(_apiEndpoints[ApiEndpointType.Rest], "/products");
            var getPricesResponse = await _restService.GetAsync<List<GdaxMarket>>(getPricesQuery.Address());

            if (getPricesResponse.IsSuccess)
            {
                availableMarkets.AddRange(getPricesResponse.Data.Select(m =>
                    _marketFactory.GetMarket(m.Id, m.BaseCurrency, m.QuoteCurrency, m.Id, true)

                ));

                await _fileService.SaveAndSerializeAsync(AppFolder.Cache, CACHE_FILE_NAME, availableMarkets);
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + getPricesResponse.ErrorMessage);
            }

            return availableMarkets;
        }

        private async Task<List<IMarket>> GetFromCacheAsync()
        {
            var toReturn = new List<IMarket>();

            var fromCache = await _fileService.LoadAndDeserializeAsync<List<CachedMarket>>(AppFolder.Cache, CACHE_FILE_NAME);

            foreach (var cachedMarket in fromCache)
            {
                toReturn.Add(_marketFactory.GetMarket(cachedMarket));
            }

            return toReturn;
        }
    }
}

