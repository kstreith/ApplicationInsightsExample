using CustomerApi.Business.Interfaces;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace DataRepository.SqlServer
{
    public static class SqlServerDependencyExtensions
    {
        public static void RegisterSqlServerDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDataRepository, SqlServerDataRepository>();
            services.AddScoped<IDataRepositoryInitializer, SqlServerDataRepositoryInitializer>();
            services.AddScoped<IDbConnection>(db => new SqlConnection(config["SqlServer:ConnectionString"]));
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(config["SqlServer:ConnectionString"])
                    .ScanIn(typeof(SqlServerDependencyExtensions).Assembly).For.Migrations());
        }
    }
}
