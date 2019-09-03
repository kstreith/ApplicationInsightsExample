using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataRepository.Cosmos
{
    public class CosmosDataRepository : IDataRepository
    {
        private readonly CosmosConnection _connection;

        public CosmosDataRepository(CosmosConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateCustomerAsync(CustomerModel customer)
        {
            var client = _connection.GetOrCreateCosmosClient();
            var container = client.GetContainer("CustomerApi", "Customer");
            await container.CreateItemAsync<CustomerModel>(customer);
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(Guid customerId)
        {
            var client = _connection.GetOrCreateCosmosClient();
            var container = client.GetContainer("CustomerApi", "Customer");
            var customerResponse = await container.ReadItemAsync<CustomerModel>(customerId.ToString(), new PartitionKey(customerId.ToString()));
            return customerResponse.Resource;
        }

        public Task<List<CustomerInteractionModel>> GetInteractionsAsync(int page)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> LookupCustomerIdByEmailAsync(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Task OverwriteCustomerAsync(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
