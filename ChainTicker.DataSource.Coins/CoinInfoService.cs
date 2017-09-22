using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using ChainTicker.DataSource.Coins.Domain;
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


        public async Task<CoinData> GetAllCoinsAsync()
        {
            var response =  await _restService.GetAsync<AllCoinsResponse>(REST_BASE_URI, "/data/coinlist");

            if (response.StatusCode == HttpStatusCode.OK)
                return HandleOk(response);
            else
                return HandleError(response);
        }




        private CoinData HandleError(IRestResponse<AllCoinsResponse> response)
        {
            Debug.WriteLine(response.ErrorMessage);

            // TODO: pull this from a cached version if available
            return null;
        }

        private CoinData HandleOk(IRestResponse<AllCoinsResponse> response)
        {
            // TODO: save response to disk
            return new CoinData(response.Data);
        }



    }
}
