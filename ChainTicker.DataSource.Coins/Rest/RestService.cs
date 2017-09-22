using System.Threading.Tasks;
using RestSharp;

namespace ChainTicker.DataSource.Coins.Rest
{
    public class RestService : IRestService
    {


        public async Task<IRestResponse<T>> GetAsync<T>(string endpointBaseUri, string restEndpointUrl)
        {
            var restClient = new RestClient(endpointBaseUri);
            var request = new RestRequest(restEndpointUrl);

            return await restClient.ExecuteTaskAsync<T>(request);

        }


    }
}
