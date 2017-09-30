using System;
using System.Net.Http;
using System.Threading.Tasks;
using ChanTicker.Core.IO;

namespace ChainTicker.Transport.Rest
{
    public class RestService : IRestService
    {
        private ServiceCommands _availableCommands;
        private readonly ISerialize _serializer;

        public RestService(ISerialize serializer)
        {
            _serializer = serializer;
        }

        public async Task<IResponse<T>> GetAsync<T>(string commandName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var restEndpointUrl = _availableCommands.GetCommandUri(commandName);
                    var result = await client.GetStringAsync(restEndpointUrl);
                    var data = _serializer.Deserialize<T>(result);
                    return new Response<T>(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new Response<T>(ex.Message);
                }
            }

        }

        public async Task<IResponse<T>> GetAsync<T>(string commandName, string commandArgs)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    var restEndpointUrl = _availableCommands.GetCommandUri(commandName, commandArgs);
                    var result = await client.GetStringAsync(restEndpointUrl);
                    var data = _serializer.Deserialize<T>(result);
                    return new Response<T>(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new Response<T>(ex.Message);
                }
            }

        }

        public void RegisterCommands(ServiceCommands commands)
        {
            _availableCommands = commands;
        }
    }
}

