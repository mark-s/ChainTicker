using System;

namespace ChainTicker.Transport.WebSocket
{
    public interface IWebSocketTransport 
    {

        IObservable<string> SubscribeToEndpoint(string endpointUri);


    }
}