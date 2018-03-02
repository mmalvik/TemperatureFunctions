using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace TemperatureFunctions
{
    public static class GetHealth
    {
        [FunctionName("GetHealth")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetTemperatures")]HttpRequestMessage req, TraceWriter log)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
