using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using TemperatureFunctions.Grafana;
using TemperatureFunctions.Grafana.Extensions;

namespace TemperatureFunctions
{
    public static class GetTemperatures
    {
        [FunctionName("GetTemperatures")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetTemperatures/query")]HttpRequestMessage req, TraceWriter log)
        {
            long value = 1;
            var date = DateTime.Now;

            var timeseries = new TimeSeries
            {
                Target = "target",
                Datapoints = new long[][] { new long[] { value, date.MillisecondsFromUnixEpoch() } }
            };

            var json = JsonConvert.SerializeObject(timeseries);

            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }
}
