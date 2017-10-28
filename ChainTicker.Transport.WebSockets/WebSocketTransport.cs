using System;
using System.Reactive.Linq;
using WebSocket4Net;

namespace ChainTicker.Transport.WebSocket
{
    public class WebSocketTransport : IWebSocketTransport
    {
        private readonly WebSocket4Net.WebSocket _websocket;
        private bool _isListening = false;

        public IObservable<string> RecievedMessagesObservable { get; private set; }


        public WebSocketTransport(string endpointUri)
        {
            _websocket = new WebSocket4Net.WebSocket(endpointUri);
            _websocket.Opened += (sender, args) => StartListen();
            _websocket.Open();

        }

        private void StartListen()
        {
            RecievedMessagesObservable = Observable.FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                                                                                   handler => _websocket.MessageReceived += handler,
                                                                                                   handler => _websocket.MessageReceived -= handler)
                                                                                                        .Select(m => m?.EventArgs?.Message);
        }

        public void Send(string message)
        {
            _websocket.Send(message);
        }


    }
}
