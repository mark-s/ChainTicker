using System;

namespace ChainTicker.Transport.Pubnub
{
    public interface IPubnubTransport
    {
        IObservable<ReceivedMessage> RecievedMessagesObservable { get; }

        void SubscribeToChannel(string channelName);

        void UnsubscribeToChannel(string channelName);

    }
}