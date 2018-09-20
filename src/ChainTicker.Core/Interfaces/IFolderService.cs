using ChainTicker.Core.IO;

namespace ChainTicker.Core.Interfaces
{
    public interface IFolderService
    {
        string GetFolderPath(AppFolder folder);
    }
}