using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Domain;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.DataSource.Coins.Rest;
using ChanTicker.Core.IO;
using RestSharp;

namespace ChainTicker.DataSource.Coins
{
    public class CoinInfoService
    {
        private readonly IRestService _restService;
        private readonly IFileIOService _fileIOService;

        private const string REST_BASE_URI = "https://www.cryptocompare.com/api";
        private const string DATA_FILE = "coins.json";

        private const int MAX_CACHE_AGE_DAYS = 7;

        public CoinInfoService(IRestService restService, IFileIOService fileIOService)
        {
            _restService = restService;
            _fileIOService = fileIOService;
        }

        public async Task<CoinData> GetAllCoinsAsync()
        {
            if (ShouldLoadFromCache(MAX_CACHE_AGE_DAYS))
                return await LoadFromCacheAsync();
            else
                return await GetFromWebServiceOrCacheOnFailureAsync();
        }



        private async Task<CoinData> GetFromWebServiceOrCacheOnFailureAsync()
        {
            var response = await _restService.GetAsync<AllCoinsResponse>(REST_BASE_URI, "/data/coinlist");

            if (response.StatusCode == HttpStatusCode.OK)
                return await HandleOkAsync(response);
            else
                return await HandleErrorAsync(response);
        }

        private bool ShouldLoadFromCache(int maxAgeDays)
        {
            // check there's actually some saved data to load from 
            if (_fileIOService.FileExists(DATA_FILE) == false)
                return false;

            var maxAge = DateTime.Today.AddDays(-maxAgeDays);
            var savedDate = _fileIOService.GetFileSaveDate(DATA_FILE);

            return DateTime.Compare(savedDate, maxAge) > 0;
        }

        private async Task<CoinData> HandleOkAsync(IRestResponse<AllCoinsResponse> response)
        {
            await _fileIOService.SaveAsync(DATA_FILE, response.Data);

            return Parse(response.Data);
        }

        private async Task<CoinData> HandleErrorAsync(IRestResponse<AllCoinsResponse> response)
        {
            Debug.WriteLine(response.ErrorMessage);

            if (_fileIOService.FileExists(DATA_FILE))
            {
                return await LoadFromCacheAsync();
            }
            else
                return null;
        }

        private async Task<CoinData> LoadFromCacheAsync()
        {
            var fromDisk = await _fileIOService.LoadAsync<AllCoinsResponse>(DATA_FILE);
            return Parse(fromDisk);
        }

        private CoinData Parse(AllCoinsResponse allCoinsResponse)
            => new CoinData(allCoinsResponse);

    }
}
