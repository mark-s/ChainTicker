namespace ChanTicker.Core.Interfaces
{
    public interface ICoin
    {
        string Code { get; }
        string Description { get; }
        string Name { get; }

        string Algorithm { get; }
        string ImageUrl { get; }
        string InfoUrl { get; }
        bool IsFullyPremined { get; }
        string PreMinedValue { get; }
        string ProofType { get; }
        string TotalCoinsFreeFloat { get; }
        string TotalCoinSupply { get; }
    }
}