using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Models;
using CustomerApi.Business.Services.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CreateCustomerService _createCustomerService;
        private readonly GetCustomerService _getCustomerService;

        public CustomerController(
            CreateCustomerService createCustomerService,
            GetCustomerService getCustomerService)
        {
            _createCustomerService = createCustomerService;
            _getCustomerService = getCustomerService;
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerModel> Get(string id)
        {
            try
            {
                var customer = _getCustomerService.GetCustomer(id);
                return new OkObjectResult(customer);
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        public ActionResult<CustomerModel> Post([FromBody] CustomerModel value)
        {
            var result = _createCustomerService.CreateCustomer(value);
            return new CreatedAtActionResult(nameof(Get), nameof(CustomerController), new { id = result.Id }, result);
        }
    }
}