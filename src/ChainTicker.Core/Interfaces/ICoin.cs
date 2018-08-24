namespace ChainTicker.Core.Interfaces
{
    public interface ICoin
    {
        bool IsValid { get; }

        string Code { get; }

        string Description { get; }

        string Name { get; }

        ICoinUrlSet Urls { get; }

    }
}
