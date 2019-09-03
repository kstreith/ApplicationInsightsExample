using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using CustomerApi.Business.Services.ValueTypes;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class CreateCustomerService
    {
        private readonly IDataRepository _dataRepository;

        public CreateCustomerService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IdentifiableResult<CustomerModel>> CreateCustomerAsync(CustomerModel customer)
        {
            var newId = Guid.NewGuid();
            customer.Id = newId;
            await _dataRepository.CreateCustomerAsync(customer);
            return new IdentifiableResult<CustomerModel>(newId.ToString(), customer);
        } 
    }
}
