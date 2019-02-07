
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
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Serialization;

namespace ProductsApi
{
    public static class GetProducts
    {
        // private static DocumentClient client = new DocumentClient(new Uri(""),"");
        // private static Uri productCollectionUri = UriFactory.CreateDocumentCollectionUri("serverless","products");

        // private static readonly FeedOptions productQueryOptions = new FeedOptions { MaxItemCount = -1 };

        [FunctionName("GetProducts")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req, ILogger log)
        {
            // var results = client.CreateDocumentQuery<Product>(productCollectionUri, productQueryOptions);
            var results = new List<Product>()
            {
                new Product
                {
                    Id = "",
                    Name = "Metallica",
                    Size = "XL",
                    Colour = "Black",
                    TypeId = "tshirt",
                    QuantityInStock = 10
                }
            };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Headers.Add("cache-control","public");
            responseMessage.Content = new StringContent
            (
                JsonConvert.SerializeObject
                (
                    results,
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
