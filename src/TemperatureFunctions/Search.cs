using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using TemperatureFunctions.Grafana;

namespace TemperatureFunctions
{
    public static class Search
    {
        [FunctionName("Search")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "GetTemperatures/search")]HttpRequestMessage req, TraceWriter log)
        {
            var requestData = await req.Content.ReadAsStringAsync();
            log.Info($"GetTemperatures/query called with: \n {requestData}");

            var targets = new List<string>{ GrafanaConstants.TemperatureTarget };
            var json = JsonConvert.SerializeObject(targets);

            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }
}
