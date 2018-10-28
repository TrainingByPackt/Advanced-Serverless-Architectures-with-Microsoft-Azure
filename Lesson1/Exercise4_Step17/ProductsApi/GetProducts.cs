
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using System.Linq;
using System.Collections.Generic;
using ProductsApi.Models;

namespace ProductsApi
{
    public static class GetProducts
    {
        private static DocumentClient client = new DocumentClient(new Uri(""),"");
        private static Uri productCollectionUri = UriFactory.CreateDocumentCollectionUri("serverless","products");

        private static readonly FeedOptions productQueryOptions = new FeedOptions { MaxItemCount = -1 };

        [FunctionName("GetProducts")]
        public static async Task<List<Product>> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req, ILogger log)
        {
            return client.CreateDocumentQuery<Product>(productCollectionUri, productQueryOptions).ToList();
        }
    }
}
