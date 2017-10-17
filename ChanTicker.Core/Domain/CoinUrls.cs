namespace ChanTicker.Core.Domain
{
    public class CoinUrls : Interfaces.ICoinUrlSet
    {
        public string ImageUrlShort { get; }
        public string InfoUrlShort { get; }
        public string ImageUrlFull { get; }
        public string InfoUrlFull { get; }
        public string ImageFileName { get; }

        public CoinUrls(string imageUrlShort,
                                string infoUrlShort,
                                string imageUrlFull,
                                string infoUrlFull,
                                string imageFileName)
        {
            ImageUrlShort = imageUrlShort;
            InfoUrlShort = infoUrlShort;
            ImageUrlFull = imageUrlFull;
            InfoUrlFull = infoUrlFull;
            ImageFileName = imageFileName;
        }
    }



}