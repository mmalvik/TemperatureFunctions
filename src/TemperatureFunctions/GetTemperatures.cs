using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TemperatureFunctions.CQRS;
using TemperatureFunctions.CQRS.Queries;
using TemperatureFunctions.Dto;
using TemperatureFunctions.Grafana;

namespace TemperatureFunctions
{
    public static class GetTemperatures
    {
        [FunctionName("GetTemperatures")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "GetTemperatures/query")]HttpRequestMessage req, TraceWriter log)
        {
            var requestData = await req.Content.ReadAsStringAsync();
            var request = TimeSerieRequest.FromJson(requestData);

            var queryExecutor = new QueryExecutor();
            var query = new TimeSeriesQuery(request.Range.From, request.Range.To, request.MaxDataPoints);

            var temperatureRegistrations = await queryExecutor.Execute<TemperatureRegistration>(query.Sql);

            var timeseries = AddToTimeSeriesData(GrafanaConstants.TemperatureTarget, temperatureRegistrations);

            var json = JsonConvert.SerializeObject(new List<TimeSeries> { timeseries });

            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

        private static TimeSeries AddToTimeSeriesData(string target, IEnumerable<TemperatureRegistration> registrations)
        {
            var timeSeries = new TimeSeries();
            var datapoint = new List<List<long>>();
            foreach (var registration in registrations)
            {
                var epochTimestampMilliseconds = registration.Ts * 1000;
                datapoint.Add(new List<long>{ registration.Temperature, epochTimestampMilliseconds });    
            }

            timeSeries.Target = target;
            timeSeries.Datapoints = datapoint.Select(x => x.ToArray()).ToArray();

            return timeSeries;
        }
    }
}
