namespace ChainTicker.Transport.Rest
{
    public class Response<T>
    {
        public T Data { get; }
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        private Response(T data, string errorMessage, bool isSuccess)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static Response<T> Success(T data)
            => new Response<T>(data, string.Empty, true);

        public static Response<T> Failure(string errorMessage)
            => new Response<T>(default(T), errorMessage, false);

    }
}