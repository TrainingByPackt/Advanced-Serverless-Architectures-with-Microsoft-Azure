using System;
using MicroBlogPostFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MicroBlogPostFunctions
{
    public static class DequeueMicroBlogPost
    {
        [FunctionName("DequeueMicroBlogPost")]
        public static void Run(
            [QueueTrigger("micro-blog-posts", Connection = "AzureWebJobsStorage")]MicroBlogPost microBlogPost, 
            [CosmosDB(
                databaseName: "MicroBlogSite",
                collectionName: "MicroBlogPosts",
                ConnectionStringSetting = "CosmosDBConnection")]out MicroBlogPost document,
            ILogger log)
        {
            document = microBlogPost;
        }
    }
}
