﻿using System;
using System.Threading.Tasks;


namespace ChainTicker.Transport.Rest
{
    public interface IRestService
    {
        Task<Response<T>> GetAsync<T>(string restEndpointUrl, Func<string, T> deserialize);

    }
}