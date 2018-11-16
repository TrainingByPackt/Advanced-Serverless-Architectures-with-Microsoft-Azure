using System;
using Microsoft.Azure.WebJobs;
using OrderDurableFunctions.Models;

namespace OrderDurableFunctions {
    public class OrchestrateOrderProcessing {
        [FunctionName("OrchestrateOrderProcessing")]
        public static void Run([OrchestrationTrigger] DurableOrchestrationContext context){
            var input = context.GetInput<Order>();
            Console.WriteLine(input);
        }
    }
}