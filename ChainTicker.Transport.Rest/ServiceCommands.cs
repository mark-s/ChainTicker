using System;
using System.Collections.Generic;

namespace ChainTicker.Transport.Rest
{
    public class ServiceCommands
    {
        private readonly string _serviceBaseUri;
        private readonly Dictionary<string, Command> _commandMap = new Dictionary<string, Command>();

        public ServiceCommands(string serviceBaseUri)
        {
            _serviceBaseUri = serviceBaseUri.TrimEnd('/');
        }

        public void AddCommand(string commandName, Command command)
            => _commandMap[commandName] = command;

        public string GetCommandUri(string commandName)
            => _serviceBaseUri + _commandMap[commandName].GetForUri();

        public string GetCommandUri(string commandName, string args)
            => _serviceBaseUri + _commandMap[commandName].GetForUri(args);

    }
}