using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Rest;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;
using EnsureThat;

namespace ChainTicker.Exchange.BitFlyer.Services
{

    public class MarketsService : IMarketsService
    {

        private readonly ApiEndpointCollection _apiEndpoints;
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _fileService;
        private readonly BitFlyerMarketFactory _marketFactory;

        private const string CACHE_FILE_NAME = "BitFlyerAvailableMarkets.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromHours(3);

        public MarketsService(ApiEndpointCollection apiEndpoints,
                                                IRestService restService,
                                                IChainTickerFileService fileService,
                                                BitFlyerMarketFactory marketFactory)
        {
            _apiEndpoints = EnsureArg.IsNotNull(apiEndpoints, nameof(apiEndpoints));
            _restService = EnsureArg.IsNotNull(restService, nameof(restService));
            _fileService = EnsureArg.IsNotNull(fileService, nameof(fileService));
            _marketFactory = EnsureArg.IsNotNull(marketFactory, nameof(marketFactory));
        }

        public Task<List<IMarket>> GetAvailableMarketsAsync()
        {
            if (_fileService.IsCacheStale(new CachedFile(CACHE_FILE_NAME, _maxCacheAge)))
                return GetFromWebServiceAsync();
            else
                return GetFromCacheAsync();
        }

        private async Task<List<IMarket>> GetFromWebServiceAsync()
        {
            var availableMarkets = new List<IMarket>();

            // note: Using 'getprices' here as it returns nicer data than 'getmarkets'
            var getPricesQuery = new RestQuery(_apiEndpoints[ApiEndpointType.Rest], "/v1/getprices");
            var getPricesResponse = await _restService.GetAsync<List<BitFlyerMarket>>(getPricesQuery.Address());

            // but it returns All markets - even ones that can't be subscribed to... So need to flag them
            var getMarketsQuery = new RestQuery(_apiEndpoints[ApiEndpointType.Rest], "/v1/getmarkets");
            var getMarketsResponse = await _restService.GetAsync<List<BitFlyerMarket>>(getMarketsQuery.Address());

            if (getPricesResponse.IsSuccess && getMarketsResponse.IsSuccess)
            {
                var marketsWithLivePrices = getMarketsResponse.Data;
                availableMarkets.AddRange(getPricesResponse.Data.Select(m => _marketFactory.GetMarket(m, marketsWithLivePrices.Any(p => p.ProductCode == m.ProductCode))));

                await _fileService.SaveAndSerializeAsync(ChainTickerFolder.Cache, CACHE_FILE_NAME, availableMarkets);
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

            var fromCache = await _fileService.LoadAndDeserializeAsync<List<CachedMarket>>(ChainTickerFolder.Cache, CACHE_FILE_NAME);

            foreach (var cachedMarket in fromCache)
                toReturn.Add(_marketFactory.GetMarket(cachedMarket));

            return toReturn;
        }
    }
}
