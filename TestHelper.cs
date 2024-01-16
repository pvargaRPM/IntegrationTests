using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    internal class TestHelper
    {

        public static HttpContent GetHttpContentFromSampleData(string jsonFileName)
        {
            string json = File.ReadAllText($"SampleData/{jsonFileName}");
            var postContent = GetHttpContentFromJson(json);

            return postContent;
        }

        public static HttpContent GetHttpContentFromJson(string json)
        {
            var postContent = new StringContent(json)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            };
            return postContent;
        }

        public static async Task<dynamic> GetContentFromHttpResponse(HttpResponseMessage responseMessage)
        {
            string responseContent = await responseMessage.Content.ReadAsStringAsync();
            dynamic content = JsonConvert.DeserializeObject<dynamic>(responseContent);
            return content;
        }
    }
}

