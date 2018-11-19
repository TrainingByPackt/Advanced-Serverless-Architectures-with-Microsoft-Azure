using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MicroBlogPostFunctions.Models;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Serialization;

namespace MicroBlogPostFunctions
{
    public static class GetMicroBlogPosts
    {
        [FunctionName("GetMicroBlogPosts")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "MicroBlogSite",
                collectionName: "MicroBlogPosts",
                ConnectionStringSetting = "CosmosDBConnection", 
                SqlQuery = "SELECT * FROM MicroBlogPosts")] MicroBlogPost[] microBlogPosts,
            ILogger log)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Headers.Add("cache-control","public");
            responseMessage.Content = new StringContent
            (
                JsonConvert.SerializeObject
                (
                    microBlogPosts,
                    new JsonSerializerSettings 
                    { 
                        ContractResolver = new CamelCasePropertyNamesContractResolver() 
                    }
                )
            );
            responseMessage.Content.Headers.Expires = DateTime.Now.AddMinutes(1);            
            return responseMessage;
        }
    }
}
