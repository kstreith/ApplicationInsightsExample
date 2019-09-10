using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace CustomerApi.Tests
{
    public class CustomerApiFactory : WebApplicationFactory<Startup>
    {
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);
            Bootstrap.InitializeApplication(server.Host).Wait();
            return server;
        }
    }
}
