using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class GetCustomerService
    {
        private readonly IDataRepository _dataRepository;

        public GetCustomerService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<CustomerModel> GetCustomerAsync(string id)
        {
            if (!Guid.TryParse(id, out var idGuid)) {
                throw new NotFoundException();
            }
            var customer = await _dataRepository.GetCustomerByIdAsync(idGuid);
            if (customer == null)
            {
                throw new NotFoundException();
            }
            return customer;
        }
    }
}
