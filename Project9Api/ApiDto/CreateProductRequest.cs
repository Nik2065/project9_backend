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
        public decimal Cost { get; set; }

        //[JsonProperty("cpu_id")]
        public int CpuId { get; set; }


        //[JsonProperty("gpu_id")]
        public int? GpuId { get; set; }
    }
}
