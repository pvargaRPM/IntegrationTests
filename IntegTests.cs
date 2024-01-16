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
        public async Task GetRead()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/turvoOtherDocument/download/4c452066-6c66-4edc-9a8c-e1fa2078cac7";

            // Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
        [Fact, TestPriority(1)]
        public async Task PostUpdate()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string postUri = $"{_httpClientFixture.BaseUrl}/api/turvoOtherDocument/withAttachments";

            var postRequestContent = TestHelper.GetHttpContentFromSampleData("SOMEDOC.pdf");
            postRequestContent.Headers.Add("Authorization", "jF9gOMjU7ZgH0WL1xqbHC4SOFVDj8FuXlSdwBUoAAcg=");

            // Act
            var putResponse = await client.PostAsync(postUri, postRequestContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
        }
    }
}