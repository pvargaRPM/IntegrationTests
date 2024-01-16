using IntegrationTests.Ordering;
using System.Net;
using System.Text.Json;

namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.Ordering.PriorityOrderer", "Integration.Tests")]
    public class IntegTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;

        public IntegTests(HttpClientFixture httpClassFixture)
        {
            _httpClientFixture = httpClassFixture;
        }

        [Fact, TestPriority(0)]
        public async Task Test1()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api";

            // Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}