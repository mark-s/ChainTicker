using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace ChainTicker.DataSource.Coins
{

    public static class RestClientExtensions
    {
        public static Task<IRestResponse> ExecuteTaskAsync(this IRestClient restClient, IRestRequest request)
        {
            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));

            var tcs = new TaskCompletionSource<IRestResponse>();

            restClient.ExecuteAsync(request, (response) =>
            {
                if (response.ErrorException != null)
                    tcs.TrySetException(response.ErrorException);
                else
                    tcs.TrySetResult(response);
            });

            return tcs.Task;
        }
    }

    public class CoinService
    {


        public async Task<Dictionary<string, CurrencyDefinition>> GetAllAvailableCoinsAsync(string restEndpointUrl, Action onFailureCallback)
        {
            var client = new RestClient("https://www.cryptocompare.com/api");
            var request = new RestRequest(restEndpointUrl);
            //var request = new RestRequest("/data/coinlist");

            var response = await client.ExecuteTaskAsync<AllCoins>(request);

            if (response.ErrorException != null)
            {
                onFailureCallback();
                return new Dictionary<string, CurrencyDefinition>(0);
            }
            else
                return response.Data.Data;


        }





    }


    public class AllCoins
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public string BaseImageUrl { get; set; }
        public string BaseLinkUrl { get; set; }

        public int Type { get; set; }

        public Dictionary<string, CurrencyDefinition> Data { get; set; }
    }

    public class CurrencyDefinition
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string CoinName { get; set; }
        public string FullName { get; set; }
        public string Algorithm { get; set; }
        public string ProofType { get; set; }
        public string FullyPremined { get; set; }
        public string TotalCoinSupply { get; set; }
        public string PreMinedValue { get; set; }
        public string TotalCoinsFreeFloat { get; set; }
        public string SortOrder { get; set; }
    }

}
