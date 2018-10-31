using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Exchange.BitFlyer.Converters;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Rest;
using EnsureThat;

namespace ChainTicker.Exchange.BitFlyer.Services
{

    public class MarketsService
    {
        private readonly ApiEndpointCollection _apiEndpoints;
        private readonly IRestService _restService;
        private readonly IMarketsServiceCache _cache;

        public MarketsService(ApiEndpointCollection apiEndpoints,
                                      IRestService restService,
                                      IMarketsServiceCache cache)
        {
            _apiEndpoints = EnsureArg.IsNotNull(apiEndpoints, nameof(apiEndpoints));
            _restService = EnsureArg.IsNotNull(restService, nameof(restService));
            _cache = EnsureArg.IsNotNull(cache, nameof(cache));
        }

        public Task<MarketCollection> GetAvailableMarketsAsync()
        {
            if (_cache.IsCacheStale())
                return GetFromWebServiceAsync();
            else
                return _cache.ReadCacheAsync();
        }

        private async Task<MarketCollection> GetFromWebServiceAsync()
        {
            var availableMarkets = new List<IMarket>();

            // note: Using 'getprices' here as it returns nicer data than 'getmarkets'
            var getPricesQuery = new RestQueryUri(_apiEndpoints[ApiEndpointType.Rest], "/v1/getprices");
            var getPricesResponse = await _restService.GetAsync<List<BitFlyerMarketDTO>>(getPricesQuery.Address());

            // but it returns All markets - even ones that can't be subscribed to... So need to flag them
            var getMarketsQuery = new RestQueryUri(_apiEndpoints[ApiEndpointType.Rest], "/v1/getmarkets");
            var getMarketsResponse = await _restService.GetAsync<List<BitFlyerMarketDTO>>(getMarketsQuery.Address());

            if (getPricesResponse.IsSuccess && getMarketsResponse.IsSuccess)
            {
                var marketsWithLivePrices = getMarketsResponse.Data;

                availableMarkets.AddRange(getPricesResponse.Data.Select(m => Helpers.ConvertToMarket(m, marketsWithLivePrices.Any(p => p.ProductCode == m.ProductCode))));

                var marketCollection = new MarketCollection(availableMarkets);

                await _cache.WriteCacheAsync(marketCollection);

                return marketCollection;
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + getPricesResponse.ErrorMessage);
                return new MarketCollection(new List<IMarket>(0));
            }

        }

    }
}
