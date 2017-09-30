using System;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerCoinPriceDataSource : ICoinPriceDataSource
    {
        private readonly IPubnubTransport _pubnubTransport;
        private readonly IRestService _restService;

        public BitFlyerCoinPriceDataSource(IPubnubTransport pubnubTransport, IRestService restService)
        {
            _pubnubTransport = pubnubTransport;
            _restService = restService;
        }

        public async Task<ICoinPair[]> GetAvailableCoinPairsAsync()
        {
            var result = await  _restService.GetAsync<BitFlyerMarket>("getmarkets");
        }

        public Task<ICoinPairPrice> GetCurrentPriceAsync(ICoinPair coinPair)
        {
            return TODO_IMPLEMENT_ME;
        }

        public Task<IObservable<ICoinPairPrice>> SubscribeToPriceTicker(ICoinPair coinPair)
        {
            return TODO_IMPLEMENT_ME;
        }
    }
}