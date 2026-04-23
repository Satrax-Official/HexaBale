using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a shipping query for payment processing.
/// </summary>
/// <remarks>
/// Sent when a user confirms a payment and shipping address is required.
/// </remarks>
public class BleShippingQuery
{
    /// <summary>
    /// Unique identifier for this query.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// User who sent the query.
    /// </summary>
    [JsonPropertyName("from")]
    public BleUser? From { get; set; }

    /// <summary>
    /// Invoice payload specified by the bot.
    /// </summary>
    [JsonPropertyName("invoice_payload")]
    public string? InvoicePayload { get; set; }

    /// <summary>
    /// Shipping address provided by the user.
    /// </summary>
    [JsonPropertyName("shipping_address")]
    public BleShippingAddress? ShippingAddress { get; set; }
}

