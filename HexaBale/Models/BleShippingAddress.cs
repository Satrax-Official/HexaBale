using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HexaBale.Models
{/// <summary>
 /// Represents a shipping address.
 /// </summary>
    public class BleShippingAddress
    {
        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("street_line1")]
        public string? StreetLine1 { get; set; }

        [JsonPropertyName("street_line2")]
        public string? StreetLine2 { get; set; }

        [JsonPropertyName("post_code")]
        public string? PostCode { get; set; }
    }
}
