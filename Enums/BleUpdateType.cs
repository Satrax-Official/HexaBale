using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HexaBale.Enums;

/// <summary>
/// Represents the type of update received from Bale API.
/// </summary>
/// <remarks>
/// Used for filtering webhook updates or categorizing incoming updates.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BleUpdateType
{
    /// <summary>
    /// New incoming message of any kind.
    /// </summary>
    [EnumMember(Value = "message")]
    [JsonPropertyName("message")]
    Message,

    /// <summary>
    /// Edited message update.
    /// </summary>
    [EnumMember(Value = "edited_message")]
    [JsonPropertyName("edited_message")]
    EditedMessage,

    /// <summary>
    /// New channel post update.
    /// </summary>
    [EnumMember(Value = "channel_post")]
    [JsonPropertyName("channel_post")]
    ChannelPost,

    /// <summary>
    /// Edited channel post update.
    /// </summary>
    [EnumMember(Value = "edited_channel_post")]
    [JsonPropertyName("edited_channel_post")]
    EditedChannelPost,

    /// <summary>
    /// Callback query from an inline keyboard button click.
    /// </summary>
    [EnumMember(Value = "callback_query")]
    [JsonPropertyName("callback_query")]
    CallbackQuery,

    /// <summary>
    /// Inline query from a user (for inline bots).
    /// </summary>
    [EnumMember(Value = "inline_query")]
    [JsonPropertyName("inline_query")]
    InlineQuery,

    /// <summary>
    /// Result of a chosen inline query.
    /// </summary>
    [EnumMember(Value = "chosen_inline_result")]
    [JsonPropertyName("chosen_inline_result")]
    ChosenInlineResult,

    /// <summary>
    /// Shipping query for payment processing.
    /// </summary>
    [EnumMember(Value = "shipping_query")]
    [JsonPropertyName("shipping_query")]
    ShippingQuery,

    /// <summary>
    /// Pre-checkout query for payment processing.
    /// </summary>
    [EnumMember(Value = "pre_checkout_query")]
    [JsonPropertyName("pre_checkout_query")]
    PreCheckoutQuery,

    /// <summary>
    /// Poll state update.
    /// </summary>
    [EnumMember(Value = "poll")]
    [JsonPropertyName("poll")]
    Poll,

    /// <summary>
    /// User's answer to a poll.
    /// </summary>
    [EnumMember(Value = "poll_answer")]
    [JsonPropertyName("poll_answer")]
    PollAnswer,

    /// <summary>
    /// Bot's chat member status update.
    /// </summary>
    [EnumMember(Value = "my_chat_member")]
    [JsonPropertyName("my_chat_member")]
    MyChatMember,

    /// <summary>
    /// Chat member status update (when a user's status changes).
    /// </summary>
    [EnumMember(Value = "chat_member")]
    [JsonPropertyName("chat_member")]
    ChatMember,

    /// <summary>
    /// Request to join a chat.
    /// </summary>
    [EnumMember(Value = "chat_join_request")]
    [JsonPropertyName("chat_join_request")]
    ChatJoinRequest
}