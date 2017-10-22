using System;
using System.Reactive.Linq;
using WebSocket4Net;

namespace ChainTicker.Transport.WebSocket
{
    public class WebSocketTransport : IWebSocketTransport
    {

        public IObservable<string> SubscribeToEndpoint(string endpointUri)
        {
            var websocket = new WebSocket4Net.WebSocket(endpointUri);
            return Observable.Using(() => websocket,
                                             ws => Observable.FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                                                                                   handler => ws.MessageReceived += handler,
                                                                                                   handler => ws.MessageReceived -= handler))
                                                                                                   .Select(m => m?.EventArgs?.Message);
        }

    }
}
