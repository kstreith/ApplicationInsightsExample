using CustomerApi.Business.Services.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Config
{
    public static class DependencyExtensions
    {
        public static void RegisterAppDependencies(this IServiceCollection services)
        {
            services.AddTransient<CreateSampleCustomerDataService, CreateSampleCustomerDataService>();
            services.AddTransient<CreateCustomerService, CreateCustomerService>();
            services.AddTransient<GetCustomerService, GetCustomerService>();
            services.AddTransient<UpdateCustomerService, UpdateCustomerService>();
            services.AddTransient<GetRandomCustomerService, GetRandomCustomerService>();
        }
    }
}
