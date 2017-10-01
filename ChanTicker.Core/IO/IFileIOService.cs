using System;
using System.Threading.Tasks;

namespace ChanTicker.Core.IO
{
    public interface IFileIOService
    {
        Task<string> LoadTextAsync(ChainTickerFolder folder, string fileName);

        Task SaveTextAsync(ChainTickerFolder folder, string fileName, string textToSave);

        DateTime GetFileSaveTime(ChainTickerFolder folder, string fileName);

        string GetPathAndFilename(ChainTickerFolder folder, string fileName);

        bool FileExists(ChainTickerFolder folder, string fileName);

        bool FileExists(string fileName);
    }
}