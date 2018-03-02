using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using TemperatureFunctions.CQRS;
using TemperatureFunctions.Dto;

namespace TemperatureFunctions
{
    public static class RegisterTemperatures
    {
        [FunctionName("RegisterTemperatures")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
            TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");

                var data = await req.Content.ReadAsStringAsync();
                var temperature = JsonConvert.DeserializeObject<TemperatureRegistration>(data);

                var commandExecutor = new CommandExecutor();
                await commandExecutor.Handle(temperature);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                log.Error("RegisterTemperatures crashed", e);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
