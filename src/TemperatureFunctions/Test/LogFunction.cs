using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using TemperatureFunctions.Dto;

namespace TemperatureFunctions.Test
{
    public static class LogFunction
    {
        [FunctionName("LogFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");

                if (req == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }

                var data = await req.Content.ReadAsStringAsync();
                var temperature = JsonConvert.DeserializeObject<TemperatureRegistration>(data);

                log.Info($"Temperature: {temperature.Temperature} Time: {temperature.TimeStamp.Date}");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(data, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {
                log.Error("Function failed", e);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
