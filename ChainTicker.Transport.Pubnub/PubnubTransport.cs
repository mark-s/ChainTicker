using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using PubnubApi;

namespace ChainTicker.Transport.Pubnub
{
    public class PubnubTransport : IPubnubTransport
    {
        private readonly PubnubApi.Pubnub _pubnubConnector;
        public IObservable<ReceivedMessage> RecievedMessagesObservable => _messageSubject.AsObservable();
        private readonly Subject<ReceivedMessage> _messageSubject = new Subject<ReceivedMessage>();


        public PubnubTransport(string subscribeKey, IPubnubLogger logger)
        {
            var config = CreateConfiguration(subscribeKey, logger);

            _pubnubConnector = new PubnubApi.Pubnub(config);

            var listenerSubscribeCallback = new SubscribeCallbackExt((pubnubObj, message) => MessageReceivedCallback(message),
                                                                     (pubnubObj, presence) => PresenceCallback(presence), StatusCallback);

            _pubnubConnector.AddListener(listenerSubscribeCallback);

        }


        public void SubscribeToChannel(string channelName)
        {
            //.Channels(new[] { "lightning_ticker_BTC_JPY" })
            //    .Channels(new[] { "lightning_ticker_ETH_BTC" })

            _pubnubConnector.Subscribe<string>().Channels(new[] { channelName }).Execute();
        }

        public void UnsubscribeToChannel(string channelName)
        {
            _pubnubConnector.Unsubscribe<string>().Channels(new[] { channelName }).Execute();
        }

        private PNConfiguration CreateConfiguration(string subscribeKey, IPubnubLog logger)
        {
            // subscribeKey = "sub-c-52a9ab50-291b-11e5-baaa-0619f8945a4f"
            return new PNConfiguration
                       {
                           SubscribeKey = subscribeKey,
                           PubnubLog = logger
                       };
        }


        private void PresenceCallback(PNPresenceEventResult presence)
            => Debug.WriteLine(presence.Event);

        private void MessageReceivedCallback(PNMessageResult<object> message)
            => _messageSubject.OnNext(new ReceivedMessage(message.Channel, message.Message as string));

        private void StatusCallback(PubnubApi.Pubnub pubnubObj, PNStatus status)
        {
            // the status object returned is always related to subscribe but could contain
            // information about subscribe, heartbeat, or errors
            // use the PNOperationType to switch on different options
            switch (status.Operation)
            {
                // let's combine unsubscribe and subscribe handling for ease of use
                case PNOperationType.PNSubscribeOperation:
                case PNOperationType.PNUnsubscribeOperation:
                    // note: subscribe statuses never have traditional
                    // errors, they just have categories to represent the
                    // different issues or successes that occur as part of subscribe
                    switch (status.Category)
                    {
                        case PNStatusCategory.PNConnectedCategory:
                            // this is expected for a subscribe, this means there is no error or issue whatsoever
                            Debug.WriteLine("PNConnectedCategory");
                            break;
                        case PNStatusCategory.PNReconnectedCategory:
                            // this usually occurs if subscribe temporarily fails but reconnects. This means
                            // there was an error but there is no longer any issue
                            Debug.WriteLine("PNReconnectedCategory");
                            break;
                        case PNStatusCategory.PNDisconnectedCategory:
                            // this is the expected category for an unsubscribe. This means there
                            // was no error in unsubscribing from everything
                            Debug.WriteLine("PNDisconnectedCategory");
                            break;
                        case PNStatusCategory.PNUnexpectedDisconnectCategory:
                            // this is usually an issue with the internet connection, this is an error, handle appropriately
                            Debug.WriteLine("PNUnexpectedDisconnectCategory");
                            break;
                        case PNStatusCategory.PNAccessDeniedCategory:
                            // this means that PAM does allow this client to subscribe to this
                            // channel and channel group configuration. This is another explicit error
                            Debug.WriteLine("PNAccessDeniedCategory");
                            break;
                        default:
                            // More errors can be directly specified by creating explicit cases for other
                            // error categories of `PNStatusCategory` such as `PNTimeoutCategory` or `PNMalformedFilterExpressionCategory` or `PNDecryptionErrorCategory`
                            break;
                    }
                    break;
                case PNOperationType.PNHeartbeatOperation:
                    // heartbeat operations can in fact have errors, so it is important to check first for an error.
                    if (status.Error)
                    {
                        // There was an error with the heartbeat operation, handle here
                        Debug.WriteLine("HEARTBEAT ERROR!");
                    }
                    else
                    {
                        // heartbeat operation was successful
                        Debug.WriteLine("HEARTBEAT");
                    }
                    break;
            }
        }
    }
}