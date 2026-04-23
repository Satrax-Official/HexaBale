using System.Text.Json.Serialization;
using HexaBale.Enums;

namespace HexaBale.Models;

/// <summary>
/// Represents an incoming update from Bale API.
/// </summary>
/// <remarks>
/// Updates are received via getUpdates method or webhook and contain new events like messages, edits, or callbacks.
/// </remarks>
public class BleUpdate
{
    /// <summary>
    /// The update's unique identifier.
    /// </summary>
    [JsonPropertyName("update_id")]
    public long UpdateId { get; set; }

    /// <summary>
    /// New incoming message of any kind (text, photo, etc.).
    /// </summary>
    [JsonPropertyName("message")]
    public BleMessage? Message { get; set; }

    /// <summary>
    /// New version of a message that is known to the bot and was edited.
    /// </summary>
    [JsonPropertyName("edited_message")]
    public BleMessage? EditedMessage { get; set; }

    /// <summary>
    /// New incoming channel post of any kind.
    /// </summary>
    [JsonPropertyName("channel_post")]
    public BleMessage? ChannelPost { get; set; }

    /// <summary>
    /// New version of a channel post that was edited.
    /// </summary>
    [JsonPropertyName("edited_channel_post")]
    public BleMessage? EditedChannelPost { get; set; }

    /// <summary>
    /// New incoming callback query (button click from inline keyboard).
    /// </summary>
    [JsonPropertyName("callback_query")]
    public BleCallbackQuery? CallbackQuery { get; set; }

    /// <summary>
    /// New incoming inline query (for inline bots).
    /// </summary>
    [JsonPropertyName("inline_query")]
    public BleInlineQuery? InlineQuery { get; set; }

    /// <summary>
    /// New incoming shipping query (for payments).
    /// </summary>
    [JsonPropertyName("shipping_query")]
    public BleShippingQuery? ShippingQuery { get; set; }

    /// <summary>
    /// New incoming pre-checkout query (for payments).
    /// </summary>
    [JsonPropertyName("pre_checkout_query")]
    public BlePreCheckoutQuery? PreCheckoutQuery { get; set; }

    /// <summary>
    /// New poll state (for polls).
    /// </summary>
    [JsonPropertyName("poll")]
    public BlePoll? Poll { get; set; }

    /// <summary>
    /// A user changed their answer in a non-anonymous poll.
    /// </summary>
    [JsonPropertyName("poll_answer")]
    public BlePollAnswer? PollAnswer { get; set; }

    /// <summary>
    /// The bot's chat member status was updated.
    /// </summary>
    [JsonPropertyName("my_chat_member")]
    public BleChatMemberUpdated? MyChatMember { get; set; }

    /// <summary>
    /// A chat member's status was updated.
    /// </summary>
    [JsonPropertyName("chat_member")]
    public BleChatMemberUpdated? ChatMember { get; set; }

    /// <summary>
    /// A request to join the chat has been sent.
    /// </summary>
    [JsonPropertyName("chat_join_request")]
    public BleChatJoinRequest? ChatJoinRequest { get; set; }

    /// <summary>
    /// Gets the type of this update.
    /// </summary>
    public BleUpdateType Type
    {
        get
        {
            if (Message != null) return BleUpdateType.Message;
            if (EditedMessage != null) return BleUpdateType.EditedMessage;
            if (ChannelPost != null) return BleUpdateType.ChannelPost;
            if (EditedChannelPost != null) return BleUpdateType.EditedChannelPost;
            if (CallbackQuery != null) return BleUpdateType.CallbackQuery;
            if (InlineQuery != null) return BleUpdateType.InlineQuery;
            if (ShippingQuery != null) return BleUpdateType.ShippingQuery;
            if (PreCheckoutQuery != null) return BleUpdateType.PreCheckoutQuery;
            if (Poll != null) return BleUpdateType.Poll;
            if (PollAnswer != null) return BleUpdateType.PollAnswer;
            if (MyChatMember != null) return BleUpdateType.MyChatMember;
            if (ChatMember != null) return BleUpdateType.ChatMember;
            if (ChatJoinRequest != null) return BleUpdateType.ChatJoinRequest;
            return BleUpdateType.Message;
        }
    }

    /// <summary>
    /// Gets the chat ID from the update (if available).
    /// </summary>
    public long? ChatId => Message?.Chat?.Id ?? EditedMessage?.Chat?.Id ?? ChannelPost?.Chat?.Id;
}