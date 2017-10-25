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
            _websocket.Open();
        }

        public void StartListen()
        {
            if (_isListening) return;
            _isListening = true;
            

            RecievedMessagesObservable = Observable.Using(() => _websocket,
                                             ws => Observable.FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                                                                                   handler => ws.MessageReceived += handler,
                                                                                                   handler => ws.MessageReceived -= handler))
                                                                                                   .Select(m => m?.EventArgs?.Message);
        }

        public void Send(string message)
        {
            StartListen();
            _websocket.Send(message);
        }


    }
}
