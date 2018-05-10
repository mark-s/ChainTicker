using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ChainTicker.Core.Domain;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Core.Interfaces;
using EnsureThat;

namespace ChainTicker.Exchange.BitFlyer.Services
{
    public class PriceService : IPriceService, IDisposable
    {
        private readonly IPubnubTransport _pubnubTransport;
        private readonly IPollingPriceService _priceQueryService;
        private readonly MessageParser _messageParser;


        public PriceService(IPubnubTransport pubnubTransport, IPollingPriceService priceQueryService, MessageParser messageParser)
        {
            _pubnubTransport = EnsureArg.IsNotNull(pubnubTransport, nameof(pubnubTransport));
            _priceQueryService = EnsureArg.IsNotNull(priceQueryService, nameof(priceQueryService));
            _messageParser = EnsureArg.IsNotNull(messageParser, nameof(messageParser));
        }


        public IObservable<ITick> SubscribeToTicks(IMarket market)
        {
            EnsureArg.IsNotNull(market, nameof(market));

            if (market.HasRealTimeUpdates)
                return SubscribeToLiveMarket(market);
            else
                return SubscribeToTimedUpdated(market);

        }

        private IObservable<ITick> SubscribeToTimedUpdated(IMarket market)
        {
            var marketToSubscribeTo = EnsureArg.IsNotNull(market, nameof(market));

            return _priceQueryService.Subscribe(marketToSubscribeTo);
        }

        // This is for markets that have realtime updates available
        private IObservable<ITick> SubscribeToLiveMarket(IMarket market)
        {
            EnsureArg.IsNotNull(market, nameof(market));

            var channelName = GetChannelName(market);

            _pubnubTransport.SubscribeToChannel(channelName);

            return _pubnubTransport.RecievedMessagesObservable.ObserveOn(Scheduler.Default)
                                                                                     .Where(m => m.ChannelName == channelName)
                                                                                     .Select(m => _messageParser.ConvertToTick(m));
        }

        public void UnsubscribeFromTicks(IMarket market)
        {
            EnsureArg.IsNotNull(market, nameof(market));

            if (market.HasRealTimeUpdates)
                _pubnubTransport.UnsubscribeFromChannel(GetChannelName(market));
            else
                _priceQueryService.Unubscribe(market);
        }


        private string GetChannelName(IMarket market)
            => "lightning_ticker_" + market.ProductCode;

        public async Task<ITick> GetCurrentPriceAsync(IMarket market)
        {
            EnsureArg.IsNotNull(market, nameof(market));
            return await _priceQueryService.GetCurrentPriceAsync(market);
        }


        public void Dispose()
            => _pubnubTransport?.Dispose();

        public bool IsSubscribedToTicks(IMarket market)
        {
            EnsureArg.IsNotNull(market, nameof(market));
            var channelName = GetChannelName(market);
            return _pubnubTransport.IsSubscribedToChannel(channelName);
        }
    }
}