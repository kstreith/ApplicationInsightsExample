using CustomerApi.Business.Models;
using System;

namespace DataRepository.Cosmos
{
    public class CustomerDocument
    {
        public string id => $"{PartitionKey}|CustomerDocument";
        public string PartitionKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthDay { get; set; }

        public CustomerDocument()
        {

        }

        public CustomerDocument(CustomerModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            PartitionKey = customer.Id.Value.ToString();
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            EmailAddress = customer.EmailAddress;
            BirthMonth = customer.BirthMonth;
            BirthDay = customer.BirthDay;
        }

        public CustomerModel ToCustomerModel()
        {
            return new CustomerModel
            {
                Id = Guid.Parse(PartitionKey),
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                BirthMonth = BirthMonth,
                BirthDay = BirthDay
            };
        }
    }
}
