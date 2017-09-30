using System;
using System.Collections.Generic;

namespace ChainTicker.Transport.Pubnub
{
    public interface IPubnubTransport
    {
        IObservable<ReceivedMessage> RecievedMessagesObservable { get; }

        void SubscribeToChannel(string channelName);

        List<string> GetSubscribedChannels();

        void UnsubscribeToChannel(string channelName);
        
        void UnsubscribeFromAllChannels();

        void Disconnect();

    }
}