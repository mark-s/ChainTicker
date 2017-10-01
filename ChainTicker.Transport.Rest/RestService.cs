using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace ChainTicker.Transport.Rest
{
    public class RestService : IRestService
    {

        public async Task<Response<T>> GetAsync<T>(string restEndpointUrl, Func<string,T> deserialize)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetStringAsync(restEndpointUrl);
                    var data = deserialize(result);
                    return new Response<T>(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new Response<T>(ex.Message);
                }
            }

        }

    }
}

