namespace ChanTicker.Core.Interfaces
{
    public interface ICoin
    {
        bool IsValid { get; }

        string Code { get; }
        string Description { get; }
        string Name { get; }

        string Algorithm { get; }

        string ImageUrlShort { get; }
        string InfoUrlShort { get; }

        string ImageUrlFull { get; }
        string InfoUrlFull { get; }

        string ImageFileName { get; }

        bool IsFullyPremined { get; }
        string PreMinedValue { get; }
        string ProofType { get; }
        string TotalCoinsFreeFloat { get; }
        string TotalCoinSupply { get; }

        

    }


}