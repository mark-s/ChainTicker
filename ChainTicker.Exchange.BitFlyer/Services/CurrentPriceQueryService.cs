using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public class CurrentPriceQueryService : ICurrentPriceQueryService
    {
        private readonly IRestService _restService;
        private readonly ISubscribableRestService<List<BitFlyerMarket>> _subscribableRestService;

        private readonly ChainTickerJsonSerializer _serialiser = new ChainTickerJsonSerializer();
        private bool _isListening;
        private readonly RestQuery _getPricesQuery;



        private readonly Subject<MarketAndTick> _rawReceivedSubject = new Subject<MarketAndTick>();

        public CurrentPriceQueryService(IRestService restService, string endpointBaseUrl, TimeSpan updateTimeSpan)
        {
            _restService = restService;
            _getPricesQuery = new RestQuery(endpointBaseUrl, "/v1/getprices");
            _subscribableRestService = new SubscribableRestService<List<BitFlyerMarket>>(restService, 
                                                                                                                        _getPricesQuery.GetAddress(),
                                                                                                                        s => _serialiser.Deserialize<List<BitFlyerMarket>>(s),
                                                                                                                        updateTimeSpan);
            _subscribableRestService.RecievedMessagesObservable
                                                    .ObserveOn(Scheduler.Default)
                                                    .Subscribe(PopulateTickFromMarketList);

        }


        public void StartListening()
        {
            if (_isListening == false)
                _subscribableRestService.Subscribe();

            _isListening = true;
        }

        public IObservable<ITick> Subscribe(Market market)
        {
            return _rawReceivedSubject.Where(m => m.MarketId == market.ProductCode).Select(m => m.Tick).AsObservable();
        }


        private void PopulateTickFromMarketList(List<BitFlyerMarket> bitFlyerMarkets)
        {
            foreach (var bitFlyerMarket in bitFlyerMarkets)
                _rawReceivedSubject.OnNext( new MarketAndTick(bitFlyerMarket.ProductCode , new PriceOnlyTick(bitFlyerMarket.CurrentPrice, DateTimeOffset.Now)));
        }



        

        public async Task<ITick> GetCurrentPriceAsync(Market market)
        {
            var getPricesResponse = await _restService.GetAsync(_getPricesQuery.GetAddress(), s => _serialiser.Deserialize<List<BitFlyerMarket>>(s)).ConfigureAwait(false);
            if (getPricesResponse.IsSuccess)
            {
                var thismarket = getPricesResponse.Data.FirstOrDefault(m => m.ProductCode == market.ProductCode);
                if (thismarket != null)
                    return new PriceOnlyTick(thismarket.CurrentPrice, DateTimeOffset.Now);
                else
                    return new EmptyTick();
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + getPricesResponse.ErrorMessage);
                return new EmptyTick();
            }
        }







        class MarketAndTick
        {
            public string MarketId { get; }

            public ITick Tick { get;  }

            public MarketAndTick(string marketId, ITick tick)
            {
                MarketId = marketId;
                Tick = tick;
            }
        }


    }



}
