using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using PubnubApi;

namespace ChainTicker.Transport.Pubnub
{
    public class PubnubTransport : IPubnubTransport
    {
        private readonly PubnubApi.Pubnub _pubnubConnector;
        public IObservable<PubnubMessage> RecievedMessagesObservable => _messageSubject.AsObservable();
        private readonly Subject<PubnubMessage> _messageSubject = new Subject<PubnubMessage>();


        public PubnubTransport(string subscribeKey, IPubnubLog logger)
        {
            var config = CreateConfiguration(subscribeKey, logger);

            _pubnubConnector = new PubnubApi.Pubnub(config);

            var listenerSubscribeCallback = new SubscribeCallbackExt((pubnubObj, message) => MessageReceivedCallback(message),
                                                                                         (pubnubObj, presence) => PresenceCallback(presence),
                                                                                         StatusCallback);

            _pubnubConnector.AddListener(listenerSubscribeCallback);
        }


        public void SubscribeToChannel(string channelName)
        {
            var subscribed = GetSubscribedChannels();

            if (subscribed.Contains(channelName))
            {
                Debug.WriteLine($"Trying to subscribe to already subscribed channel, Ignoring. [{channelName}]");
                return;
            }

            subscribed.Add(channelName);

            _pubnubConnector.Subscribe<string>().Channels(subscribed.ToArray()).Execute();
        }

        public void UnsubscribeFromChannel(string channelName)
        {
            if (GetSubscribedChannels().Contains(channelName))
                _pubnubConnector.Unsubscribe<string>().Channels(new[] { channelName }).Execute();
            else
                Debug.WriteLine($"Trying to Unsubscribe from an unknown channel, Ignoring. [{channelName}]");
        }

        public void UnsubscribeFromAllChannels()
            => _pubnubConnector.UnsubscribeAll<string>();

        public bool IsSubscribedToChannel(string channelName)
            => GetSubscribedChannels().Contains(channelName);


        public List<string> GetSubscribedChannels()
            => _pubnubConnector.GetSubscribedChannels() ?? new List<string>();

        private PNConfiguration CreateConfiguration(string subscribeKey, IPubnubLog logger)
        {
            return new PNConfiguration
            {
                SubscribeKey = subscribeKey,
                PubnubLog = logger,
                Secure = true
            };
        }


        private void PresenceCallback(PNPresenceEventResult presence)
            => Debug.WriteLine(presence.Event);

        private void MessageReceivedCallback(PNMessageResult<object> message)
            => _messageSubject.OnNext(new PubnubMessage(message.Channel, message.Message as string));

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


        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pubnubConnector?.Disconnect<string>();
                _messageSubject?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PubnubTransport()
        {
            Dispose(false);
        }
    }
}