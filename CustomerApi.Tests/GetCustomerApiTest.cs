using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class TestFactory : WebApplicationFactory<Startup>
    {
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);
            Bootstrap.InitializeApplication(server.Host).Wait();
            return server;
        }
    }
    public class GetCustomerApiTest : IClassFixture<TestFactory>
    {
        private readonly TestFactory _webApplicationFactory;

        public GetCustomerApiTest(TestFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task GetCustomer_Works()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();

            // Act
            var response = await client.GetAsync("/api/customer/88f37d26-6616-4598-8792-e3bb9b814c72");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be(@"{""id"":""88f37d26-6616-4598-8792-e3bb9b814c72"",""firstName"":""TestFirst"",""lastName"":""TestLast"",""emailAddress"":""test@test.itsnull.com""}");
        }
    }
}
