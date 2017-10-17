using ChanTicker.Core.Domain;

namespace ChanTicker.Core.Interfaces
{
    public interface ICoin
    {
        bool IsValid { get; }

        string Code { get; }
        string Description { get; }
        string Name { get; }

        ICoinUrlSet Urls { get; }

        IMiningData Mining { get; }

    }
}
