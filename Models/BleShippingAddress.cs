using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HexaBale.Models
{
    /// <summary>
    /// Represents a shipping address.
    /// </summary>
    public class BleShippingAddress
    {
        /// <summary>
        /// Two-letter ISO 3166-1 alpha-2 country code.
        /// </summary>
        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        /// <summary>
        /// State or province name.
        /// </summary>
        [JsonPropertyName("state")]
        public string? State { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <summary>
        /// First line of the street address.
        /// </summary>
        [JsonPropertyName("street_line1")]
        public string? StreetLine1 { get; set; }

        /// <summary>
        /// Second line of the street address (optional).
        /// </summary>
        [JsonPropertyName("street_line2")]
        public string? StreetLine2 { get; set; }

        /// <summary>
        /// Postal code or ZIP code.
        /// </summary>
        [JsonPropertyName("post_code")]
        public string? PostCode { get; set; }
    }
}