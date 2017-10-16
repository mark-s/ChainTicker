using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerMarketsService
    {
        private readonly ISerialize _serialiser;
        private readonly string _endpointBaseUrl;
        private readonly IRestService _restService;

        public BitFlyerMarketsService(string endpointBaseUrl,
                                                IRestService restService)
        {
            _serialiser = new ChainTickerJsonSerializer();
            _endpointBaseUrl = endpointBaseUrl;
            _restService = restService;
        }

        public async Task<List<Market>> GetAvailableMarketsAsync()
        {
            return await GetFromWebServiceAsync();
        }

        private async Task<List<Market>> GetFromWebServiceAsync()
        {
            var availableMarkets = new List<Market>();

            // note: Using 'getprices' here as it returns nicer data than 'getmarkets'
            var getPricesQuery = new RestQuery(_endpointBaseUrl, "/v1/getprices");
            var getPricesResponse = await _restService.GetAsync(getPricesQuery.GetAddress(), s => _serialiser.Deserialize<List<BitFlyerMarket>>(s));

            // but it returns All markets - even ones that can't be subscribed to... So need to flag them
            var getMarketsQuery = new RestQuery(_endpointBaseUrl, "/v1/getmarkets");
            var getMarketsResponse = await _restService.GetAsync(getMarketsQuery.GetAddress(), s => _serialiser.Deserialize<List<PlainMarket>>(s));

            if (getPricesResponse.IsSuccess && getMarketsResponse.IsSuccess)
            {
                var marketsWithLivePrices = getMarketsResponse.Data;
                availableMarkets.AddRange(getPricesResponse.Data.Select(m => new Market(m.ProductCode, m.MainCurrency, m.SubCurrency, m.ProductCode, m.CurrentPrice, marketsWithLivePrices.Any(p => p.ProductCode == m.ProductCode))));
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + getPricesResponse.ErrorMessage);
            }

            return availableMarkets;
        }

    }
}
