using CustomerApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Business.Interfaces
{
    public interface IDataRepository
    {
        Task<CustomerModel> GetCustomerByIdAsync(Guid customerId);
        Task CreateCustomerAsync(CustomerModel customer);
        Task OverwriteCustomerAsync(CustomerModel customer);
        Task<List<string>> GetRandomCustomerIdsAsync();
    }
}
