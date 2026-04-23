using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a voice message in a message.
/// </summary>
/// <remarks>
/// Voice messages are audio recordings sent by users, typically short and compressed.
/// </remarks>
public class BleVoice
{
    /// <summary>
    /// Identifier for this file.
    /// </summary>
    [JsonPropertyName("file_id")]
    public string? FileId { get; set; }

    /// <summary>
    /// Unique identifier for this file (same for all bots).
    /// </summary>
    [JsonPropertyName("file_unique_id")]
    public string? FileUniqueId { get; set; }

    /// <summary>
    /// Duration of the voice message in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    /// <summary>
    /// File size in bytes (optional).
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }
}