namespace ChainTicker.Transport.Rest
{
    public class Response<T> 
    {
        public T Data { get; }
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        public Response(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        public Response(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
            Data = default(T);
        }


    }
}