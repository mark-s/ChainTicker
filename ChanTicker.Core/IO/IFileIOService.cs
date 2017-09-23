using System;
using System.Threading.Tasks;

namespace ChanTicker.Core.IO
{
    public interface IFileIOService
    {
        Task<T> LoadAsync<T>(string fileName);

        Task SaveAsync<T>(string fileName, T data);

        DateTime GetFileSaveDate(string fileName);

        bool FileExists(string fileName);

    }
}