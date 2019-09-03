using CustomerApi.Business.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CustomerApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            var initializer = host.Services.GetService<IDataRepositoryInitializer>();
            if (initializer != null)
            {
                await initializer.InitAsync();
            }
            var config = host.Services.GetService<IConfiguration>();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices((context, services) =>
                {
                    var config = context.Configuration;
                    services.AddApplicationInsightsTelemetry(options =>
                    {
                        config.Bind("ApplicationInsights", options);
                    });
                    services.AddApplicationInsightsTelemetryProcessor<CustomTelemetryProcessor>();
                    var instrumentationKey = config["ApplicationInsights:InstrumentationKey"];
                    //services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitializer>();
                    //services.AddLogging((loggingBuilder) =>
                    //{
                    //    //loggingBuilder.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
                    //    //loggingBuilder.AddConfiguration(config.GetSection("Logging"));
                    //    loggingBuilder.AddApplicationInsights();
                    //});
                });
    }
}
