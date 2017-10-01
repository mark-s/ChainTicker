using System;
using System.Threading.Tasks;

namespace ChanTicker.Core.IO
{
    public interface IFileIOService
    {
        Task<string> LoadAsync(string fileName);

        Task SaveAsync(string fileName, string textToSave);

        DateTime GetFileSaveTime(string fileName);

        bool FileExists(string fileName);
    }
}