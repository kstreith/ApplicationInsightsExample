using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class CreateSampleCustomerDataService
    {
        private readonly IDataRepository _dataRepository;

        public CreateSampleCustomerDataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task CreateSampleData()
        {
            var id = new Guid("88f37d26-6616-4598-8792-e3bb9b814c72");
            var existingCustomer = await _dataRepository.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                var customer = new CustomerModel
                {
                    Id = id,
                    FirstName = "TestFirst",
                    LastName = "TestLast",
                    EmailAddress = "test@test.itsnull.com"
                };
                await _dataRepository.CreateCustomerAsync(customer);
            }
        }
    }
}
