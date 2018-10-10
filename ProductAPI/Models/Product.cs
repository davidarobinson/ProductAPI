using Newtonsoft.Json;

namespace ProductAPI.Models
{
    public partial class Product
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string ProductGroup { get; set; }
    }
}
