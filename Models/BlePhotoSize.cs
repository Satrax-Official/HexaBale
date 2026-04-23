using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a photo or thumbnail in a message.
/// </summary>
/// <remarks>
/// Photos can have multiple sizes; the last element in the array is the largest version.
/// </remarks>
public class BlePhotoSize
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
    /// Photo width in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Photo height in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// File size in bytes (optional).
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }

    /// <summary>
    /// Returns the dimensions as a string (e.g., "1920x1080").
    /// </summary>
    public string Dimensions => $"{Width}x{Height}";
}