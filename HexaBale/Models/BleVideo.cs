using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a video file in a message.
/// </summary>
/// <remarks>
/// Contains video metadata including dimensions, duration, and thumbnail.
/// </remarks>
public class BleVideo
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
    /// Video width in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Video height in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// Video duration in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    /// <summary>
    /// Video thumbnail (optional).
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public BlePhotoSize? Thumbnail { get; set; }

    /// <summary>
    /// MIME type of the video (optional).
    /// </summary>
    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }

    /// <summary>
    /// File size in bytes (optional).
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }

    /// <summary>
    /// Returns the duration in human-readable format (MM:SS).
    /// </summary>
    public string DurationFormatted => $"{Duration / 60}:{Duration % 60:D2}";
}