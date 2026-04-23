using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a dice message (random value game).
/// </summary>
/// <remarks>
/// Dice messages have an emoji and a random value. Supported emojis: 🎲, 🎯, 🏀, ⚽, 🎰.
/// </remarks>
public class BleDice
{
    /// <summary>
    /// Emoji representing the dice type.
    /// </summary>
    [JsonPropertyName("emoji")]
    public string? Emoji { get; set; }

    /// <summary>
    /// Random value (1-6 for 🎲, 1-6 for 🎯, 1-5 for 🏀, 1-5 for ⚽, 1-64 for 🎰).
    /// </summary>
    [JsonPropertyName("value")]
    public int Value { get; set; }
}