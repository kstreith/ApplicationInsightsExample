using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class GetCustomerApiTest : IClassFixture<CustomerApiFactory>
    {
        private readonly CustomerApiFactory _webApplicationFactory;

        public GetCustomerApiTest(CustomerApiFactory webApplicationFactory)
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
            responseContent.Should().Be(@"{""id"":""88f37d26-6616-4598-8792-e3bb9b814c72"",""firstName"":""TestFirst"",""lastName"":""TestLast"",""emailAddress"":""test@test.itsnull.com"",""birthMonth"":null,""birthDay"":null}");
        }

        [Fact]
        public async Task GetCustomer_NotFound()
        {
            // Arrange
            var notFoundId = "e722bc15-59d7-4830-acd4-36e987f47e8d";
            var client = _webApplicationFactory.CreateDefaultClient();

            // Act
            var response = await client.GetAsync($"/api/customer/{notFoundId}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var responseContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            responseContent.Should().HaveCount(4);
            responseContent.Should().ContainKeys("type", "title", "status", "traceId");
            responseContent["type"].Should().Be("https://tools.ietf.org/html/rfc7231#section-6.5.4");
            responseContent["title"].Should().Be("Not Found");
            responseContent["status"].Should().Be("404");
            responseContent["traceId"].Should().NotBeNullOrWhiteSpace();
        }

    }
}
