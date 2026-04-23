using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents an inline query from a user.
/// </summary>
/// <remarks>
/// Inline queries allow users to interact with bots directly from the message input field.
/// </remarks>
public class BleInlineQuery
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
    /// Query text (up to 256 characters).
    /// </summary>
    [JsonPropertyName("query")]
    public string? Query { get; set; }

    /// <summary>
    /// Offset for pagination.
    /// </summary>
    [JsonPropertyName("offset")]
    public string? Offset { get; set; }

    /// <summary>
    /// Type of the chat from which the inline query was sent.
    /// </summary>
    [JsonPropertyName("chat_type")]
    public string? ChatType { get; set; }

    /// <summary>
    /// Location of the user (optional).
    /// </summary>
    [JsonPropertyName("location")]
    public BleLocation? Location { get; set; }
}