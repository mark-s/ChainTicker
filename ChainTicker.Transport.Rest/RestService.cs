using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;


namespace ChainTicker.Transport.Rest
{
    public class RestService : IRestService
    {
        public async Task<Response<T>> GetAsync<T>(string restQueryAddress, Func<string,T> deserialize)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    Debug.WriteLine("GET: From rest endpoint: " + restQueryAddress);

                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:24.0) Gecko/20100101 Firefox/24.0");
                    var result = await client.GetStringAsync(restQueryAddress);

                    var data = deserialize(result);
                    return new Response<T>(data);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return new Response<T>(ex.Message);
                }
            }

        }


    }
}

