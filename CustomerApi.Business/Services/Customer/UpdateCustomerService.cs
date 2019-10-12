using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using CustomerApi.Business.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class UpdateCustomerService
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger<UpdateCustomerService> _logger;
        private readonly BusinessSimulationSetting _setting;
        private static long _counter;

        public UpdateCustomerService(IDataRepository dataRepository,
            ILogger<UpdateCustomerService> logger,
            BusinessSimulationSetting setting)
        {
            _dataRepository = dataRepository;
            _logger = logger;
            _setting = setting;
        }

        public async Task UpdateCustomerAsync(string id, CustomerModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            if (!Guid.TryParse(id, out var idGuid)) {
                throw new NotFoundException();
            }
            var newValue = Interlocked.Increment(ref _counter);
            if (newValue % _setting.InternalExceptionRate == 0)
            {
                throw new InvalidOperationException("Fake update exception");
            }
            var existingCustomer = await _dataRepository.GetCustomerByIdAsync(idGuid);
            if (existingCustomer == null)
            {
                throw new NotFoundException();
            }
            if (existingCustomer.BirthDay != customer.BirthDay)
            {
                _logger.LogInformation("{field} field updated to {value}", nameof(customer.BirthDay), customer.BirthDay);
            }
            if (existingCustomer.BirthMonth != customer.BirthMonth)
            {
                 _logger.LogInformation("{field} field updated to {value}", nameof(customer.BirthMonth), customer.BirthMonth);
            }
            customer.Id = idGuid;
            await _dataRepository.OverwriteCustomerAsync(customer);
        }
    }
}
