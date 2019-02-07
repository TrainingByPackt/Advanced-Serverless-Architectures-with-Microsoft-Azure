using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using OrderDurableFunctions.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OrderDurableFunctions.Activities {
    public static class UpdateStockLevel {
        private const string _databaseName = "serverless";
        private const string _collectionName = "products";

        private static Uri _productsCollectionUri = UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName);

        [FunctionName("UpdateStockLevel")]
        public static async Task<bool> Run(
            [ActivityTrigger] Order order,
                [CosmosDB(
                databaseName: _databaseName,
                collectionName: _collectionName,
                ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
                ILogger log
        ){
            var product = client.CreateDocumentQuery<Product>(_productsCollectionUri)
                .Where(x=> x.Id == order.ProductId).ToArray()[0];
            product.QuantityInStock -= order.Quantity;
            if(product.QuantityInStock < 0){
                throw new Exception("Not enough stock");
            }
            var resultOfUpdate = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseName,_collectionName,product.Id),product);
            if((int)resultOfUpdate.StatusCode >= 300){
                throw new Exception($"Failed to update the product with id: ${product.Id}");
            }
            return true;
        }
    }
}