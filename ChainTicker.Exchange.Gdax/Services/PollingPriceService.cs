using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.Exchange.Gdax.DTO.Responses;
using ChainTicker.Transport.Rest;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Exchange.Gdax.Services
{
    public class PollingPriceService : IPollingPriceService
    {
        private readonly IRestService _restService;
        private readonly ApiEndpointCollection _infoApiEndpoints;
        private readonly IJsonSerializer _jsonSerialiser;

        public PollingPriceService(IRestService restService, ApiEndpointCollection infoApiEndpoints, IJsonSerializer jsonSerialiser)
        {
            _restService = restService;
            _infoApiEndpoints = infoApiEndpoints;
            _jsonSerialiser = jsonSerialiser;
        }


        public async Task<ITick> GetCurrentPriceAsync(Market market)
        {
            var productPriceResponse = await _restService.GetAsync(GetProductTickerUrl(market.ProductCode),
                                                                   s => _jsonSerialiser.Deserialize<GdaxNonRealtimeTick>(s)).ConfigureAwait(false);

            if (productPriceResponse.IsSuccess)
            {
                var tick = productPriceResponse.Data;
                return new Tick(tick.Price, tick.TimeStamp, tick.Ask, tick.Bid, tick.Volume);
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get price! " + productPriceResponse.ErrorMessage);
                return new EmptyTick();
            }
        }


        private string GetProductTickerUrl(string productId)
        {
            var uriBuilder = new UriBuilder(_infoApiEndpoints[ApiEndpointType.Rest]) {Path = $"/products/{productId}/ticker"};
            return uriBuilder.Uri.ToString();
        }



        public IObservable<ITick> Subscribe(Market market) 
            => throw new InvalidOperationException("All GDAX subscriptions are realtime - prefer realtime subscriptions");

        public void Unubscribe(Market market)
            => throw new InvalidOperationException("All GDAX subscriptions are realtime - prefer realtime subscriptions");
    }
}