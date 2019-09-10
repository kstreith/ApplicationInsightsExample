using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class CreateCustomerApiTest : IClassFixture<CustomerApiFactory>
    {
        private readonly CustomerApiFactory _webApplicationFactory;

        public CreateCustomerApiTest(CustomerApiFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task CreateConsumer_Works()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                firstName = "TestFirst",
                lastName = "TestLast",
                emailAddress = "test2@test.pandora.net"
            };
            var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/customer/", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Headers.Location.ToString().Should().StartWith("http://localhost/api/Customer/");
            var responseContent = await result.Content.ReadAsStringAsync();
            responseContent.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateConsumer_MissingRequired_FirstName_Fails()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                lastName = "TestLast",
                emailAddress = "test2@test.pandora.net"
            };
            var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/customer/", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Headers.Location.Should().BeNull();
            var responseString = await result.Content.ReadAsStringAsync();
            //responseString.Should().Be("");
            var responseContent = JObject.Parse(responseString);
            responseContent.Should().HaveCount(4);
            responseContent.Should().ContainKeys("errors", "title", "status", "traceId");
            responseContent["title"]?.ToString().Should().Be("One or more validation errors occurred.");
            responseContent["status"]?.ToString().Should().Be("400");
            responseContent["traceId"].Should().NotBeNull();
            var firstNameErrors = responseContent.Value<JObject>("errors").Value<JArray>("FirstName");
            firstNameErrors.Should().HaveCount(1);
            firstNameErrors[0].ToString().Should().Be("The FirstName field is required.");
        }

    }
}
