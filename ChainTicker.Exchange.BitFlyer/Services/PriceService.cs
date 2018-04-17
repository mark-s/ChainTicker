﻿using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Core.Domain;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public class PriceService : IPriceService, IDisposable
    {
        private readonly IPubnubTransport _pubnubTransport;
        private readonly IPollingPriceService _priceQueryService;
        private readonly MessageParser _messageParser;


        public PriceService(IPubnubTransport pubnubTransport, IPollingPriceService priceQueryService, MessageParser messageParser)
        {
            _pubnubTransport = pubnubTransport;
            _priceQueryService = priceQueryService;
            _messageParser = messageParser;
        }
        

        public IObservable<ITick> SubscribeToTicks(Market market)
        {
            if (market.HasRealTimeUpdates)
                return SubscribeToLiveMarket(market);
            else
                return SubscribeToTimedUpdated(market);

        }

        private IObservable<ITick> SubscribeToTimedUpdated(Market market) 
            => _priceQueryService.Subscribe(market);

        // This is for markets that have realtime updates available
        private IObservable<ITick> SubscribeToLiveMarket(Market market)
        {
            var channelName = GetChannelName(market);

            _pubnubTransport.SubscribeToChannel(channelName);

            return _pubnubTransport.RecievedMessagesObservable.ObserveOn(Scheduler.Default)
                                                                                     .Where(m => m.ChannelName == channelName)
                                                                                     .Select(m => _messageParser.ConvertToTick(m));
        }

        public void UnsubscribeFromTicks(Market market)
        {
            if (market.HasRealTimeUpdates)
                _pubnubTransport.UnsubscribeFromChannel(GetChannelName(market));
            else
                _priceQueryService.Unubscribe(market);
        }


        private string GetChannelName(Market market)
            => "lightning_ticker_" + market.ProductCode;

        public async Task<ITick> GetCurrentPriceAsync(Market market)
            => await _priceQueryService.GetCurrentPriceAsync(market);


        public void Dispose() 
            => _pubnubTransport?.Dispose();

        public bool IsSubscribedToTicks(Market market)
        {
            var channelName = GetChannelName(market);
            return _pubnubTransport.IsSubscribedToChannel(channelName);
        }
    }
}