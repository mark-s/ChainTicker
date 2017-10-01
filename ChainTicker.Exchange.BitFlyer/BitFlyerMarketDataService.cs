using System.Diagnostics;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerMarketDataService : IMarketDataService
    {
        private readonly string _baseUrl;
        private readonly IRestService _restService;
        private readonly ISerialize _serializer;

        public BitFlyerMarketDataService(string baseUrl, IRestService restService)
        {
            _baseUrl = baseUrl;
            _restService = restService;
            _serializer = new ChainTickerJsonSerializer();
        }

        public async Task<ITick> GetCurrentPriceAsync(Market market)
        {
            var query = new RestQuery(_baseUrl, "/v1/getticker", "product_code");

            var result = await _restService.GetAsync(query.GetAddress(market.Id), s => _serializer.Deserialize<BitFlyerTick>(s));

            if (result.IsSuccess)
            {
                return new Tick(result.Data.LastTradedPrice, result.Data.TickTimeStamp);
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + result.ErrorMessage);
                return new EmptyTick();
            }

        }
    }
}