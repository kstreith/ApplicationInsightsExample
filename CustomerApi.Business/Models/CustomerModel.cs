using System;

namespace CustomerApi.Business.Models
{
    public class CustomerModel
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
