using System.Threading.Tasks;
using RestSharp;

namespace ChainTicker.DataSource.Coins.Rest
{
    public interface IRestService
    {
        Task<IRestResponse<T>> GetAsync<T>(string endpointBaseUri, string restEndpointUrl);
    }
}