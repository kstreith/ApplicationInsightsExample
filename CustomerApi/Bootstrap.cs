using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Services.Customer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CustomerApi
{
    public static class Bootstrap
    {
        public static async Task InitializeApplication(IWebHost host)
        {
            var initializer = host.Services.GetService<IDataRepositoryInitializer>();
            if (initializer != null)
            {
                await initializer.InitAsync();
            }
            var config = host.Services.GetService<IConfiguration>();
            if (config.GetValue<bool>("InitializeWithSampleData"))
            {
                var createDataService = host.Services.GetService<CreateSampleCustomerDataService>();
                await createDataService.CreateSampleData();
            }
        }
    }
}
