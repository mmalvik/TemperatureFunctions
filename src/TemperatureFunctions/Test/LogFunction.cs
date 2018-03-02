using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

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

                if (req.Content != null)
                {
                    log.Info("Data sent: ", req.Content.ToString());
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(@"{""logging"": ""success""}", Encoding.UTF8, "application/json")
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
