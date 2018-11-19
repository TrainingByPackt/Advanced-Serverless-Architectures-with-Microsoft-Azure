using Newtonsoft.Json;

namespace ProductsApi.Models {
    public class Product {
        public string TypeId {get; set;}
        public string Name { get; set; }

        public string Size { get; set; }

        public string Colour { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }

        public int QuantityInStock { get; set; }
    }
}