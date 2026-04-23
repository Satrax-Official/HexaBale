using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents information about the bot's webhook configuration.
/// </summary>
/// <remarks>
/// Contains details about the currently set webhook URL and its status.
/// </remarks>
public class BleWebhookInfo
{
    /// <summary>
    /// Webhook URL (empty if webhook is not set).
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// True, if a custom certificate was provided for the webhook.
    /// </summary>
    [JsonPropertyName("has_custom_certificate")]
    public bool HasCustomCertificate { get; set; }

    /// <summary>
    /// Number of updates awaiting delivery.
    /// </summary>
    [JsonPropertyName("pending_update_count")]
    public int PendingUpdateCount { get; set; }

    /// <summary>
    /// Unix time of the most recent error (if any).
    /// </summary>
    [JsonPropertyName("last_error_date")]
    public int? LastErrorDate { get; set; }

    /// <summary>
    /// Error message from the most recent error.
    /// </summary>
    [JsonPropertyName("last_error_message")]
    public string? LastErrorMessage { get; set; }

    /// <summary>
    /// Unix time of the last successful connection.
    /// </summary>
    [JsonPropertyName("last_synchronization_error_date")]
    public int? LastSynchronizationErrorDate { get; set; }

    /// <summary>
    /// Maximum number of allowed simultaneous connections.
    /// </summary>
    [JsonPropertyName("max_connections")]
    public int? MaxConnections { get; set; }

    /// <summary>
    /// List of update types the webhook is subscribed to.
    /// </summary>
    [JsonPropertyName("allowed_updates")]
    public List<string>? AllowedUpdates { get; set; }
}