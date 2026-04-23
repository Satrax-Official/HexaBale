using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a button that copies text to the user's clipboard.
/// </summary>
/// <remarks>
/// Useful for sharing codes, links, or any text that users might want to copy.
/// </remarks>
public class BleCopyTextButton
{
    /// <summary>
    /// The text to be copied to the clipboard.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}