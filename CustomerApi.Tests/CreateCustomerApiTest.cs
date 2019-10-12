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
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
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
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/customer/", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Headers.Location.Should().BeNull();
            var responseString = await result.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseString);
            responseContent.Should().HaveCount(5);
            responseContent.Should().ContainKeys("type", "errors", "title", "status", "traceId");
            responseContent["title"]?.ToString().Should().Be("One or more validation errors occurred.");
            responseContent["status"]?.ToString().Should().Be("400");
            responseContent["traceId"].Should().NotBeNull();
            var firstNameErrors = responseContent.Value<JObject>("errors").Value<JArray>("FirstName");
            firstNameErrors.Should().HaveCount(1);
            firstNameErrors[0].ToString().Should().Be("The FirstName field is required.");
        }

        [Fact]
        public async Task CreateConsumer_MissingRequired_LastName_Fails()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                firstName = "TestFirst",
                emailAddress = "test2@test.pandora.net"
            };
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/customer/", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Headers.Location.Should().BeNull();
            var responseString = await result.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseString);
            responseContent.Should().HaveCount(5);
            responseContent.Should().ContainKeys("type", "errors", "title", "status", "traceId");
            responseContent["title"]?.ToString().Should().Be("One or more validation errors occurred.");
            responseContent["status"]?.ToString().Should().Be("400");
            responseContent["traceId"].Should().NotBeNull();
            var lastNameErrors = responseContent.Value<JObject>("errors").Value<JArray>("LastName");
            lastNameErrors.Should().HaveCount(1);
            lastNameErrors[0].ToString().Should().Be("The LastName field is required.");
        }

        [Fact]
        public async Task CreateConsumer_MissingRequired_EmailAddress_Fails()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                firstName = "TestFirst",
                lastName = "TestLast"
            };
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/customer/", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Headers.Location.Should().BeNull();
            var responseString = await result.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseString);
            responseContent.Should().HaveCount(5);
            responseContent.Should().ContainKeys("type", "errors", "title", "status", "traceId");
            responseContent["title"]?.ToString().Should().Be("One or more validation errors occurred.");
            responseContent["status"]?.ToString().Should().Be("400");
            responseContent["traceId"].Should().NotBeNull();
            var emailAddressErrors = responseContent.Value<JObject>("errors").Value<JArray>("EmailAddress");
            emailAddressErrors.Should().HaveCount(1);
            emailAddressErrors[0].ToString().Should().Be("The EmailAddress field is required.");
        }

    }
}
