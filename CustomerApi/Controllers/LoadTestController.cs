using CustomerApi.Business.Services.Customer;
using CustomerApi.Business.ValueTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadTestController : ControllerBase
    {
        private readonly GetRandomCustomerService _getRandomCustomerService;

        public LoadTestController(
            GetRandomCustomerService getRandomCustomerService)
        {
            _getRandomCustomerService = getRandomCustomerService;
        }

        [HttpGet("randomcustomer")]
        public async Task<ActionResult<RandomListOfIdsResult>> GetAsync()
        {
            var result = await _getRandomCustomerService.GetRandomCustomerIdsAsync();
            return new OkObjectResult(result);
        }
    }
}