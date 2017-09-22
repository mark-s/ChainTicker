using System;
using System.Threading.Tasks;
using RestSharp;

namespace ChainTicker.DataSource.Coins.Rest
{
    public static class RestClientExtensions
    {
        public static Task<IRestResponse> ExecuteTaskAsync(this IRestClient restClient, IRestRequest request)
        {
            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

            restClient.ExecuteAsync(request, response =>
            {
                if (response.ErrorException != null)
                    taskCompletionSource.TrySetException(response.ErrorException);
                else
                    taskCompletionSource.TrySetResult(response);
            });

            return taskCompletionSource.Task;
        }
    }
}