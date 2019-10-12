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
    public class UpdateCustomerApiTest : IClassFixture<CustomerApiFactory>
    {
        private readonly CustomerApiFactory _webApplicationFactory;

        public UpdateCustomerApiTest(CustomerApiFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task UpdateConsumer_Works()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                firstName = "TestFirstUpdate",
                lastName = "TestLastUpdate",
                emailAddress = "test2@test.pandora.net"
            };
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PutAsync("/api/customer/88f37d26-6616-4598-8792-e3bb9b814c72", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await result.Content.ReadAsStringAsync();
            responseContent.Should().BeEmpty();
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("26cf4f56-b8f2-4034-b9c8-86d8e242492a")]
        public async Task UpdateConsumer_NotFoundId_Fails(string id)
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                firstName = "TestFirstUpdate",
                lastName = "TestLastUpdate",
                emailAddress = "test2@test.pandora.net"
            };
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PutAsync($"/api/customer/{id}", request);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var responseContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(await result.Content.ReadAsStringAsync());
            responseContent.Should().HaveCount(4);
            responseContent.Should().ContainKeys("type", "title", "status", "traceId");
            responseContent["type"].Should().Be("https://tools.ietf.org/html/rfc7231#section-6.5.4");
            responseContent["title"].Should().Be("Not Found");
            responseContent["status"].Should().Be("404");
            responseContent["traceId"].Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task UpdateConsumer_MissingRequired_FirstName_Fails()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                lastName = "TestLastUpdate",
                emailAddress = "test2@test.pandora.net"
            };
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PutAsync("/api/customer/88f37d26-6616-4598-8792-e3bb9b814c72", request);

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
        public async Task UpdateConsumer_MissingRequired_LastName_Fails()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                firstName = "TestFirst",
                emailAddress = "test2@test.pandora.net"
            };
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PutAsync("/api/customer/88f37d26-6616-4598-8792-e3bb9b814c72", request);

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
        public async Task UpdateConsumer_MissingRequired_EmailAddress_Fails()
        {
            // Arrange
            var client = _webApplicationFactory.CreateDefaultClient();
            var requestObj = new
            {
                firstName = "TestFirst",
                lastName = "TestLast"
            };
            using var request = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var result = await client.PutAsync("/api/customer/88f37d26-6616-4598-8792-e3bb9b814c72", request);

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
