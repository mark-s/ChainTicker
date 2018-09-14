using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Domain;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.Transport.Rest;
using ChainTicker.Core.Interfaces;
using ChainTicker.Core.IO;

namespace ChainTicker.DataSource.Coins
{

    // This is the main API for accessing Information about cryptocurrency 'coins'
    public class CoinInfoService : ICoinInfoService
    {
        private readonly IRestService _restService;
        private readonly IChainTickerFileService _fileService;

        private Domain.CoinsCollection _coinsCollection;

        private readonly CachedFile _cacheFile = new CachedFile("coins.json", TimeSpan.FromDays(5));


        public CoinInfoService(IRestService restService, IChainTickerFileService fileService)
        {
            _restService = restService;
            _fileService = fileService;
        }

        public async Task GetAvailableCoinsAsync()
        {
            if (_fileService.IsCacheStale(_cacheFile))
                _coinsCollection = await GetFromWebServiceAsync();
            else
                _coinsCollection = await GetFromCacheAsync();

        }

        public IEnumerable<ICoin> GetAllCoins()
            => _coinsCollection.GetAllCoins();

        public IEnumerable<string> GetAllCoinCodes()
            => _coinsCollection.GetAllCoinCodes();

        public ICoin GetCoinInfo(string coinCode)
            => _coinsCollection.GetCoin(coinCode);


        private async Task<Domain.CoinsCollection> GetFromWebServiceAsync()
        {
            var endpointAddress = new RestQuery("https://min-api.cryptocompare.com/", "data/all/coinlist").Address();

            var response = await _restService.GetAsync<AllCoinsResponse>(endpointAddress);

            if (response.IsSuccess)
                return await HandleSuccessAsync(response.Data);
            else
                return await HandleErrorAsync(response);
        }

        private async Task<Domain.CoinsCollection> GetFromCacheAsync()
        {
            var cachedAllCoinsResponse = await _fileService.LoadAndDeserializeAsync<AllCoinsResponse>(ChainTickerFolder.Cache, _cacheFile.FileName);
            return ConvertAllCoinsResponse.ToCoinsCollection(cachedAllCoinsResponse);
        }


        private async Task<Domain.CoinsCollection> HandleSuccessAsync(AllCoinsResponse response)
        {
            // Save to the cache so we don't need to do this expensive call all the time
            await _fileService.SaveAndSerializeAsync(ChainTickerFolder.Cache, _cacheFile.FileName, response).ConfigureAwait(false);
            return ConvertAllCoinsResponse.ToCoinsCollection(response);
        }

        private async Task<Domain.CoinsCollection> HandleErrorAsync(Response<AllCoinsResponse> response)
        {
            Debug.WriteLine(response.ErrorMessage);
            return await GetFromCacheAsync();
        }

    }
}
