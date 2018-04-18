using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;


namespace ChainTicker.Transport.Rest
{
    public class RestService : IRestService
    {
        private readonly RandomUserAgentService _uaService;
        private readonly IJsonSerializer _jsonSerialiser;

        public RestService(RandomUserAgentService randomUserAgentService, IJsonSerializer jsonSerialiser)
        {
            _uaService = randomUserAgentService;
            _jsonSerialiser = jsonSerialiser;
        }

        public async Task<Response<T>> GetAsync<T>(string restQueryAddress)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    Debug.WriteLine("GET: From rest endpoint: " + restQueryAddress);

                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", _uaService.GetUserAgent());
                    var result = await client.GetStringAsync(restQueryAddress);

                    var data = _jsonSerialiser.Deserialize<T>(result);
                    return Response<T>.Success(data);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return Response<T>.Failure(ex.Message);
                }
            }

        }


    }
}

