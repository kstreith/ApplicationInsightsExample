using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using Dapper;

namespace DataRepository.SqlServer
{
    public class SqlServerDataRepository : IDataRepository
    {
        private readonly IDbConnection _dbConnection;

        public SqlServerDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateCustomerAsync(CustomerModel customer)
        {
            await _dbConnection.ExecuteAsync(@"INSERT Customer(Id, FirstName, LastName, EmailAddress) VALUES (@Id, @FirstName, @LastName, @EmailAddress)",
                customer);
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(Guid customerId)
        {
            var results = await _dbConnection.QueryAsync<CustomerModel>("SELECT Id, FirstName, LastName, EmailAddress FROM Customer WHERE Id=@Id", new { Id = customerId });
            return results.FirstOrDefault();
        }

        public Task<List<string>> GetRandomCustomerIdsAsync()
        {
            throw new NotImplementedException();
        }

        public Task OverwriteCustomerAsync(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
