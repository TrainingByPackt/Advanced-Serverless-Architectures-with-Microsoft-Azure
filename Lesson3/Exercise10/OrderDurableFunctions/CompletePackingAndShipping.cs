using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace OrderDurableFunctions {
    public class ReportOrder {
        [FunctionName("CompletePackingAndShipping")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                "get",
                Route = "CompletePackingAndShipping/{instanceId}")] HttpRequest request,
            string instanceId,
            [OrchestrationClient] DurableOrchestrationClientBase client)
        {
            Console.WriteLine(client.GetStatusAsync(instanceId).Status.ToString());
            await client.RaiseEventAsync(instanceId, "OrderCompleted",true);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}