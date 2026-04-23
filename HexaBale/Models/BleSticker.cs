using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a sticker in a message.
/// </summary>
/// <remarks>
/// Stickers are custom images or animations used for expression.
/// </remarks>
public class BleSticker
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
    /// Sticker width in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Sticker height in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// True, if the sticker is animated.
    /// </summary>
    [JsonPropertyName("is_animated")]
    public bool IsAnimated { get; set; }

    /// <summary>
    /// True, if the sticker is a video sticker.
    /// </summary>
    [JsonPropertyName("is_video")]
    public bool IsVideo { get; set; }

    /// <summary>
    /// Emoji associated with the sticker.
    /// </summary>
    [JsonPropertyName("emoji")]
    public string? Emoji { get; set; }

    /// <summary>
    /// File size in bytes (optional).
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }
}