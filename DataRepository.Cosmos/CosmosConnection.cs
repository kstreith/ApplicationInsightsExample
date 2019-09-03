using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Cosmos
{
    public class CosmosConnection
    {
        private readonly string _endpoint;
        private readonly string _authKey;
        private CosmosClient _client;

        internal CosmosConnection(string endpoint, string authKey)
        {
            _endpoint = endpoint;
            _authKey = authKey;
            _client = null;
        }

        internal CosmosClient GetOrCreateCosmosClient()
        {
            if (_client == null)
            {
                _client = new CosmosClient(_endpoint, _authKey);
            }
            return _client;
        }
    }
}
