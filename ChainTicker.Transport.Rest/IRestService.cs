using System.Threading.Tasks;


namespace ChainTicker.Transport.Rest
{
    public interface IRestService
    {
        Task<IResponse<T>> GetAsync<T>(string commandName);

        Task<IResponse<T>> GetAsync<T>(string commandName, string commandArgs);

        void RegisterCommands(ServiceCommands commands);
    }
}