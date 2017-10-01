using ChanTicker.Core.Interfaces;

namespace ChainTicker.Domain
{
    public class ExchangeInfo : IExchangeInfo
    {
        public string Name { get; }
        public string Uri { get; }
        public string Description { get; }
        public bool IsEnabled { get; }

        public ExchangeInfo(string name, string uri, string description, bool isEnabled)
        {
            Name = name;
            Uri = uri;
            Description = description;
            IsEnabled = isEnabled;
        }

        
    }
}