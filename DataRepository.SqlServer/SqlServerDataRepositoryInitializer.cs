using CustomerApi.Business.Interfaces;
using FluentMigrator.Runner;
using System.Threading.Tasks;

namespace DataRepository.SqlServer
{
    public class SqlServerDataRepositoryInitializer : IDataRepositoryInitializer
    {
        private readonly IMigrationRunner _runner;

        public SqlServerDataRepositoryInitializer(IMigrationRunner runner)
        {
            _runner = runner;
        }

        public Task InitAsync()
        {
            _runner.MigrateUp();
            return Task.CompletedTask;
        }
    }
}
