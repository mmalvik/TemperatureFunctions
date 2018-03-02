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
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "GetTemperatures/query")]HttpRequestMessage req, TraceWriter log)
        {
            var requestData = await req.Content.ReadAsStringAsync();
            log.Info($"GetTemperatures/query called with: \n {requestData}");

            var dayMilliseconds = 86400000;
            long value = 1;
            var date = DateTime.Now;

            var timeseries = new TimeSeries
            {
                Target = "temperature",
                Datapoints = new long[][]
                {
                    new long[] { 1, date.MillisecondsFromUnixEpoch() - (2*dayMilliseconds) },
                    new long[] { 2, date.MillisecondsFromUnixEpoch() - dayMilliseconds },
                    new long[] { 3, date.MillisecondsFromUnixEpoch() }
                }
            };

            var json = JsonConvert.SerializeObject(timeseries);

            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }
}
