using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using OrderDurableFunctions.Models;

namespace OrderDurableFunctions {
    public class OrchestrateOrderProcessing {
        [FunctionName("OrchestrateOrderProcessing")]
        public static async void Run([OrchestrationTrigger] DurableOrchestrationContext context){
            var input = context.GetInput<Order>();
            Console.WriteLine(input.ProductId);
            Console.WriteLine(context.InstanceId);
            // var resultOfUpdateStockLevel = await context.CallActivityAsync<bool>("UpdateStockLevel",input);
            if(true){
                await context.CallActivityAsync("PackAndShipOrder",(input,context.InstanceId));
                bool orderSuccessful = await context.WaitForExternalEvent<bool>("OrderCompleted",TimeSpan.FromSeconds(30),false);
                if(orderSuccessful){
                    Console.WriteLine("Order was successfully shipped!");
                } else {
                    Console.WriteLine("Fail");
                }
            }
        }
    }
}