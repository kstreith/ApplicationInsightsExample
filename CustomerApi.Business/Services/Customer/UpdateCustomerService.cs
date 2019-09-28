using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class UpdateCustomerService
    {
        private readonly IDataRepository _dataRepository;

        public UpdateCustomerService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task UpdateCustomerAsync(string id, CustomerModel customer)
        {
            if (!Guid.TryParse(id, out var idGuid)) {
                throw new NotFoundException();
            }
            var existingCustomer = await _dataRepository.GetCustomerByIdAsync(idGuid);
            if (existingCustomer == null)
            {
                throw new NotFoundException();
            }
            customer.Id = idGuid;
            await _dataRepository.OverwriteCustomerAsync(customer);
        }
    }
}
