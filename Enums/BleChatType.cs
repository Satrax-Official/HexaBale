using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HexaBale.Enums;

/// <summary>
/// Represents the type of a chat in Bale Messenger.
/// </summary>
/// <remarks>
/// Chat types determine how messages behave and what features are available.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BleChatType
{
    /// <summary>
    /// Private chat between a single user and the bot.
    /// </summary>
    /// <remarks>
    /// Only the bot and one user can see messages in this chat.
    /// </remarks>
    [EnumMember(Value = "private")]
    [JsonPropertyName("private")]
    Private,

    /// <summary>
    /// Basic group chat with multiple participants.
    /// </summary>
    /// <remarks>
    /// Groups have limited features compared to supergroups.
    /// </remarks>
    [EnumMember(Value = "group")]
    [JsonPropertyName("group")]
    Group,

    /// <summary>
    /// Supergroup chat with advanced features and larger member capacity.
    /// </summary>
    /// <remarks>
    /// Supergroups support features like message pinning, admin actions, and join requests.
    /// </remarks>
    [EnumMember(Value = "supergroup")]
    [JsonPropertyName("supergroup")]
    Supergroup,

    /// <summary>
    /// Channel chat for broadcasting messages to subscribers.
    /// </summary>
    /// <remarks>
    /// In channels, only admins can post, and subscribers cannot reply.
    /// </remarks>
    [EnumMember(Value = "channel")]
    [JsonPropertyName("channel")]
    Channel
}