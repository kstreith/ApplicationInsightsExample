using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
            _customers.TryAdd(new Guid("88f37d26-6616-4598-8792-e3bb9b814c72"), new CustomerModel
            {
                FirstName = "TestFirst",
                LastName = "TestLast",
                EmailAddress = "test@test.itsnull.com"
            });
        }

        public void CreateCustomer(CustomerModel customer)
        {
            if (!_customers.TryAdd(customer.Id.Value, customer))
            {
                throw new InvalidOperationException("Add failed");
            }
        }

        public CustomerModel GetCustomerById(Guid customerId)
        {
            if (_customers.TryGetValue(customerId, out var customer))
            {
                return customer;
            }
            return null;
        }

        public List<CustomerInteractionModel> GetInteractions(int page)
        {
            throw new NotImplementedException();
        }

        public Guid LookupCustomerIdByEmail(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public void OverwriteCustomer(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
