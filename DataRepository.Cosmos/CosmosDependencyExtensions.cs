using CustomerApi.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository.Cosmos
{
    public static class CosmosDependencyExtensions
    {
        public static void RegisterCosmosDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(_ => new CosmosConnection(config["Cosmos:Endpoint"], config["Cosmos:AuthorizationKey"]));
            services.AddSingleton<IDataRepository, CosmosDataRepository>();
            services.AddSingleton<IDataRepositoryInitializer, CosmosDataRepositoryInitializer>();
        }
    }
}
