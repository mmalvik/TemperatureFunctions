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
using TemperatureFunctions.Dto;
using TemperatureFunctions.Grafana;

namespace TemperatureFunctions
{
    public static class GetTemperatures
    {
        [FunctionName("GetTemperatures")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "GetTemperatures/query")]HttpRequestMessage req, TraceWriter log)
        {
            var requestData = await req.Content.ReadAsStringAsync();
            var request = TimeSerieRequest.FromJson(requestData);

            var queryExecutor = new QueryExecutor();
            var query = new SqlQuerySpec("SELECT c.temperature, c._ts FROM c WHERE c._ts >= @timestampFrom AND c._ts <= @timestampTo",
                new SqlParameterCollection
                {
                    new SqlParameter("@timestampFrom", request.Range.From.ToUnixTimeSeconds()),
                    new SqlParameter("@timestampTo", request.Range.To.ToUnixTimeSeconds())
                });

            var result = await queryExecutor.Execute<TemperatureRegistration>(query);

            var timeseries = AddToTimeSeriesData("tempOne", result, 1);
            var timeseriesTwo = AddToTimeSeriesData("tempTwo", result, 2);

            var list = new List<TimeSeries> {timeseries, timeseriesTwo};

            var json = JsonConvert.SerializeObject(list.ToArray());

            return new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

        private static TimeSeries AddToTimeSeriesData(string target, IEnumerable<TemperatureRegistration> registrations, int factor)
        {
            var timeSeries = new TimeSeries();
            var datapoint = new List<List<long>>();
            foreach (var registration in registrations)
            {
                var tsMilliseconds = registration.Ts * 1000;
                datapoint.Add(new List<long>{ registration.Temperature*factor, tsMilliseconds});    
            }

            timeSeries.Target = target;
            var points = datapoint.Select(x => x.ToArray()).ToArray();
            timeSeries.Datapoints = points;

            return timeSeries;
        }
    }
}
