using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a pre-checkout query for payment processing.
/// </summary>
/// <remarks>
/// Sent before a payment is processed to allow the bot to validate the order.
/// </remarks>
public class BlePreCheckoutQuery
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
    /// Three-letter ISO 4217 currency code.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Total price in the smallest units of the currency.
    /// </summary>
    [JsonPropertyName("total_amount")]
    public int TotalAmount { get; set; }

    /// <summary>
    /// Invoice payload specified by the bot.
    /// </summary>
    [JsonPropertyName("invoice_payload")]
    public string? InvoicePayload { get; set; }

    /// <summary>
    /// Identifier of the shipping option chosen by the user.
    /// </summary>
    [JsonPropertyName("shipping_option_id")]
    public string? ShippingOptionId { get; set; }

    /// <summary>
    /// Order information provided by the user (optional).
    /// </summary>
    [JsonPropertyName("order_info")]
    public BleOrderInfo? OrderInfo { get; set; }
}

