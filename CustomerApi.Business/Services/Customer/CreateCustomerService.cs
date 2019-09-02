using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using CustomerApi.Business.Services.ValueTypes;
using System;

namespace CustomerApi.Business.Services.Customer
{
    public class CreateCustomerService
    {
        private readonly IDataRepository _dataRepository;

        public CreateCustomerService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IdentifiableResult<CustomerModel> CreateCustomer(CustomerModel customer)
        {
            var newId = Guid.NewGuid();
            customer.Id = newId;
            _dataRepository.CreateCustomer(customer);
            return new IdentifiableResult<CustomerModel>(newId.ToString(), customer);
        } 
    }
}
