using Azure;
using IntegrationTests.Ordering;
using System.Net;
using System.Text.Json;
using Xunit.Sdk;

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
            /*
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();

            var baseUrl = "https://staging-turvogateway.loadrpm.com";
            var token = "jF9gOMjU7ZgH0WL1xqbHC4SOFVDj8FuXlSdwBUoAAcg=";
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            var message = new HttpRequestMessage(HttpMethod.Get, "api/turvoOtherDocument/download/4c452066-6c66-4edc-9a8c-e1fa2078cac7")
            {
                Content = TestHelper.GetHttpContentFromSampleData("Attributes.json")
            };
            string getUri = $"{_httpClientFixture.BaseUrl}/api/turvoOtherDocument/download/4c452066-6c66-4edc-9a8c-e1fa2078cac7";

            message.Headers.Add("Authorization", token);
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://staging-turvogateway.loadrpm.com"),
            };

            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "VafRk0BXOvfx1QsuFT0rNkb8LdIeXpnaR+wf0iair2E=");

            var message = new HttpRequestMessage(HttpMethod.Get, "api/turvoOtherDocument/download/0179b96b-f120-44ba-adad-13e9b9fa0960");

            var result = await client.SendAsync(message);
            */

            var client = new HttpClient
            {
                BaseAddress = new Uri("https://staging-turvogateway.loadrpm.com"),
            };

            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "VafRk0BXOvfx1QsuFT0rNkb8LdIeXpnaR+wf0iair2E=");

            var message = new HttpRequestMessage(HttpMethod.Get, "api/turvoOtherDocument/download/0179b96b-f120-44ba-adad-13e9b9fa0960");

            // Act
            var response = await client.SendAsync(message);

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

            var body = await putResponse.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(body, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            _httpClientFixture.CreatedId = json.GetProperty("id").ToString();
        }
    }
}