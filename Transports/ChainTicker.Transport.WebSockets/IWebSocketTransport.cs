using System;

namespace ChainTicker.Transport.WebSocket
{
    public interface IWebSocketTransport
    {
        IObservable<string> RecievedMessagesObservable { get;  }

        void Send(string message);


    }
}