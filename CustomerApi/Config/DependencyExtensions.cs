using CustomerApi.Business.Services.Customer;
using CustomerApi.Business.Settings;
using DataRepository.Cosmos;
using DataRepository.InMemory;
using DataRepository.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CustomerApi.Config
{
    public static class DependencyExtensions
    {
        public static void RegisterAppDependencies(this IServiceCollection services, IConfiguration config)
        {
            if (string.Equals(config["Storage"], "Cosmos", StringComparison.InvariantCultureIgnoreCase))
            {
                services.RegisterCosmosDependencies(config);
            }
            else if (string.Equals(config["Storage"], "SqlServer", StringComparison.InvariantCultureIgnoreCase))
            {
                services.RegisterSqlServerDependencies(config);
            }
            else
            {
                services.RegisterInMemoryDependencies(config);
            }
            services.AddTransient<CreateSampleCustomerDataService, CreateSampleCustomerDataService>();
            services.AddTransient<CreateCustomerService, CreateCustomerService>();
            services.AddTransient<GetCustomerService, GetCustomerService>();
            services.AddTransient<UpdateCustomerService, UpdateCustomerService>();
            services.AddTransient<GetRandomCustomerService, GetRandomCustomerService>();
            services.AddSingleton<BusinessSimulationSetting>();
        }
    }
}
