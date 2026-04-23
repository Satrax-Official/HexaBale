using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents an animation (GIF) in a message.
/// </summary>
/// <remarks>
/// Animations are looping videos without sound, typically GIF format.
/// </remarks>
public class BleAnimation
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
    /// Animation width in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Animation height in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// Animation duration in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    /// <summary>
    /// Animation thumbnail.
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public BlePhotoSize? Thumbnail { get; set; }

    /// <summary>
    /// Original filename (optional).
    /// </summary>
    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }

    /// <summary>
    /// MIME type of the animation (optional).
    /// </summary>
    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }

    /// <summary>
    /// File size in bytes (optional).
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }
}