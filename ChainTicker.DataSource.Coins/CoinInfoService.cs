using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Domain;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.DataSource.Coins.Rest;
using RestSharp;

namespace ChainTicker.DataSource.Coins
{
    public class CoinInfoService : ICoinInfoService
    {
        private readonly IRestService _restService;
        private readonly CoinInfoServiceConfig _config;
        private readonly ICoinInfoCacheService _cache;


        public CoinInfoService(IRestService restService,
                                      CoinInfoServiceConfig config,
                                      ICoinInfoCacheService cacheService)
        {
            _restService = restService;
            _config = config;
            _cache = cacheService;
        }

        public async Task<CoinData> GetAllCoinsAsync()
        {
            if (_cache.IsStale(_config))
            {
                var response = await _restService.GetAsync<AllCoinsResponse>(_config.RestBaseUri, "/data/coinlist");

                if (response.StatusCode == HttpStatusCode.OK)
                    return await HandleOkAsync(response.Data);
                else
                    return await HandleErrorAsync(response);
             
            }
            else
                return await GetFromCacheAsync();
        }


        private async Task<CoinData> HandleOkAsync(AllCoinsResponse response)
        {
            // Save to the cache so we don't need to do this expensive call all the time
            await _cache.SaveAsync(_config.CacheFileName, response);

            return Parse(response);
        }

        private async Task<CoinData> HandleErrorAsync(IRestResponse response)
        {
            Debug.WriteLine(response.ErrorMessage);
            return await GetFromCacheAsync();
        }

        private async Task<CoinData> GetFromCacheAsync()
        {
            var cachedData = await _cache.LoadAsync<AllCoinsResponse>(_config.CacheFileName);
            return Parse(cachedData);
        }

        private CoinData Parse(AllCoinsResponse allCoinsResponse)
            => new CoinData(allCoinsResponse);

    }
}
