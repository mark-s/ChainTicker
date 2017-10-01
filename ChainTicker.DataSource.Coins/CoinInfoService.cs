using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Domain;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.IO;

namespace ChainTicker.DataSource.Coins
{
    public class CoinInfoService : ICoinInfoService
    {
        private readonly IRestService _restService;
        private readonly IDiskCache _cache;
        private readonly IFileIOService _fileIOService;
        private readonly ISerialize _serializer;

        private const string CACHE_FILE_NAME = "coins.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromDays(5);

        public CoinInfoService(IRestService restService,
                                      IDiskCache cache,
                                      IFileIOService fileIOService,
                                      ISerialize serializer)
        {
            _restService = restService;
            _cache = cache;
            _fileIOService = fileIOService;
            _serializer = serializer;
        }

        public async Task<CoinData> GetAllCoinsAsync()
        {
            if (_cache.IsStale(CACHE_FILE_NAME, _maxCacheAge))
                return await GetFromWebServiceAsync();
            else
                return await GetFromCacheAsync();
        }


        private async Task<CoinData> GetFromWebServiceAsync()
        {
            var query = new RestQuery("https://www.cryptocompare.com/api", "/data/coinlist").GetAddress();

            var response = await _restService.GetAsync(query, s => _serializer.Deserialize<AllCoinsResponse>(s));

            if (response.IsSuccess)
                return await SaveToCacheAndParse(response.Data);
            else
                return await HandleErrorAsync(response);
        }

        private async Task<CoinData> GetFromCacheAsync()
        {
            var rawResponseText = await _fileIOService.LoadAsync(CACHE_FILE_NAME).ConfigureAwait(false); ;
            var cachedAllCoinsResponse = _serializer.Deserialize<AllCoinsResponse>(rawResponseText);

            return Parse(cachedAllCoinsResponse);
        }


        private async Task<CoinData> SaveToCacheAndParse(AllCoinsResponse response)
        {
            // Save to the cache so we don't need to do this expensive call all the time

            await _fileIOService.SaveAsync(CACHE_FILE_NAME, _serializer.Serialize(response)).ConfigureAwait(false); ;

            return Parse(response);
        }

        private async Task<CoinData> HandleErrorAsync(Response<AllCoinsResponse> response)
        {
            Debug.WriteLine(response.ErrorMessage);
            return await GetFromCacheAsync().ConfigureAwait(false); ;
        }


        private CoinData Parse(AllCoinsResponse allCoinsResponse)
            => new CoinData(allCoinsResponse);

    }
}
