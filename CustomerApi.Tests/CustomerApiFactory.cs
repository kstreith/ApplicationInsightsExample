using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace CustomerApi.Tests
{
    public class CustomerApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder? builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
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

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);
            Bootstrap.InitializeApplication(host).Wait();
            return host;

        }
        //protected override TestServer CreateHost(IWebHostBuilder builder)
        //{
        //    var server = base.CreateServer(builder);
        //    server.Host
        //    Bootstrap.InitializeApplication(server.Host).Wait();
        //    return server;
        //}
    }
}
