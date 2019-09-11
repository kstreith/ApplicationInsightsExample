using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class GetRandomCustomerApiTest : IClassFixture<CustomerApiFactory>
    {
        private readonly CustomerApiFactory _webApplicationFactory;

        public GetRandomCustomerApiTest(CustomerApiFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task GetRandomCustomer_Works()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();

            // Act
            var response = await client.GetAsync("/api/loadtest/randomcustomer/");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseContent);
            responseObject.Should().HaveCount(2);
            responseObject.Should().ContainKeys("count", "values");
            responseObject.Value<int>("count").Should().BeGreaterThan(0);
            var values = responseObject.Value<JArray>("values");
            values.Should().HaveCountGreaterThan(0);
            values[0].Value<string>().Should().NotBeNullOrWhiteSpace();
        }
    }
}
