using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrdersApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrdersApi
{
    [StorageAccount("OrderQueueStorageAccount")]
    public static class SubmitOrder
    {
        [FunctionName("SubmitOrder")]
        [return: Queue("orders")]
        public static Order Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] Order order,
            [CosmosDB(
                databaseName: "serverless",
                collectionName: "products",
                ConnectionStringSetting = "CosmosDBConnection", 
                Id = "{ProductId}",
                PartitionKey = "black")]Product product,
            ILogger log)
        {
            log.LogInformation("bob");
            if(product != null && product.QuantityInStock >= order.Quantity){
                order.Id = $"{order.ProductId}_{order.Quantity}_{order.DeliveryAddress}_{DateTime.UtcNow}";
                return order;
            } else {
                throw new Exception("Invalid Order");
            }
        }
    }
}
