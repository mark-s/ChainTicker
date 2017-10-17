using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Domain
{
    public sealed class CoinUrlsUnknown : ICoinUrlSet
    {
        public string ImageUrlShort { get; } = string.Empty;
        public string InfoUrlShort { get; } = string.Empty;
        public string ImageUrlFull { get; } = string.Empty;
        public string InfoUrlFull { get; } = string.Empty;
        public string ImageFileName { get; } = string.Empty;
    }



}