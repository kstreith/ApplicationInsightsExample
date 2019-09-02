using CustomerApi.Business.Models;
using System;
using System.Collections.Generic;

namespace CustomerApi.Business.Interfaces
{
    public interface IDataRepository
    {
        CustomerModel GetCustomerById(Guid customerId);
        void CreateCustomer(CustomerModel customer);
        void OverwriteCustomer(CustomerModel customer);
        List<CustomerInteractionModel> GetInteractions(int page);
        Guid LookupCustomerIdByEmail(string emailAddress);
    }
}
