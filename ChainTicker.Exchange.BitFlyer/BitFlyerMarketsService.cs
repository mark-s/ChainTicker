using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerMarketsService
    {
        private readonly ISerialize _serialiser;
        private readonly string _endpointBaseUrl;
        private readonly IRestService _restService;
        private readonly IDiskCache _cache;
        private readonly IFileIOService _fileIOService;

        private const string CACHE_FILE_NAME = "BitFlyerAvailableMarkets.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromHours(8);

        public BitFlyerMarketsService(string endpointBaseUrl,
                                                IRestService restService,
                                                ISerialize serialiser,
                                                IFileIOService fileIOService,
                                                IDiskCache cache)
        {
            _serialiser = serialiser;
            _endpointBaseUrl = endpointBaseUrl;
            _restService = restService;
            _cache = cache;
            _fileIOService = fileIOService;
        }

        public async Task<List<Market>> GetAvailableMarketsAsync()
        {
            if (_cache.IsStale(CACHE_FILE_NAME, _maxCacheAge))
                return await GetFromWebServiceAsync();
            else
                return await GetFromCacheAsync();
        }

        private async Task<List<Market>> GetFromWebServiceAsync()
        {
            var availableMarkets = new List<Market>();

            // note: Using 'getprices' here as it returns nicer data than 'getmarkets'
            var query = new RestQuery(_endpointBaseUrl, "/v1/getprices");

            var result = await _restService.GetAsync(query.GetAddress(), s => _serialiser.Deserialize<List<BitFlyerMarket>>(s));

            if (result.IsSuccess)
            {
                availableMarkets.AddRange(result.Data.Select(m => new Market(m.ProductCode, m.MainCurrency, m.SubCurrency)));
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + result.ErrorMessage);
            }

            return availableMarkets;
        }

        private async Task<List<Market>> GetFromCacheAsync()
        {
            var cachedRaw = await _fileIOService.LoadAsync(CACHE_FILE_NAME);
            return _serialiser.Deserialize<List<Market>>(cachedRaw);
        }
    }
}
