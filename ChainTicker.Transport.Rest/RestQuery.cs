namespace ChainTicker.Transport.Rest
{
    public class RestQuery
    {
        private readonly string _serviceBaseUri; // eg: https://some.service
        private readonly string _path;            // eg: /v1/board

        public RestQuery(string serviceBaseUri, string path)
        {
            _serviceBaseUri = serviceBaseUri.TrimEnd('/');
            _path = path.TrimStart('/');
        }

        public string GetAddress()
            => $"{_serviceBaseUri}/{_path}";
    }
}