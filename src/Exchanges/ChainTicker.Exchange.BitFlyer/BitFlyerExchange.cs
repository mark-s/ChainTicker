using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using ChainTicker.Exchange.BitFlyer.Services;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using EnsureThat;
using System;



namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerExchange : IExchange
    {


        public ExchangeInfo Info { get; } = new ExchangeInfo("bitFlyer", "https://bitflyer.jp", "bitFlyer Japan", true,
            new ApiEndpointCollection
            {
                [ApiEndpointType.Rest] = "https://api.bitflyer.jp",
                [ApiEndpointType.Pubnub] = "sub-c-52a9ab50-29b-e5-baaa-069f8945a4f"
            });

        public MarketCollection Markets { get; private set; }

        public BitFlyerExchange(IRestService restService, IChainTickerFileService chainTickerFileService, IJsonSerializer jsonSerializer)
        {
             EnsureArg.IsNotNull(restService, nameof(restService));
            EnsureArg.IsNotNull(chainTickerFileService, nameof(chainTickerFileService));
            EnsureArg.IsNotNull(jsonSerializer, nameof(jsonSerializer));


            var marketsService = new MarketsService(Info.ApiEndpoints, restService, chainTickerFileService);

            Markets = await marketsService.GetAvailableMarketsAsync();


        }

  


    }
}
