using Newtonsoft.Json;

namespace OrderDurableFunctions.Models {
    public class Order {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        
        [JsonProperty("deliveryAddress")]
        public string DeliveryAddress {get; set;}

        [JsonProperty("id")]
        public string Id {get; set;}    

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; } 
    }
}