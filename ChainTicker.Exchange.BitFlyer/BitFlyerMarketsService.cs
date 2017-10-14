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

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerMarketsService
    {
        private readonly ISerialize _serialiser;
        private readonly IChainTickerFileService _fileService;
        private readonly string _endpointBaseUrl;
        private readonly IRestService _restService;

        private const string CACHE_FILE_NAME = "BitFlyerAvailableMarkets.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromHours(8);

        public BitFlyerMarketsService(string endpointBaseUrl,
                                                IRestService restService,
                                                IChainTickerFileService fileService)
        {
            _serialiser = new ChainTickerJsonSerializer();
            _fileService = fileService;
            _endpointBaseUrl = endpointBaseUrl;
            _restService = restService;
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
            var query = new RestQuery(_endpointBaseUrl, "/v1/getprices");

            var result = await _restService.GetAsync(query.GetAddress(), s => _serialiser.Deserialize<List<BitFlyerMarket>>(s));

            if (result.IsSuccess)
            {
                availableMarkets.AddRange(result.Data.Select(m => new Market(m.ProductCode, m.MainCurrency, m.SubCurrency, m.ProductCode)));
                
                // save to cache
                await _fileService.SaveAndSerializeAsync(ChainTickerFolder.Cache, CACHE_FILE_NAME, availableMarkets);
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + result.ErrorMessage);
            }

            return availableMarkets;
        }

        private Task<List<Market>> GetFromCacheAsync()
            => _fileService.LoadAndDeserializeAsync<List<Market>>(ChainTickerFolder.Cache, CACHE_FILE_NAME);
    }
}
