using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a chat member status update.
/// </summary>
/// <remarks>
/// Sent when a chat member's status changes (join, leave, promotion, etc.).
/// </remarks>
public class BleChatMemberUpdated
{
    /// <summary>
    /// Chat where the update occurred.
    /// </summary>
    [JsonPropertyName("chat")]
    public BleChat? Chat { get; set; }

    /// <summary>
    /// User whose status changed.
    /// </summary>
    [JsonPropertyName("from")]
    public BleUser? From { get; set; }

    /// <summary>
    /// Date when the update occurred.
    /// </summary>
    [JsonPropertyName("date")]
    public int Date { get; set; }

    /// <summary>
    /// Previous status of the user.
    /// </summary>
    [JsonPropertyName("old_chat_member")]
    public BleChatMember? OldChatMember { get; set; }

    /// <summary>
    /// New status of the user.
    /// </summary>
    [JsonPropertyName("new_chat_member")]
    public BleChatMember? NewChatMember { get; set; }
}