using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CustomerApi.Tests
{
    public class CustomerApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var dict = new Dictionary<string, string>
                {
                    ["Storage"] = "InMemory"
                };
                config.AddInMemoryCollection(dict);
            });
            base.ConfigureWebHost(builder);
        }
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);
            Bootstrap.InitializeApplication(server.Host).Wait();
            return server;
        }
    }
}
