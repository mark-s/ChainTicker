using System;
using System.Collections.Generic;

namespace ChainTicker.Transport.Pubnub
{
    public interface IPubnubTransport : IDisposable
    {
        IObservable<PubnubMessage> RecievedMessagesObservable { get; }

        void SubscribeToChannel(string channelName);

        List<string> GetSubscribedChannels();

        void UnsubscribeFromChannel(string channelName);
        
        void UnsubscribeFromAllChannels();

        bool IsSubscribedToChannel(string channelName);
    }
}