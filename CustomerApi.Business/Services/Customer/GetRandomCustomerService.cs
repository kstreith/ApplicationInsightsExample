using CustomerApi.Business.Exceptions;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.ValueTypes;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Business.Services.Customer
{
    public class GetRandomCustomerService
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger<GetRandomCustomerService> _logger;

        public GetRandomCustomerService(IDataRepository dataRepository,
            ILogger<GetRandomCustomerService> logger)
        {
            _dataRepository = dataRepository;
            _logger = logger;
        }

        public async Task<RandomListOfIdsResult> GetRandomCustomerIdsAsync()
        {
            var ids = await _dataRepository.GetRandomCustomerIdsAsync();
            _logger.LogInformation("Returned {count} random customer ids", ids.Count);
            return new RandomListOfIdsResult { Count = ids.Count, Values = ids };
        }
    }
}
