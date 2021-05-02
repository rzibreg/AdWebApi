using System.Text.Json.Serialization;

namespace AdWebApi.Entities
{
    public class ProductDefinition
    {
        [JsonPropertyName("company_prefix")]
        public long CompanyPrefix { get; set; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }

        [JsonPropertyName("item_reference")]
        public int ItemReference { get; set; }

        [JsonPropertyName("item_name")]
        public string ItemName { get; set; }
    }
}
