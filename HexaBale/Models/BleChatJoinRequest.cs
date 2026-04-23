using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a request to join a chat.
/// </summary>
/// <remarks>
/// Sent when a user requests to join a chat where the bot is an administrator.
/// </remarks>
public class BleChatJoinRequest
{
    /// <summary>
    /// Chat where the join request was made.
    /// </summary>
    [JsonPropertyName("chat")]
    public BleChat? Chat { get; set; }

    /// <summary>
    /// User who sent the join request.
    /// </summary>
    [JsonPropertyName("from")]
    public BleUser? From { get; set; }

    /// <summary>
    /// Date when the request was sent.
    /// </summary>
    [JsonPropertyName("date")]
    public int Date { get; set; }

    /// <summary>
    /// Bio of the user (optional).
    /// </summary>
    [JsonPropertyName("bio")]
    public string? Bio { get; set; }

    /// <summary>
    /// Chat invite link that was used to send the request (optional).
    /// </summary>
    [JsonPropertyName("invite_link")]
    public BleChatInviteLink? InviteLink { get; set; }
}

