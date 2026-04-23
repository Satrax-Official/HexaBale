using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HexaBale.Models
{
    /// <summary>
    /// Represents order information for payment.
    /// </summary>
    public class BleOrderInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("shipping_address")]
        public BleShippingAddress? ShippingAddress { get; set; }
    }
}
