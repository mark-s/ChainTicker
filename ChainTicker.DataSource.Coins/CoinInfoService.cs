using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Domain;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.DataSource.Coins
{
    public class CoinInfoService : ICoinInfoService
    {
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _fileService;
        private readonly ISerialize _serializer;

        private const string CACHE_FILE_NAME = "coins.json";
        private readonly TimeSpan _maxCacheAge = TimeSpan.FromDays(5);

        private CoinDataBase _coinData = new CoinDataUnavailable();

        public CoinInfoService(IRestService restService,IChainTickerFileService fileService)
        {
            _restService = restService;
            _fileService = fileService;

            _serializer = new ChainTickerJsonSerializer();
        }

        public async Task PopulateAvailableCoinsAsync()
        {
            if (_fileService.IsCacheStale(new CachedFile(CACHE_FILE_NAME, _maxCacheAge)))
                _coinData = await GetFromWebServiceAsync();
            else
                _coinData = await GetFromCacheAsync();
        }

        public IEnumerable<ICoin> GetAllCoins()
            => _coinData.GetAllCoins();

        public IEnumerable<string> GetAllCoinCodes() 
            => _coinData.GetAllCoinCodes();

        public ICoin GetCoinInfo(string coinCode) 
            => _coinData.GetCoinInfo(coinCode);


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
            var cachedAllCoinsResponse = await _fileService.LoadAndDeserializeAsync<AllCoinsResponse>(ChainTickerFolder.Cache, CACHE_FILE_NAME);
            return Parse(cachedAllCoinsResponse);
        }


        private async Task<CoinData> SaveToCacheAndParse(AllCoinsResponse response)
        {
            // Save to the cache so we don't need to do this expensive call all the time
            await _fileService.SaveAndSerializeAsync(ChainTickerFolder.Cache, CACHE_FILE_NAME,response).ConfigureAwait(false); 
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
