using System.Threading.Tasks;
using RestSharp;

namespace ChainTicker.Transport.Rest
{
    public class RestService : IRestService
    {
        private readonly ServiceCommands _availableCommands;

        public RestService(ServiceCommands availableCommands)
        {
            _availableCommands = availableCommands;
        }

        public async Task<IRestResponse<T>> GetAsync<T>(string commandName)
        {
            var restClient = new RestClient();
            var request = new RestRequest(_availableCommands.GetCommandUri(commandName));

            return await restClient.ExecuteTaskAsync<T>(request);
        }

        public async Task<IRestResponse<T>> GetAsync<T>(string commandName, string commandArgs)
        {
            var restClient = new RestClient();
            var request = new RestRequest(_availableCommands.GetCommandUri(commandName, commandArgs));

            return await restClient.ExecuteTaskAsync<T>(request);
        }

    }
}
