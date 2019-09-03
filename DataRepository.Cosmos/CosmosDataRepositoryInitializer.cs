using CustomerApi.Business.Interfaces;
using System;
using System.Threading.Tasks;

namespace DataRepository.Cosmos
{
    public class CosmosDataRepositoryInitializer : IDataRepositoryInitializer
    {
        private readonly CosmosConnection _connection;

        public CosmosDataRepositoryInitializer(CosmosConnection connection)
        {
            _connection = connection;
        }

        public async Task InitAsync()
        {
            var client = _connection.GetOrCreateCosmosClient();
            await client.CreateDatabaseIfNotExistsAsync("CustomerApi", 400);
            await client.GetDatabase("CustomerApi")
                .DefineContainer("Customer", $"/{nameof(CustomerDocument.PartitionKey)}")
                .CreateIfNotExistsAsync();
        }
    }
}
