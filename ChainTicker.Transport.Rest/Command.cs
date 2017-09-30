namespace ChainTicker.Transport.Rest
{
    public class Command
    {
        private readonly string _path;
        private readonly string _queryBase;


        /// <param name="path">eg : /v1/board</param>
        public Command(string path)
        {
            if (path.StartsWith("/"))
                _path = path;
            else
                _path = "/" + path;
        }

        /// <param name="path">eg : /v1/board</param>
        /// <param name="queryBase">eg: product_code</param>
        public Command(string path, string queryBase) : this(path)
        {
            _queryBase = queryBase;
        }


        public string GetForUri(string args)
            => $"{_path}?{_queryBase}={args}";

        public string GetForUri()
            => _path;
    }
}