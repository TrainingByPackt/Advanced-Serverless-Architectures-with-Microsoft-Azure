using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueFunctions.Models;

namespace QueueFunctions.Products
{
    [StorageAccount("AzureQueueStorageAccount")]
    public static class AddProducts
    {
        [FunctionName("AddProducts")]
        [return: Queue("product-queue")]
        public static async Task<Product> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
           
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product product = JsonConvert.DeserializeObject<Product>(requestBody);

            return product;
        }
    }
}
