using CustomerApi.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository.InMemory
{
    public static class InMemoryDependencyExtensions
    {
        public static void RegisterInMemoryDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IDataRepository, InMemoryDataRepository>();
        }
    }
}
