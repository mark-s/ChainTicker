namespace ChanTicker.Core.Interfaces
{
    public interface ISourceInfo
    {
        string Name { get; }

        string Uri { get; }

        string Description { get; }

        bool IsEnabled { get; }
    }
}