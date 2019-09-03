using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository.Cosmos
{
    public static class CosmosDependencyExtensions
    {
        public static void AddCosmosDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(new CosmosConnection(config["Cosmos:Endpoint"], config["Cosmos:AuthorizationKey"]));
        }
    }
}
