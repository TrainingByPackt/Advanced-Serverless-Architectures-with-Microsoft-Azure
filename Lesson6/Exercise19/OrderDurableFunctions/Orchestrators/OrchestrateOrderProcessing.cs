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
            var resultOfUpdateStockLevel = await context.CallActivityAsync<bool>("UpdateStockLevel",input);
            if(true){
                await context.CallActivityAsync("PackAndShipOrder",(input,context.InstanceId));
                Task<bool> orderSuccessful = context.WaitForExternalEvent<bool>("OrderCompleted");
                Task<bool> orderFailed = context.WaitForExternalEvent<bool>("OrderFailed");

                Task orderResult = await Task.WhenAny(orderSuccessful, orderFailed);
                if(orderResult == orderSuccessful){
                    Console.WriteLine("Order was successfully shipped!");
                } else {
                    await context.CallActivityAsync("SendUserApologyEmail",(input,context.InstanceId));
                    Console.WriteLine("Fail");
                }
            }
        }
    }
}