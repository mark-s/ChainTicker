using System;
using System.Threading.Tasks;
using ChainTicker.Core.IO;

namespace ChainTicker.Core.Interfaces
{
    public interface IDiskIOService
    {
        Task<string> LoadTextAsync(AppFolder folder, string fileName);

        Task SaveTextAsync(AppFolder folder, string fileName, string textToSave);

        DateTime GetFileSaveTime(AppFolder folder, string fileName);

        string GetPathAndFilename(AppFolder folder, string fileName);

        bool FileExists(AppFolder folder, string fileName);

        bool FileExists(string fileName);
    }
}