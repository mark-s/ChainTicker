using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChanTicker.Core.IO
{
    public class FileIOService : IFileIOService
    {
        private readonly Encoding _encoding = Encoding.UTF8;



        public DateTime GetFileSaveDate(string fileName) 
            => File.GetLastWriteTime(GetPathAndFilename(fileName));

        public bool FileExists(string fileName) 
            => File.Exists(GetPathAndFilename(fileName));

        public async Task SaveAsync<T>(string fileName, T data)
        {
            var jsonText = JsonConvert.SerializeObject(data);
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

            return JsonConvert.DeserializeObject<T>(jsonString);
        }


        private string GetPathAndFilename(string fileName) 
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
    }
}
