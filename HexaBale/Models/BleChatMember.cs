using System.Text.Json.Serialization;
using HexaBale.Enums;

namespace HexaBale.Models;

/// <summary>
/// Represents a chat member's information.
/// </summary>
/// <remarks>
/// Contains user information and their status in the chat.
/// </remarks>
public class BleChatMember
{
    /// <summary>
    /// Information about the user.
    /// </summary>
    [JsonPropertyName("user")]
    public BleUser? User { get; set; }

    /// <summary>
    /// Member's status in the chat.
    /// </summary>
    [JsonPropertyName("status")]
    public BleChatMemberStatus Status { get; set; }

    /// <summary>
    /// Date when restrictions will be lifted (for restricted users).
    /// </summary>
    [JsonPropertyName("until_date")]
    public int? UntilDate { get; set; }

    /// <summary>
    /// Whether the administrator can be edited by the bot.
    /// </summary>
    [JsonPropertyName("can_be_edited")]
    public bool? CanBeEdited { get; set; }

    /// <summary>
    /// Whether the administrator's presence is hidden.
    /// </summary>
    [JsonPropertyName("is_anonymous")]
    public bool? IsAnonymous { get; set; }

    /// <summary>
    /// Whether the administrator can manage the chat.
    /// </summary>
    [JsonPropertyName("can_manage_chat")]
    public bool? CanManageChat { get; set; }

    /// <summary>
    /// Whether the administrator can delete messages.
    /// </summary>
    [JsonPropertyName("can_delete_messages")]
    public bool? CanDeleteMessages { get; set; }

    /// <summary>
    /// Whether the administrator can manage video chats.
    /// </summary>
    [JsonPropertyName("can_manage_video_chats")]
    public bool? CanManageVideoChats { get; set; }

    /// <summary>
    /// Whether the administrator can restrict members.
    /// </summary>
    [JsonPropertyName("can_restrict_members")]
    public bool? CanRestrictMembers { get; set; }

    /// <summary>
    /// Whether the administrator can promote members to administrators.
    /// </summary>
    [JsonPropertyName("can_promote_members")]
    public bool? CanPromoteMembers { get; set; }

    /// <summary>
    /// Whether the administrator can change chat information.
    /// </summary>
    [JsonPropertyName("can_change_info")]
    public bool? CanChangeInfo { get; set; }

    /// <summary>
    /// Whether the administrator can invite new users.
    /// </summary>
    [JsonPropertyName("can_invite_users")]
    public bool? CanInviteUsers { get; set; }

    /// <summary>
    /// Whether the administrator can pin messages.
    /// </summary>
    [JsonPropertyName("can_pin_messages")]
    public bool? CanPinMessages { get; set; }

    /// <summary>
    /// Whether the administrator can manage topics.
    /// </summary>
    [JsonPropertyName("can_manage_topics")]
    public bool? CanManageTopics { get; set; }
}