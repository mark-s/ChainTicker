namespace ChanTicker.Core.Interfaces
{
    public interface ISerialize
    {
        T Deserialize<T>(string jsonText);
        string Serialize<T>(T value);
    }
}