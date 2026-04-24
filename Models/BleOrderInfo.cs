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
        /// <summary>
        /// Customer's name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Customer's phone number.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Customer's email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Shipping address for the order.
        /// </summary>
        [JsonPropertyName("shipping_address")]
        public BleShippingAddress? ShippingAddress { get; set; }
    }
}