using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace OrderDurableFunctions {
    public class ReportOrder {
        [FunctionName("CompletePackingAndShipping")]
        public static async Task Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                "get",
                Route = "/api/ReportPackingAndShippingStatus")] HttpRequest request,
            string instanceId,
            [OrchestrationClient] DurableOrchestrationClientBase client)
        {
            await client.RaiseEventAsync(instanceId, "OrderCompleted",true);
        }
    }
}