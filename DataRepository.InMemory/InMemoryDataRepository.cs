using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        public Task<List<string>> GetRandomCustomerIdsAsync()
        {
            var values = _customers.Values;
            var items = Math.Min(_customers.Values.Count, 100);
            return Task.FromResult(values.Select(x => x.Id.ToString()).Take(items).ToList());
        }

        public Task OverwriteCustomerAsync(CustomerModel customer)
        {
            if (!_customers.TryGetValue(customer.Id.Value, out var _))
            {
                throw new InvalidOperationException("Customer not found");
            }
            _customers.AddOrUpdate(customer.Id.Value, customer, (id, model) => customer);
            return Task.CompletedTask;
        }
    }
}
