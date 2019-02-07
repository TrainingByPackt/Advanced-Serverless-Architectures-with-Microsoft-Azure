using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SendGrid;
using OrderDurableFunctions.Models;
using SendGrid.Helpers.Mail;

namespace OrderDurableFunctions.Activities {
    public class PackAndShipOrder {
        [FunctionName("PackAndShipOrder")]
        public static void Run(
            [ActivityTrigger] (Order,string) orderTuple,
            [SendGrid(ApiKey = "SendGridApiKey2")] out SendGridMessage message
            )
            {
                var order = orderTuple.Item1;
                var instanceId = orderTuple.Item2;
                message = new SendGridMessage();
                message.AddTo("danbass8@googlemail.com");
                message.SetFrom(new EmailAddress("random@email.com"));
                message.AddContent("text/html",
                $@"<h1>We've got one!</h1>
                <p>Order of {order.Quantity} items of {order.ProductId} to {order.DeliveryAddress}. 
                <a href='http://localhost:7071/api/CompletePackingAndShipping/{instanceId}'>Click here when order complete</a><br>
                <a href='http://localhost:7071/api/FailedOrderProcessing/{instanceId}'>Click here if order failed</a>");
                message.SetSubject("Order");
        }
    }
}