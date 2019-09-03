using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;

namespace DataRepository.InMemory
{
    public class InMemoryDataRepository : IDataRepository
    {
        private ConcurrentDictionary<Guid, CustomerModel> _customers;

        public InMemoryDataRepository()
        {
            _customers = new ConcurrentDictionary<Guid, CustomerModel>();
            var id = new Guid("88f37d26-6616-4598-8792-e3bb9b814c72");
            _customers.TryAdd(id, new CustomerModel
            {
                Id = id,
                FirstName = "TestFirst",
                LastName = "TestLast",
                EmailAddress = "test@test.itsnull.com"
            });
        }

        public Task CreateCustomerAsync(CustomerModel customer)
        {
            if (!_customers.TryAdd(customer.Id.Value, customer))
            {
                throw new InvalidOperationException("Add failed");
            }
            return Task.CompletedTask;
        }

        public Task<CustomerModel> GetCustomerByIdAsync(Guid customerId)
        {
            if (_customers.TryGetValue(customerId, out var customer))
            {
                return Task.FromResult(customer);
            }
            return Task.FromResult<CustomerModel>(null);
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
