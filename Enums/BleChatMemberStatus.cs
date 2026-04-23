using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HexaBale.Enums;

/// <summary>
/// Represents the status of a user in a chat or channel.
/// </summary>
/// <remarks>
/// Status determines what actions a user can perform in the chat.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BleChatMemberStatus
{
    /// <summary>
    /// User is the creator/owner of the chat or channel.
    /// </summary>
    /// <remarks>
    /// Creator has full control and cannot be removed by other admins.
    /// </remarks>
    [EnumMember(Value = "creator")]
    [JsonPropertyName("creator")]
    Creator,

    /// <summary>
    /// User is an administrator with special privileges.
    /// </summary>
    /// <remarks>
    /// Admins can perform actions like deleting messages, banning users, and changing chat settings.
    /// </remarks>
    [EnumMember(Value = "administrator")]
    [JsonPropertyName("administrator")]
    Administrator,

    /// <summary>
    /// User is a regular member of the chat or channel.
    /// </summary>
    /// <remarks>
    /// Members can read and send messages (unless restricted by admins).
    /// </remarks>
    [EnumMember(Value = "member")]
    [JsonPropertyName("member")]
    Member,

    /// <summary>
    /// User has restricted permissions in the chat.
    /// </summary>
    /// <remarks>
    /// Restricted users may have limitations on sending messages or media.
    /// </remarks>
    [EnumMember(Value = "restricted")]
    [JsonPropertyName("restricted")]
    Restricted,

    /// <summary>
    /// User has left the chat or channel voluntarily.
    /// </summary>
    /// <remarks>
    /// Left users can rejoin if the chat allows it.
    /// </remarks>
    [EnumMember(Value = "left")]
    [JsonPropertyName("left")]
    Left,

    /// <summary>
    /// User was kicked or banned from the chat or channel.
    /// </summary>
    /// <remarks>
    /// Kicked users cannot rejoin unless unbanned by an admin.
    /// </remarks>
    [EnumMember(Value = "kicked")]
    [JsonPropertyName("kicked")]
    Kicked
}