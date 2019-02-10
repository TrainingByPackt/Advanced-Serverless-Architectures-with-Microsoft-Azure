using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace OrderDurableFunctions {
    public class FailedOrderProcessing {
        [FunctionName("FailedOrderProcessing")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                "get",
                Route = "FailedOrderProcessing/{instanceId}")] HttpRequest request,
            string instanceId,
            [OrchestrationClient] DurableOrchestrationClientBase client)
        {
            await client.RaiseEventAsync(instanceId, "OrderFailed",false);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}