namespace ChainTicker.Transport.Rest
{
    public interface IResponse<out T>
    {
        T Data { get; }
        bool IsSuccess { get; }
        string ErrorMessage { get; }

    }
}