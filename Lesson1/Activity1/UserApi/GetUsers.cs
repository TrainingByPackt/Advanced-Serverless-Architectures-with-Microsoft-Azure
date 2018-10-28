
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
using UserApi.Models;

namespace UserApi
{
    public static class GetUsers
    {
        private static DocumentClient client = new DocumentClient(new Uri(""),"");
        private static Uri userCollectionUri = UriFactory.CreateDocumentCollectionUri("serverless","users");

        private static readonly FeedOptions userQueryOptions = new FeedOptions { MaxItemCount = -1 };

        [FunctionName("GetUsers")]
        public static async Task<List<User>> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req, ILogger log)
        {
            return client.CreateDocumentQuery<User>(userCollectionUri, userQueryOptions).ToList();
        }
    }
}