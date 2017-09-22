using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.DataSource.Coins.Rest;
using RestSharp;

namespace ChainTicker.DataSource.Coins
{
    public class CoinInfoService
    {
        private readonly IRestService _restService;

        private const string REST_BASE_URI = "https://www.cryptocompare.com/api";

        public CoinInfoService(IRestService restService)
        {
            _restService = restService;
        }


        public async Task<Dictionary<string, CoinInfo>> GetAllCoinsAsync()
        {
            var response =  await _restService.GetAsync<AllCoins>(REST_BASE_URI, "/data/coinlist");

            if (response.StatusCode == HttpStatusCode.OK)
                return HandleOk(response);
            else
                return HandleError(response);
        }


        public Uri GetImageUri(CoinInfo coinInfo)
            => new Uri(_baseImageUrl + coinInfo.ImageUrl);

        public Uri GetInfoUri(CoinInfo coinInfo)
            => new Uri(_baseLinkUrl + coinInfo.Url);


        private Dictionary<string, CoinInfo> HandleError(IRestResponse<AllCoins> response)
        {
            Debug.WriteLine(response.ErrorMessage);

            // TODO: pull this from a cached version if available
            return new Dictionary<string, CoinInfo>(0);
        }

        private Dictionary<string, CoinInfo> HandleOk(IRestResponse<AllCoins> response)
        {
            PopulateSessionInfo(response.Data);

            // TODO: save response to disk
            return response.Data.Coins;
        }

        private void PopulateSessionInfo(AllCoins responseData)
        {
            _baseImageUrl = responseData.BaseImageUrl;
            _baseLinkUrl = responseData.BaseLinkUrl;
        }


    }
}
