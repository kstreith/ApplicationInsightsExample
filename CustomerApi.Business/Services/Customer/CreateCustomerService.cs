using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using CustomerApi.Business.Services.ValueTypes;
using CustomerApi.Business.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class CreateCustomerService
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger<CreateCustomerService> _logger;
        private readonly BusinessSimulationSetting _setting;
        private long _counter;

        public CreateCustomerService(IDataRepository dataRepository,
            ILogger<CreateCustomerService> logger,
            BusinessSimulationSetting setting)
        {
            _dataRepository = dataRepository;
            _logger = logger;
            _setting = setting;
        }

        public async Task<IdentifiableResult<CustomerModel>> CreateCustomerAsync(CustomerModel customer)
        {
            if (customer.BirthDay == null)
            {
                _logger.LogInformation($"{nameof(customer.BirthDay)} field set to null");
            }
            if (customer.BirthMonth == null)
            {
                _logger.LogInformation($"{nameof(customer.BirthMonth)} field set to null");
            }
            var newValue = Interlocked.Increment(ref _counter);
            if (newValue % _setting.InternalExceptionRate == 0)
            {
                throw new InvalidOperationException("Fake creation exception");
            }
            var newId = Guid.NewGuid();
            customer.Id = newId;
            await _dataRepository.CreateCustomerAsync(customer);
            return new IdentifiableResult<CustomerModel>(newId.ToString(), customer);
        } 
    }
}
