using CustomerApi.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;

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
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.RegisterAppDependencies(Configuration);
            services.ConfigureApplicationInsights(Configuration);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CA1822 // Mark members as static
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CA1822 // Mark members as static
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    "default", "api/{controller=Home}/{action=Get}/{id?}");
            });
            app.UseMiddleware<HomePageMiddleware>();
        }
    }

    public class HomePageMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public HomePageMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            if (httpContext.Request.Path == "/")
            {
                await httpContext.Response.WriteAsync("Customer API");
            }
            else
            {
                await _requestDelegate(httpContext);
            }
        }
    }
}
