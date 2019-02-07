using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SendGrid;
using OrderDurableFunctions.Models;
using SendGrid.Helpers.Mail;

namespace OrderDurableFunctions.Activities {
    public class SendUserApologyEmail {
        [FunctionName("SendUserApologyEmail")]
        public static void Run(
            [ActivityTrigger] (Order,string) orderTuple,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message
            )
            {
                var order = orderTuple.Item1;
                var instanceId = orderTuple.Item2;
                message = new SendGridMessage();
                message.AddTo(order.EmailAddress);
                message.SetFrom(new EmailAddress("random@email.com"));
                message.AddContent("text/html",
                $@"<h1>Sorry, your order was unsuccessful</h1>
                <p>Your order of {order.Quantity} items of {order.ProductId} to {order.DeliveryAddress} was unfortunately 
                unsuccessful. 
                Please call us on 07123456789 to see how we can help.</p>");
                message.SetSubject("Order unsuccessful");
        }
    }
}