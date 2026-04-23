using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a user's answer to a poll.
/// </summary>
/// <remarks>
/// Sent when a user changes their answer in a non-anonymous poll.
/// </remarks>
public class BlePollAnswer
{
    /// <summary>
    /// Unique poll identifier.
    /// </summary>
    [JsonPropertyName("poll_id")]
    public string? PollId { get; set; }

    /// <summary>
    /// The user who changed their answer.
    /// </summary>
    [JsonPropertyName("user")]
    public BleUser? User { get; set; }

    /// <summary>
    /// 0-based identifiers of the selected options.
    /// </summary>
    [JsonPropertyName("option_ids")]
    public int[]? OptionIds { get; set; }
}