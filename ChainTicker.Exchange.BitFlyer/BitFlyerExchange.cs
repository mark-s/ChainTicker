
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange, IMarketDataService
    {
        private readonly RestService _restService;
        private readonly ChainTickerJsonSerializer _serialiser;
        private PubnubTransport _pubnubTransport;
        private readonly IMarketDataService _marketDataService;

        public ExchangeInfo Info { get; } = new ExchangeInfo("BitFlyer", "https://bitflyer.jp", "BitFlyer Japan", true);

        private const string SUBSCRIBE_KEY = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f";
        private const string BASE_URL = "https://api.bitflyer.jp";


        public BitFlyerExchange()
        {
            _pubnubTransport = null;//new PubnubTransport(SUBSCRIBE_KEY, new DebugLogger());
            _serialiser = new ChainTickerJsonSerializer();
            _restService = new RestService();
            _marketDataService = new BitFlyerMarketDataService(BASE_URL, _restService, _serialiser);
        }

        public async Task<List<Market>> GetAvailableMarketsAsync()
        {
            var availableMarkets = new List<Market>();

            // note: Using 'getprices' here as it returns nicer data than 'getmarkets'
            var query = new RestQuery(BASE_URL, "/v1/getprices");

            var result = await _restService.GetAsync(query.GetAddress(), s => _serialiser.Deserialize<List<BitFlyerMarket>>(s));

            if (result.IsSuccess)
            {
                availableMarkets.AddRange(result.Data.Select(m => new Market(m.ProductCode, m.MainCurrency, m.SubCurrency)));
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + result.ErrorMessage);
            }

            return availableMarkets;
        }


        public Task<ITick> GetCurrentPriceAsync(Market market) 
            => _marketDataService.GetCurrentPriceAsync(market);
    }
}
