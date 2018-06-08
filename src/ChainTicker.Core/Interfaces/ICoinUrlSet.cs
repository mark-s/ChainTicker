namespace ChainTicker.Core.Interfaces
{
    public interface ICoinUrlSet
    {

        string ImageUrlShort { get; }
        string InfoUrlShort { get; }

        string ImageUrlFull { get; }
        string InfoUrlFull { get; }

        string ImageFileName { get; }

    }



}