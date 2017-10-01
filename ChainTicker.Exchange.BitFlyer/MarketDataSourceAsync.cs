using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChainTicker.Domain;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer
{
    public class MarketDataSourceAsync : IMarketDataSource
    {
        //private readonly IPubnubTransport _pubnubTransport;
        private readonly IRestService _restService;

        public MarketDataSourceAsync(IPubnubTransport pubnubTransport, IRestService restService)
        {
            //_pubnubTransport = pubnubTransport;
            _restService = restService;
        }

        public async Task<List<IMarket>> GetAvailableMarketsAsync()
        {
            var availableMarkets = new List<IMarket>();

            // note: Using 'getprices' here as it returns nicer data than 'getmarkets'
            var result = await _restService.GetAsync<List<BitFlyerMarket>>("getprices");
            if (result.IsSuccess)
            {
                availableMarkets.AddRange(result.Data.Select(m => new Market(this, m.ProductCode, m.MainCurrency, m.SubCurrency)));
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + result.ErrorMessage);
            }

            return availableMarkets;
        }

        public async Task<ITick> GetCurrentPriceForMarketAsync(string marketId)
        {
            var result = await _restService.GetAsync<BitFlyerTick>("getticker", marketId);

            return new Tick(result.Data.LastTradedPrice, result.Data.TickTimeStamp);
        }
    }
}