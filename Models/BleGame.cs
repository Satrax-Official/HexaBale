using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a game message.
/// </summary>
/// <remarks>
/// Games are HTML5 games that can be played within Bale Messenger.
/// </remarks>
public class BleGame
{
    /// <summary>
    /// Game title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Game description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Game photo (thumbnail).
    /// </summary>
    [JsonPropertyName("photo")]
    public BlePhotoSize[]? Photo { get; set; }

    /// <summary>
    /// Game text (optional).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Special entities for the game text.
    /// </summary>
    [JsonPropertyName("text_entities")]
    public object[]? TextEntities { get; set; }

    /// <summary>
    /// Game animation (optional).
    /// </summary>
    [JsonPropertyName("animation")]
    public BleAnimation? Animation { get; set; }
}