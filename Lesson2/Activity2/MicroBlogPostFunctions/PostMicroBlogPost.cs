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

namespace MicroBlogPostFunctions
{
    [StorageAccount("AzureQueueStorageAccount")]
    public static class PostMicroBlogPost
    {
        [return: Queue("MicroBlogPosts")]
        [FunctionName("PostMicroBlogPost")]
        public static MicroBlogPost Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] MicroBlogPost microBlogPost,
            ILogger log)
        {
            return microBlogPost;
        }
    }
}
