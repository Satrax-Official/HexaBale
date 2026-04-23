using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a poll in a message.
/// </summary>
/// <remarks>
/// Polls allow users to vote on multiple choice questions.
/// </remarks>
public class BlePoll
{
    /// <summary>
    /// Unique poll identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Poll question (1-300 characters).
    /// </summary>
    [JsonPropertyName("question")]
    public string? Question { get; set; }

    /// <summary>
    /// List of poll options.
    /// </summary>
    [JsonPropertyName("options")]
    public BlePollOption[]? Options { get; set; }

    /// <summary>
    /// Total number of users that voted in the poll.
    /// </summary>
    [JsonPropertyName("total_voter_count")]
    public int TotalVoterCount { get; set; }

    /// <summary>
    /// True, if the poll is closed.
    /// </summary>
    [JsonPropertyName("is_closed")]
    public bool IsClosed { get; set; }

    /// <summary>
    /// True, if the poll is anonymous.
    /// </summary>
    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; set; }

    /// <summary>
    /// Poll type ('regular' or 'quiz').
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// True, if multiple answers can be selected.
    /// </summary>
    [JsonPropertyName("allows_multiple_answers")]
    public bool AllowsMultipleAnswers { get; set; }

    /// <summary>
    /// 0-based identifier of the correct answer (for quiz polls).
    /// </summary>
    [JsonPropertyName("correct_option_id")]
    public int? CorrectOptionId { get; set; }

    /// <summary>
    /// Text shown when a user selects an incorrect answer (for quiz polls).
    /// </summary>
    [JsonPropertyName("explanation")]
    public string? Explanation { get; set; }

    /// <summary>
    /// Special entities for the explanation text.
    /// </summary>
    [JsonPropertyName("explanation_entities")]
    public object[]? ExplanationEntities { get; set; }

    /// <summary>
    /// Amount of time in seconds the poll will be active.
    /// </summary>
    [JsonPropertyName("open_period")]
    public int? OpenPeriod { get; set; }

    /// <summary>
    /// Point in time (Unix timestamp) when the poll will be automatically closed.
    /// </summary>
    [JsonPropertyName("close_date")]
    public int? CloseDate { get; set; }
}