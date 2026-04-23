using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a user or bot on Bale Messenger.
/// </summary>
/// <remarks>
/// Contains basic information about a Bale user including identifiers and display names.
/// </remarks>
public class BleUser
{
    /// <summary>
    /// Unique identifier for this user or bot.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// True, if this user is a bot.
    /// </summary>
    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }

    /// <summary>
    /// User's first name.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// User's last name (optional).
    /// </summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// User's username (optional).
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// IETF language tag of the user's language.
    /// </summary>
    [JsonPropertyName("language_code")]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Returns the full name or username of the user for display purposes.
    /// </summary>
    public string DisplayName => !string.IsNullOrEmpty(FirstName)
        ? $"{FirstName} {LastName}".Trim()
        : Username ?? Id.ToString();
}