using System.Threading.Tasks;
using RestSharp;

namespace ChainTicker.Transport.Rest
{
    public interface IRestService
    {
        Task<IRestResponse<T>> GetAsync<T>(string commandName);

        Task<IRestResponse<T>> GetAsync<T>(string commandName, string commandArgs);

    }
}