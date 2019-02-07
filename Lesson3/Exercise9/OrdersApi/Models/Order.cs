using Newtonsoft.Json;

namespace OrdersApi.Models {
    public class Order {
        
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}