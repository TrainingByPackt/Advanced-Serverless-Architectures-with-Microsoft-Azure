using Newtonsoft.Json;

namespace OrderDurableFunctions.Models {
    public class Product {
         public string TypeId {get; set;}
        public string Name { get; set; }

        public string Size { get; set; }

        public string Colour { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
         [JsonProperty("quantityInStock")]
        public int QuantityInStock { get; set; }
    }
}