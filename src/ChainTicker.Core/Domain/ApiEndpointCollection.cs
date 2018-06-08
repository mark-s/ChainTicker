using System.Collections.Generic;

namespace ChainTicker.Core.Domain
{
    public class ApiEndpointCollection
    {
        private readonly Dictionary<ApiEndpointType, string> _endpoints = new Dictionary<ApiEndpointType, string>(3);

        public string this[ApiEndpointType endpointType] // Indexer declaration  
        {
            get => _endpoints[endpointType];
            set => _endpoints[endpointType] = value;
        }
    }

}
