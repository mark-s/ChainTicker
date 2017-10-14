﻿using System;
 
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ChainTicker.Exchange.BitFlyer.DTO;
using ChainTicker.Transport.Pubnub;
using ChainTicker.Transport.Rest;
using ChanTicker.Core.Domain;
using ChanTicker.Core.Interfaces;
using ChanTicker.Core.IO;

namespace ChainTicker.Exchange.BitFlyer
{
    public class BitFlyerMarketDataService : IMarketDataService, IDisposable
    {
        private readonly string _baseUrl;
        private readonly IRestService _restService;
        private readonly IPubnubTransport _pubnubTransport;
        private readonly ISerialize _serializer;
        private readonly MessageParser _messageParser;


        public BitFlyerMarketDataService(string baseUrl, IRestService restService, IPubnubTransport pubnubTransport)
        {
            _baseUrl = baseUrl;
            _restService = restService;
            _pubnubTransport = pubnubTransport;
            _serializer = new ChainTickerJsonSerializer();

            _messageParser = new MessageParser(_serializer);
        }

        public async Task<ITick> GetCurrentPriceAsync(Market market)
        {
            var query = new RestQuery(_baseUrl, "/v1/getticker", "product_code");

            var result = await _restService.GetAsync(query.GetAddress(market.Id), s => _serializer.Deserialize<BitFlyerTick>(s));

            if (result.IsSuccess)
            {
                return new Tick(result.Data.LastTradedPrice, result.Data.TickTimeStamp);
            }
            else
            {
                // TODO: display this to user
                Debug.WriteLine("Failed to get Markets! " + result.ErrorMessage);
                return new EmptyTick();
            }
        }


        public IObservable<ITick> SubscribeToTicks(Market market)
        {
            var channelName = GetChannelName(market);

            _pubnubTransport.SubscribeToChannel(channelName);

            return _pubnubTransport.RecievedMessagesObservable
                                                 .ObserveOn(Scheduler.Default)
                                                .Where(m => m.ChannelName == channelName)
                                                .Select(m => _messageParser.ConvertToTick(m));
        }

        public void UnsubscribeFromTicks(Market market) 
            => _pubnubTransport.UnsubscribeFromChannel(GetChannelName(market));


        private string GetChannelName(Market market)
            => "lightning_ticker_" + market.Id;


        public void Dispose()
        {
            _pubnubTransport?.Dispose();
        }

        public bool IsSubscribedToTicks(Market market)
        {
            var channelName = GetChannelName(market);
            return _pubnubTransport.IsSubscribedToChannel(channelName);
        }
    }
}