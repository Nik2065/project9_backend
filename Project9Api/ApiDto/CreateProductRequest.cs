using Newtonsoft.Json;

namespace Project9Api.ApiDto
{
    public class CreateProductRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }
    }
}
