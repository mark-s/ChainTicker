using ChainTicker.Core.Interfaces;
using ChainTicker.DataSource.Coins.Domain;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.DataSource.Coins.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChainTicker.DataSource.Coins
{

    // This is the main API for accessing Information about cryptocurrency 'coins'
    public class CoinsService : ICoinsService
    {
        private readonly IWebSource _webSource;
        private readonly ICacheSource _cacheSource;

        private CoinsCollection _coinsCollection;


        public CoinsService(IWebSource webSource, ICacheSource cacheSource)
        {
            _webSource = webSource;
            _cacheSource = cacheSource;
        }

        
        public async Task<IEnumerable<ICoin>> GetAllCoinsAsync()
        {
            if (_coinsCollection == null)
                await PopulateAvailableCoinsAsync();

            return _coinsCollection.GetAllCoins();
        }

        public async Task<IEnumerable<string>> GetAllCoinCodesAsync()
        {
            if (_coinsCollection == null)
                await PopulateAvailableCoinsAsync();

            return _coinsCollection.GetAllCoinCodes();
        }

        public async Task<ICoin> GetCoinInfoAsync(string coinCode)
        {
            if (_coinsCollection == null)
                await PopulateAvailableCoinsAsync();

            return _coinsCollection.GetCoin(coinCode);
        }
        

        private async Task PopulateAvailableCoinsAsync()
        {
            if (_cacheSource.IsCacheStale())
                await _webSource.GetFromWebServiceAsync(async response => await WebSuccess(response), WebFailure);
            else
                await _cacheSource.GetFromCacheAsync(CacheSuccess, CacheFailure);

        }

        private void CacheFailure(string errorMessage)
        {
            // TODO: this needs work - error handling / retry etc.
            Debug.WriteLine(errorMessage);
        }

        private void CacheSuccess(AllCoinsResponse cachedData)
        {
            _coinsCollection = ConvertAllCoinsResponse.ToCoinsCollection(cachedData);
        }

        private void WebFailure(string errorMessage)
        {
            // TODO: this needs work - error handling / retry etc.
            Debug.WriteLine(errorMessage);
        }

        private async Task WebSuccess(AllCoinsResponse response)
        {
            // Save to the cache so we don't need to do this expensive call all the time
            await _cacheSource.UpdateCachedDataAsync(response);
            _coinsCollection = ConvertAllCoinsResponse.ToCoinsCollection(response); ;
        }


    }
}
