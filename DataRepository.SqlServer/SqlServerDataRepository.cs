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
            await _dbConnection.ExecuteAsync(@"INSERT Customer(Id, FirstName, LastName, EmailAddress, BirthMonth, BirthDay) VALUES (@Id, @FirstName, @LastName, @EmailAddress, @BirthMonth, @BirthDay)",
                customer);
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(Guid customerId)
        {
            var results = await _dbConnection.QueryAsync<CustomerModel>("SELECT Id, FirstName, LastName, EmailAddress, BirthMonth, BirthDay FROM Customer WHERE Id=@Id", new { Id = customerId });
            return results.FirstOrDefault();
        }

        public async Task<List<string>> GetRandomCustomerIdsAsync()
        {
            var randomGuid = Guid.NewGuid();
            var results = await _dbConnection.QueryAsync<CustomerModel>("SELECT TOP 100 Id FROM Customer WHERE Id > @Id", new { Id = randomGuid });
            var ids = results.Select(x => x.Id.ToString()).ToList();
            if (ids.Count < 100)
            {
                var moreResults = await _dbConnection.QueryAsync<CustomerModel>("SELECT TOP 100 Id FROM Customer WHERE Id <= @Id", new { Id = randomGuid });
                var moreIds = moreResults.Select(x => x.Id.ToString());
                ids.AddRange(moreIds);
            }
            return ids;
        }

        public async Task OverwriteCustomerAsync(CustomerModel customer)
        {
            await _dbConnection.ExecuteAsync(@"UPDATE Customer(Id, FirstName, LastName, EmailAddress, BirthMonth, BirthDay) VALUES (@Id, @FirstName, @LastName, @EmailAddress, @BirthMonth, @BirthDay) WHERE Id = @Id",
                            customer);
        }
    }
}
 