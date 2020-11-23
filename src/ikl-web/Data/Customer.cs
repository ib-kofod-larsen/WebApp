using System.Text.Json.Serialization;

namespace ikl_web.Data
{
    public class Customer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("names")]
        public string[] Names { get; set; }
        
    }
}