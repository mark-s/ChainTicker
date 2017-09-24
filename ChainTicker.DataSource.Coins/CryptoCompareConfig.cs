using System;

namespace ChainTicker.DataSource.Coins
{
    public class CryptoCompareConfig
    {
        public string RestBaseUri { get; }
        public string CacheFileName { get; }
        public int MaxCacheAgeDays { get; }


        public CryptoCompareConfig(string restBaseUri = "https://www.cryptocompare.com/api",
                                             string cacheFileName = "coins.json",
                                             int maxCacheAgeDays = 7)
        {
            if (Uri.IsWellFormedUriString(restBaseUri, UriKind.RelativeOrAbsolute) == false)
                throw new ArgumentException("provide a valid uri!", nameof(restBaseUri));

            if (maxCacheAgeDays < 0)
                throw new ArgumentOutOfRangeException(nameof(maxCacheAgeDays), "max cache age must be a positive value!");

            if (string.IsNullOrEmpty(cacheFileName))
                throw new ArgumentNullException(nameof(cacheFileName), "filename is required!");

            RestBaseUri = restBaseUri;
            CacheFileName = cacheFileName;
            MaxCacheAgeDays = maxCacheAgeDays;
        }
    }
}
