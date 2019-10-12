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
            await container.CreateItemAsync<CustomerDocument>(new CustomerDocument(customer));
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

        public async Task<List<string>> GetRandomCustomerIdsAsync()
        {
            var randomGuid = Guid.NewGuid();
            var client = _connection.GetOrCreateCosmosClient();
            var container = client.GetContainer("CustomerApi", "Customer");
            var queryable = container.GetItemLinqQueryable<CustomerDocument>(requestOptions: new QueryRequestOptions { MaxBufferedItemCount = 100 });
#pragma warning disable CA1307 // Specify StringComparison
            var iterator = queryable.Where(x => x.PartitionKey.CompareTo(randomGuid.ToString()) > 0).ToFeedIterator();
#pragma warning restore CA1307 // Specify StringComparison
            var results = await iterator.ReadNextAsync();
            var ids = results.Resource.Select(x => x.PartitionKey).ToList();
            if (ids.Count < 100)
            {
                var queryable2 = container.GetItemLinqQueryable<CustomerDocument>(requestOptions: new QueryRequestOptions { MaxBufferedItemCount = 100 });
#pragma warning disable CA1307 // Specify StringComparison
                var iterator2 = queryable2.Where(x => x.PartitionKey.CompareTo(randomGuid.ToString()) <= 0).ToFeedIterator();
#pragma warning restore CA1307 // Specify StringComparison
                var results2 = await iterator2.ReadNextAsync();
                var moreIds = results2.Resource.Select(x => x.PartitionKey).ToList();
                ids.AddRange(moreIds);
            }
            return ids;
        }

        public async Task OverwriteCustomerAsync(CustomerModel customer)
        {
            var client = _connection.GetOrCreateCosmosClient();
            var container = client.GetContainer("CustomerApi", "Customer");
            var customerDocument = new CustomerDocument(customer);
            await container.ReplaceItemAsync<CustomerDocument>(customerDocument, customerDocument.id, new PartitionKey(customerDocument.PartitionKey));
        }
    }
}
