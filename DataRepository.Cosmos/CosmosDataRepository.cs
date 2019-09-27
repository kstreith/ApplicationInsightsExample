using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            try
            {
                await container.CreateItemAsync<CustomerDocument>(new CustomerDocument(customer));
            } catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(Guid customerId)
        {
            var client = _connection.GetOrCreateCosmosClient();
            var container = client.GetContainer("CustomerApi", "Customer");
            try
            {
                var customerResponse = await container.ReadItemAsync<CustomerDocument>($"{customerId.ToString()}|CustomerDocument", new PartitionKey(customerId.ToString()));
                var customerModel = customerResponse.Resource.ToCustomerModel();
                return customerModel;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public Task<List<CustomerInteractionModel>> GetInteractionsAsync(int page)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetRandomCustomerIdsAsync()
        {
            var randomGuid = Guid.NewGuid();
            var client = _connection.GetOrCreateCosmosClient();
            var container = client.GetContainer("CustomerApi", "Customer");
            var queryable = container.GetItemLinqQueryable<CustomerDocument>(requestOptions: new QueryRequestOptions { MaxBufferedItemCount = 100 });
            var iterator = queryable.Where(x => x.PartitionKey.CompareTo(randomGuid.ToString()) > 0).ToFeedIterator();
            var results = await iterator.ReadNextAsync();
            var ids = results.Resource.Select(x => x.PartitionKey).ToList();
            if (ids.Count < 100)
            {
                var queryable2 = container.GetItemLinqQueryable<CustomerDocument>(requestOptions: new QueryRequestOptions { MaxBufferedItemCount = 100 });
                var iterator2 = queryable2.Where(x => x.PartitionKey.CompareTo(randomGuid.ToString()) <= 0).ToFeedIterator();
                var results2 = await iterator2.ReadNextAsync();
                var moreIds = results2.Resource.Select(x => x.PartitionKey).ToList();
                ids.AddRange(moreIds);
            }
            return ids;
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
