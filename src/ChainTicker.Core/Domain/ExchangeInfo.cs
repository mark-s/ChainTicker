using System.Diagnostics;

namespace ChainTicker.Core.Domain
{

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class ExchangeInfo
    {
        public string Name { get; }

        public string HomePageUrl { get; }

        public string Description { get; }

        public bool IsEnabled { get; }

        public ApiEndpointCollection ApiEndpoints { get;  }

        public ExchangeInfo(string name, string homePageUrl, string description, bool isEnabled, ApiEndpointCollection apiEndpoints)
        {
            Name = name;
            HomePageUrl = homePageUrl;
            Description = description;
            IsEnabled = isEnabled;
            ApiEndpoints = apiEndpoints;
        }
        
    }
}