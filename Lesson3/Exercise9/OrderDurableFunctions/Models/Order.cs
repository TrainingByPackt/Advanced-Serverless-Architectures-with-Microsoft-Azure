using Newtonsoft.Json;

namespace OrderDurableFunctions.Models {
    public class Order {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}