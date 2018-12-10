using Newtonsoft.Json;

namespace VerifyUserEmail {
    public class User {
        [JsonProperty("name")]
        public string Name {get; set;}

        [JsonProperty("emailAddress")]
        public string EmailAddress {get; set;}
    }
}