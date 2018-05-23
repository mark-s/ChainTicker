namespace ChainTicker.Transport.Rest
{
    public class RestQuery
    {
        private readonly string _serviceBaseUri; // eg: https://some.service
        private readonly string _path;            // eg: /v1/board
        private readonly string _queryString; // eg: product_code


        public RestQuery(string serviceBaseUri, string path)
        {
            _serviceBaseUri = serviceBaseUri.TrimEnd('/');
            _path = path.TrimStart('/');
        }

        public RestQuery(string serviceBaseUri, string path, string queryString) : this(serviceBaseUri, path)
        {
            _queryString = queryString.TrimEnd('=');
        }


        public string Address(string queryStringArgs)
            => $"{_serviceBaseUri}/{_path}?{_queryString}={queryStringArgs}";

        public string Address()
            => $"{_serviceBaseUri}/{_path}";
    }
}