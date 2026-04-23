using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents an audio file (music) in a message.
/// </summary>
/// <remarks>
/// Audio files are typically music tracks with performer and title information.
/// </remarks>
public class BleAudio
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
    /// Duration of the audio in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    /// <summary>
    /// Performer of the audio (artist name).
    /// </summary>
    [JsonPropertyName("performer")]
    public string? Performer { get; set; }

    /// <summary>
    /// Title of the audio track.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// File size in bytes (optional).
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }
}