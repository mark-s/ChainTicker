namespace ChainTicker.Core.Interfaces
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string jsonText);

        string Serialize<T>(T value);

    }
}