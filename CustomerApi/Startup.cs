using System;
using System.Linq;
using CustomerApi.Config;
using DataRepository.Cosmos;
using DataRepository.InMemory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace CustomerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.RegisterAppDependencies();
            if (string.Equals(Configuration["Storage"], "Cosmos", StringComparison.InvariantCultureIgnoreCase))
            {
                services.RegisterCosmosDependencies(Configuration);
            }
            else
            {
                services.RegisterInMemoryDependencies(Configuration);
            }
            services.PostConfigureAll<LoggerFilterOptions>(action =>
            {
                var matchingRule = action.Rules.SingleOrDefault(x => x.ProviderName == typeof(ApplicationInsightsLoggerProvider).FullName);
                if (matchingRule != null)
                {
                    action.Rules.Remove(matchingRule);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
