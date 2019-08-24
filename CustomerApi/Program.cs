using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Logging.Configuration;

namespace CustomerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
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
