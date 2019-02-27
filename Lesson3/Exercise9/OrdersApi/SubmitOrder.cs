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
            if(product != null && product.QuantityInStock >= order.Quantity){
                return order;
            } else {
                return null;
            }
        }
    }
}
