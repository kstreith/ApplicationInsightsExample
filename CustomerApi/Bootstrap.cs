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
            using (var scope = host.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<IDataRepositoryInitializer>();
                if (initializer != null)
                {
                    await initializer.InitAsync();
                }
                var config = scope.ServiceProvider.GetService<IConfiguration>();
                if (config.GetValue<bool>("InitializeWithSampleData"))
                {
                    var createDataService = scope.ServiceProvider.GetService<CreateSampleCustomerDataService>();
                    await createDataService.CreateSampleData();
                }
            }
        }
    }
}
