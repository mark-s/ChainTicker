using System.Collections.Generic;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.Services;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;


namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {
        private readonly BitFlyerMarketsService _bitFlyerMarketsService;

        public List<Market> Markets { get; private set; }

        public ExchangeInfo Info => Config.Info;

        public BitFlyerExchange(IRestService restService, IChainTickerFileService chainTickerFileService, ISerialize jsonSerializer, BitFlyerPriceDataService priceDataService)
        {
            _bitFlyerMarketsService = new BitFlyerMarketsService(Info.ApiEndpoints, restService, chainTickerFileService, jsonSerializer, priceDataService);
        }


        public async Task PopulateAvailableMarketsAsync()
            => Markets = await _bitFlyerMarketsService.GetAvailableMarketsAsync();

    }
}
