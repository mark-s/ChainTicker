namespace ChainTicker.Transport.Shared
{
    public interface ISerialize
    {
        T Deserialize<T>(string jsonText);
        string Serialize<T>(T value);
    }
}