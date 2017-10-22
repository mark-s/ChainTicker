using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using WebSocket4Net;

namespace ChainTicker.Transport.WebSocket
{
    public class WebSocketTransport  //: IWebSocketTransport
    {
        private readonly WebSocket4Net.WebSocket _websocket;

        private readonly HashSet<string> _subscribedChannels = new HashSet<string>();

        public WebSocketTransport(string endpointUri)
        {
            _websocket = new WebSocket4Net.WebSocket(endpointUri);
        }

        public IObservable<string> StartListen()
        {
            
            return Observable.Using(() => _websocket,
                                             ws => Observable.FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                                                                                   handler => ws.MessageReceived += handler,
                                                                                                   handler => ws.MessageReceived -= handler))
                                                                                                   .Select(m => m?.EventArgs?.Message);
        }

        public void SubscribeToChannel(string channelName)
        {
            if (_subscribedChannels.Contains(channelName))
            {
                Debug.WriteLine($"Trying to subscribe to already subscribed channel, Ignoring. [{channelName}]");
                return;
            }
            _subscribedChannels.Add(channelName);


            _websocket.Send()
        }


    }
}
