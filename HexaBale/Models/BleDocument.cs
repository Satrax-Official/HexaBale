using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a general file (document) in a message.
/// </summary>
/// <remarks>
/// Documents can be PDFs, ZIP files, text files, or any other file type.
/// </remarks>
public class BleDocument
{
    /// <summary>
    /// Identifier for this file (can be used to download or resend).
    /// </summary>
    [JsonPropertyName("file_id")]
    public string? FileId { get; set; }

    /// <summary>
    /// Unique identifier for this file (same for all bots, useful for caching).
    /// </summary>
    [JsonPropertyName("file_unique_id")]
    public string? FileUniqueId { get; set; }

    /// <summary>
    /// Document thumbnail (optional).
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public BlePhotoSize? Thumbnail { get; set; }

    /// <summary>
    /// Original filename (optional).
    /// </summary>
    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }

    /// <summary>
    /// MIME type of the file (optional).
    /// </summary>
    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }

    /// <summary>
    /// File size in bytes (optional).
    /// </summary>
    [JsonPropertyName("file_size")]
    public long FileSize { get; set; }

    /// <summary>
    /// Returns the file size in human-readable format (KB, MB, etc.).
    /// </summary>
    public string FileSizeFormatted => FormatFileSize(FileSize);

    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}