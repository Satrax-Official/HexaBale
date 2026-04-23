using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents an option in a poll.
/// </summary>
/// <remarks>
/// Each poll option has text and a vote count.
/// </remarks>
public class BlePollOption
{
    /// <summary>
    /// Option text (1-100 characters).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Number of users that voted for this option.
    /// </summary>
    [JsonPropertyName("voter_count")]
    public int VoterCount { get; set; }
}