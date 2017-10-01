namespace ChanTicker.Core.Domain
{
    public class ExchangeInfo
    {
        public string Name { get; }

        public string HomePageUrl { get; }

        public string Description { get; }

        public bool IsEnabled { get; }

        public string ApiBaseUrl { get; }

        public ExchangeInfo(string name, string homePageUrl, string description, bool isEnabled, string apiBaseUrl)
        {
            Name = name;
            HomePageUrl = homePageUrl;
            Description = description;
            IsEnabled = isEnabled;
            ApiBaseUrl = apiBaseUrl;
        }
    }
}