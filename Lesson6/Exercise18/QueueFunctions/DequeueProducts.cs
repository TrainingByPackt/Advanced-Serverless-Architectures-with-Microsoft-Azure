using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using QueueFunctions.Models;

namespace QueueFunctions.Products
{
    public static class DequeueProducts
    {
        [FunctionName("DequeueProducts")]
        public static void Run(
            [QueueTrigger("product-queue", Connection = "AzureQueueStorageAccount")]Product product,
            [CosmosDB(
                databaseName: "serverless",
                collectionName: "products",
                ConnectionStringSetting = "CosmosDBConnectionString"
            )] out Product outProduct,
             ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {product}");
            outProduct = product;
        }
    }
}
