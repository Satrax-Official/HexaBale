using System.Text.Json.Serialization;
using HexaBale.Enums;

namespace HexaBale.Models;

/// <summary>
/// Represents a chat (private, group, supergroup, or channel) on Bale Messenger.
/// </summary>
/// <remarks>
/// Contains information about a chat including its type, title, and identifiers.
/// </remarks>
public class BleChat
{
    /// <summary>
    /// Unique identifier for this chat.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Type of the chat (private, group, supergroup, or channel).
    /// </summary>
    [JsonPropertyName("type")]
    public BleChatType Type { get; set; }

    /// <summary>
    /// Title of the chat (for groups, supergroups, and channels).
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Username of the chat (for private chats and channels).
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// First name of the other party in a private chat.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the other party in a private chat.
    /// </summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Returns the display name of the chat for UI purposes.
    /// </summary>
    public string DisplayName => Title ?? Username ?? FirstName ?? Id.ToString();
}