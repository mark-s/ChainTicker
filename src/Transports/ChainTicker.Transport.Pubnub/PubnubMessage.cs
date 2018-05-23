namespace ChainTicker.Transport.Pubnub
{
    public class PubnubMessage
    {
        public string ChannelName { get;}
        public string Content { get; }

        public PubnubMessage(string channelName, string content)
        {
            ChannelName = channelName;
            Content = content;
        }
    }
}