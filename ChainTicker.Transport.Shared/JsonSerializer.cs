using Newtonsoft.Json;

namespace ChainTicker.Transport.Shared
{
    public class JsonSerializer : ISerialize
    {
        public T Deserialize<T>(string jsonText) => JsonConvert.DeserializeObject<T>(jsonText);

        public string Serialize<T>(T value) => JsonConvert.SerializeObject(value);
    }
}