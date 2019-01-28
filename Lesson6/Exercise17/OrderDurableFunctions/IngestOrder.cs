using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using OrderDurableFunctions.Models;

namespace OrderDurableFunctions
{
    public static class IngestOrder
    {
        [FunctionName("IngestOrder")]
        public static Task Run(
            [QueueTrigger("orders", Connection = "OrderStorage")]Order order,
            [OrchestrationClient] DurableOrchestrationClientBase orchestrationClientBase, 
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {order}");
            return orchestrationClientBase.StartNewAsync("OrchestrateOrderProcessing",order);
        }
    }
}
