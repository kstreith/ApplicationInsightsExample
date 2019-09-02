using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using System;

namespace CustomerApi.Business.Services.Customer
{
    public class GetCustomerService
    {
        private readonly IDataRepository _dataRepository;

        public GetCustomerService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public CustomerModel GetCustomer(string id)
        {
            if (!Guid.TryParse(id, out var idGuid)) {
                throw new NotFoundException();
            }
            var customer = _dataRepository.GetCustomerById(idGuid);
            if (customer == null)
            {
                throw new NotFoundException();
            }
            return customer;
        }
    }
}
