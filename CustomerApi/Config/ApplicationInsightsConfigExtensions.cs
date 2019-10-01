﻿using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Config
{
    public static class ApplicationInsightsConfigExtensions
    {
        public static void ConfigureApplicationInsights(this IServiceCollection services, IConfiguration config)
        {
            services.AddApplicationInsightsTelemetry(options =>
            {
                config.Bind("ApplicationInsights", options);
            });
            services.AddApplicationInsightsTelemetryProcessor<ScrubCustomerIdTelemetryProcessor>();
            var instrumentationKey = config["ApplicationInsights:InstrumentationKey"];
            services.AddSingleton<ITelemetryInitializer, FakeApiUserTelemetryInitializer>();
            services.PostConfigureAll<LoggerFilterOptions>(action =>
            {
                var matchingRule = action.Rules.SingleOrDefault(x => x.ProviderName == typeof(ApplicationInsightsLoggerProvider).FullName);
                if (matchingRule != null)
                {
                    action.Rules.Remove(matchingRule);
                }
            });
        }
    }
}
