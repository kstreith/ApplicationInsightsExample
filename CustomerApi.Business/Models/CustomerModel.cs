using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Business.Models
{
    public class CustomerModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }
    }
}
