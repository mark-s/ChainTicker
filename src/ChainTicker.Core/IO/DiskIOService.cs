using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.IO
{
    public class DiskIOService : IDiskIOService
    {
        private readonly IFolderService _folderService;

        private readonly Encoding _encoding = Encoding.UTF8;


        public DiskIOService(IFolderService folderService)
        {
            _folderService = folderService;
        }


        public DateTime GetFileSaveTime(AppFolder folder, string fileName)
            => File.GetLastWriteTime(GetPathAndFilename(folder, fileName));

        public bool FileExists(AppFolder folder, string fileName)
            => File.Exists(GetPathAndFilename(folder, fileName));

        public bool FileExists(string fileName)
            => File.Exists(fileName);

        public async Task SaveTextAsync(AppFolder folder, string fileName, string textToSave)
        {
            var jsonAsBytes = _encoding.GetBytes(textToSave);

            var fullPathAndFileName = GetPathAndFilename(folder, fileName);
            if (File.Exists(fullPathAndFileName))
                File.Delete(fullPathAndFileName);

            using (var fileStream = File.Open(fullPathAndFileName, FileMode.OpenOrCreate))
            {
                await fileStream.WriteAsync(jsonAsBytes, 0, jsonAsBytes.Length);
            }
        }

        public async Task<string> LoadTextAsync(AppFolder folder, string fileName)
        {
            var fullPathAndFileName = GetPathAndFilename(folder, fileName);
            byte[] result;

            using (var fileStream = File.Open(fullPathAndFileName, FileMode.Open))
            {
                result = new byte[fileStream.Length];
                await fileStream.ReadAsync(result, 0, (int)fileStream.Length).ConfigureAwait(false);
            }

            return _encoding.GetString(result);
        }


        public string GetPathAndFilename(AppFolder folder, string fileName)
            => Path.Combine(_folderService.GetFolderPath(folder), fileName);




    }
}
