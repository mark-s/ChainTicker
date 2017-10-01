using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChanTicker.Core.IO
{
    public class FileIOService : IFileIOService
    {
        private readonly ISerialize _serializer;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _applicationBaseFolder;
        private const string APPNAME = "ChainTicker";

        public FileIOService(ISerialize serializer)
        {
            _serializer = serializer;

            _applicationBaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), APPNAME);
            EnsureApplicationStorageExists(_applicationBaseFolder);
        }
 

        public DateTime GetFileSaveTime(string fileName) 
            => File.GetLastWriteTime(GetPathAndFilename(fileName));

        public bool FileExists(string fileName) 
            => File.Exists(GetPathAndFilename(fileName));

        public async Task SaveAsync<T>(string fileName, T data)
        {
            var jsonText = _serializer.Serialize(data);

            var jsonAsBytes = _encoding.GetBytes(jsonText);

            var fullPathAndFileName = GetPathAndFilename(fileName);
            if (File.Exists(fullPathAndFileName))
                File.Delete(fullPathAndFileName);

            using (var fileStream = File.Open(fullPathAndFileName, FileMode.OpenOrCreate))
            {
                await fileStream.WriteAsync(jsonAsBytes, 0, jsonAsBytes.Length);
            }
        }

        public async Task<T> LoadAsync<T>(string fileName)
        {
            var fullPathAndFileName = GetPathAndFilename(fileName);
            byte[] result;

            using (var fileStream = File.Open(fullPathAndFileName, FileMode.Open))
            {
                result = new byte[fileStream.Length];
                await fileStream.ReadAsync(result, 0, (int)fileStream.Length);
            }

            var jsonString = _encoding.GetString(result);

            return _serializer.Deserialize<T>(jsonString);
        }


        private string GetPathAndFilename(string fileName) 
            => Path.Combine(_applicationBaseFolder, fileName);


        private void EnsureApplicationStorageExists(string applicationBaseFolder)
        {
            if (Directory.Exists(applicationBaseFolder) == false)
                Directory.CreateDirectory(applicationBaseFolder);
        }

    }
}
