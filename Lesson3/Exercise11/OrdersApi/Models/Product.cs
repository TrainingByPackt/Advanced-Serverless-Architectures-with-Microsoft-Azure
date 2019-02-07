using Newtonsoft.Json;

namespace OrdersApi.Models {
    public class Product {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantityInStock")]
        public int QuantityInStock { get; set; }
    }
}