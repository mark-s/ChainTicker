using ChanTicker.Core.IO;

namespace ChanTicker.Core.Interfaces
{
    public interface IFolderService
    {
        string GetFolderPath(ChainTickerFolder folder);
    }
}