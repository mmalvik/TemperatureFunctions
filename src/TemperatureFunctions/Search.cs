using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace TemperatureFunctions
{
    public static class Search
    {
        [FunctionName("Search")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "GetTemperatures/search")]HttpRequestMessage req, TraceWriter log)
        {
            var requestData = await req.Content.ReadAsStringAsync();
            log.Info($"GetTemperatures/query called with: \n {requestData}");

            var json = JsonConvert.SerializeObject(new TargetObj {Target = "temperature"});

            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

        private class TargetObj
        {
            [JsonProperty("target")]
            public string Target { get; set; }

        }
    }
}
