using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Services.Customer;
using DataRepository.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Config
{
    public static class DependencyExtensions
    {
        public static void RegisterAppDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IDataRepository, InMemoryDataRepository>();
            services.AddTransient<CreateCustomerService, CreateCustomerService>();
            services.AddTransient<GetCustomerService, GetCustomerService>();
        }
    }
}
