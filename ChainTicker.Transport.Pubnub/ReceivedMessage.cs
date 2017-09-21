namespace ChainTicker.Transport.Pubnub
{
    public class ReceivedMessage
    {
        public string ChannelName { get;}
        public string RawMessage { get; }

        public ReceivedMessage(string channelName, string rawMessage)
        {
            ChannelName = channelName;
            RawMessage = rawMessage;
        }
    }
}