using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Models;
using CustomerApi.Business.Services.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CreateCustomerService _createCustomerService;
        private readonly GetCustomerService _getCustomerService;
        private readonly UpdateCustomerService _updateConsumerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            CreateCustomerService createCustomerService,
            GetCustomerService getCustomerService,
            UpdateCustomerService updateConsumerService,
            ILogger<CustomerController> logger)
        {
            _createCustomerService = createCustomerService;
            _getCustomerService = getCustomerService;
            _updateConsumerService = updateConsumerService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetAsync(string id)
        {
            try
            {
                _logger.LogInformation("Test message for {id}", id);
                var customer = await _getCustomerService.GetCustomerAsync(id);
                return new OkObjectResult(customer);
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(string id, CustomerModel customer)
        {
            try
            {
                _logger.LogInformation("Test message for {id}", id);
                await _updateConsumerService.UpdateCustomerAsync(id, customer);
                return new OkResult();
            }
            catch (NotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostAsync([FromBody] CustomerModel value)
        {
            var result = await _createCustomerService.CreateCustomerAsync(value);
            return new CreatedAtActionResult(nameof(GetAsync), "Customer", new { id = result.Id }, null);
        }
    }
}