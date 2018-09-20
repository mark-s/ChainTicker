using ChainTicker.DataSource.Coins.Domain;
using ChainTicker.DataSource.Coins.DTO;
using ChainTicker.Transport.Rest;
using System;
using System.Threading.Tasks;

namespace ChainTicker.DataSource.Coins.Services
{
    public class WebSource : IWebSource
    {
        private readonly IRestService _restService;

        public WebSource(IRestService restService)
        {
            _restService = restService;
        }

        public async Task GetFromWebServiceAsync(Action<AllCoinsResponse> onSuccess, Action<string> onFailure)
        {
            var endpointAddress = new RestQueryUri("https://min-api.cryptocompare.com/", "data/all/coinlist").Address();

            var response = await _restService.GetAsync<AllCoinsResponse>(endpointAddress);

            if (response.IsSuccess)
                onSuccess(response.Data);
            else
                onFailure(response.ErrorMessage);
        }

    }
}
