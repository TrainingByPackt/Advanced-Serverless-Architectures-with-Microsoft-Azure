using System;
using Microsoft.Azure.WebJobs;
using OrderDurableFunctions.Models;

namespace OrderDurableFunctions {
    public class OrchestrateOrderProcessing {
        [FunctionName("OrchestrateOrderProcessing")]
        public static async void Run([OrchestrationTrigger] DurableOrchestrationContext context){
            var input = context.GetInput<Order>();
            Console.WriteLine(input.ProductId);
            var resultOfUpdateStockLevel = await context.CallActivityAsync<bool>("UpdateStockLevel",input);
            if(resultOfUpdateStockLevel){
                await context.CallActivityAsync("SaveOrderToStorageTable",input);
            }
        }
    }
}