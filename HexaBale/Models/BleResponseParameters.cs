using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Additional parameters returned in error responses from Bale API.
/// </summary>
/// <remarks>
/// These parameters provide extra information about the error, such as retry duration for rate limits.
/// </remarks>
public class BleResponseParameters
{
    /// <summary>
    /// Number of seconds to wait before retrying the request (for rate limit errors).
    /// </summary>
    [JsonPropertyName("retry_after")]
    public int? RetryAfter { get; set; }

    /// <summary>
    /// The chat ID to migrate to (when a group is upgraded to a supergroup).
    /// </summary>
    [JsonPropertyName("migrate_to_chat_id")]
    public long? MigrateToChatId { get; set; }
}