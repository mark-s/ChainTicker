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
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;
using EnsureThat;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public class PollingPriceService : IPollingPriceService
    {
        private readonly IRestService _restService;
        private readonly ISubscribableRestService<List<BitFlyerMarketDTO>> _subscribableRestService;


        private readonly string _getPricesQuery;

        private readonly Subject<MarketAndTick> _rawReceivedSubject = new Subject<MarketAndTick>();

        private readonly HashSet<string> _subscriptions = new HashSet<string>();


        public PollingPriceService(IRestService restService, string apiEndpoint, TimeSpan updateTimeSpan)
        {
            _restService = EnsureArg.IsNotNull(restService, nameof(restService));
            _getPricesQuery = new RestQueryUri(apiEndpoint, "/v1/getprices").Address();
            _subscribableRestService = new SubscribableRestService<List<BitFlyerMarketDTO>>(restService, 
                                                                                                                        _getPricesQuery,
                                                                                                                        updateTimeSpan);
            _subscribableRestService.RecievedMessagesObservable
                                                    .ObserveOn(Scheduler.Default)
                                                    .Subscribe(PopulateTickFromMarketList);

        }


        public IObservable<ITick> Subscribe(IMarket market)
        {
            StartListeningIfNeeded();

            _subscriptions.Add(market.ProductCode);

            return _rawReceivedSubject.Where(m => m.MarketId == market.ProductCode).Select(m => m.Tick).AsObservable();
        }

        public void Unubscribe(IMarket market)
        {
            _subscriptions.Remove(market.ProductCode);

            if (_subscriptions.Any() == false)
                _subscribableRestService.Unsubscribe();
        }

        public async Task<ITick> GetCurrentPriceAsync(IMarket market)
        {
            var getPricesResponse = await _restService.GetAsync<List<BitFlyerMarketDTO>>(_getPricesQuery).ConfigureAwait(false);
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


        private void StartListeningIfNeeded()
        {
            if (_subscriptions.Any() == false)
                _subscribableRestService.Subscribe();
        }

        private void PopulateTickFromMarketList(List<BitFlyerMarketDTO> bitFlyerMarkets)
        {
            foreach (var bitFlyerMarket in bitFlyerMarkets)
                _rawReceivedSubject.OnNext( new MarketAndTick(bitFlyerMarket.ProductCode , new PriceOnlyTick(bitFlyerMarket.CurrentPrice, DateTimeOffset.Now)));
        }

        
        private class MarketAndTick
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
