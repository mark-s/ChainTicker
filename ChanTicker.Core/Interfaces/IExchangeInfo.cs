namespace ChanTicker.Core.Interfaces
{
    public interface IExchangeInfo
    {
        string Name { get; }

        string Uri { get; }

        string Description { get; }

        bool IsEnabled { get; }
    }
}