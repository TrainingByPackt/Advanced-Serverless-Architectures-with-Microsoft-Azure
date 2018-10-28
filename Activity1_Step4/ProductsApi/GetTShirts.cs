
namespace ProductsApi
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.WebJobs.Host;
    using Microsoft.Extensions.Logging;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using System.Collections.Generic;
    using ProductsApi.Models;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    public static class GetTShirts
    {
        private static DocumentClient client;
        private static Uri tShirtCollectionUri;
        // Sets the max number of items in the response to infinity. Don't do this in production, 
        // it's advisable to page requests.
        private static readonly FeedOptions TShirtQueryOptions = new FeedOptions { MaxItemCount = -1 };


        [FunctionName("GetTShirts")]
        public static async Task<List<TShirt>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req, 
            ILogger log, 
            ExecutionContext context)
        {
            // Only create the client if its the first execution of the instance
            // This prevents port exhaustion and generally improves performance as the instantiation is expensive
            // if(client == null && tShirtCollectionUri == null){
            //         var config = new ConfigurationBuilder()
            //         .SetBasePath(context.FunctionAppDirectory)
            //         .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            //         .AddEnvironmentVariables()
            //         .Build();

            //     client = new DocumentClient(
            //         new Uri(config["CosmosEndpointUri"]),
            //         config["CosmosKey"]);

            //     tShirtCollectionUri = UriFactory.CreateDocumentCollectionUri(
            //         config["DatabaseName"], 
            //         config["TShirtCollectionName"]);
            // }
            // return client.CreateDocumentQuery<TShirt>(tShirtCollectionUri, TShirtQueryOptions).ToList();
            return new List<TShirt>() { new TShirt {
                Name = "Metallica",
                Colour = "Black",
                Size = "XL",
                Id = "1"
            }};
        }
    }
}
