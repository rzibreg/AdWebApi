using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdWebApi.Entities
{
    public class InventoryData
    {
        [JsonPropertyName("id")]
        [MaxLength(32)]
        public string Id { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("date_of_inventory")]
        public DateTime DateOfInventory { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        public List<Sgtin96Tag> DecodedTags { get; set; }
    }
}
