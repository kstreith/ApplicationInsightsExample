using CustomerApi.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository.InMemory
{
    public static class InMemoryDependencyExtensions
    {
        public static void RegisterInMemoryDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IDataRepository, InMemoryDataRepository>();
        }
    }
}
