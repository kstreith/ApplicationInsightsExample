using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using CustomerApi.Business.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class GetCustomerService
    {
        private readonly IDataRepository _dataRepository;
        private readonly BusinessSimulationSetting _setting;
        private static long _counter;

        public GetCustomerService(IDataRepository dataRepository,
            BusinessSimulationSetting setting)
        {
            _dataRepository = dataRepository;
            _setting = setting;
        }

        public async Task<CustomerModel> GetCustomerAsync(string id)
        {
            if (!Guid.TryParse(id, out var idGuid)) {
                throw new NotFoundException();
            }
            var newValue = Interlocked.Increment(ref _counter);
            if (newValue % _setting.InternalExceptionRate == 0)
            {
                throw new InvalidOperationException("Fake retrieval exception");
            }
            var customer = await _dataRepository.GetCustomerByIdAsync(idGuid);
            if (customer == null)
            {
                throw new NotFoundException();
            }
            return customer;
        }
    }
}
