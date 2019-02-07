using Newtonsoft.Json;

namespace QueueFunctions.Models {
    public class Product {
        [JsonProperty(PropertyName = "typeId")]
        public string TypeId { get; set; }
        [JsonProperty(PropertyName = "name")]

        public string Name { get; set; }
        [JsonProperty(PropertyName = "size")]

        public string Size { get; set; }
        [JsonProperty(PropertyName = "colour")]

        public string Colour { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}