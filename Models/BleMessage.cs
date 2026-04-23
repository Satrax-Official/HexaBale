using System.Text.Json.Serialization;
using HexaBale.Enums;

namespace HexaBale.Models;

/// <summary>
/// Represents a message sent or received on Bale Messenger.
/// </summary>
/// <remarks>
/// Contains all possible message content types including text, media, and service messages.
/// </remarks>
public class BleMessage
{
    /// <summary>
    /// Unique message identifier inside this chat.
    /// </summary>
    [JsonPropertyName("message_id")]
    public int MessageId { get; set; }

    /// <summary>
    /// Sender of the message (empty for messages sent to channels).
    /// </summary>
    [JsonPropertyName("from")]
    public BleUser? From { get; set; }

    /// <summary>
    /// Sender of the message (original sender in case of anonymous forwarding).
    /// </summary>
    [JsonPropertyName("sender_chat")]
    public BleChat? SenderChat { get; set; }

    /// <summary>
    /// Date the message was sent in Unix time (seconds).
    /// </summary>
    [JsonPropertyName("date")]
    public int Date { get; set; }

    /// <summary>
    /// Conversation the message belongs to.
    /// </summary>
    [JsonPropertyName("chat")]
    public BleChat? Chat { get; set; }

    /// <summary>
    /// For forwarded messages, sender of the original message.
    /// </summary>
    [JsonPropertyName("forward_from")]
    public BleUser? ForwardFrom { get; set; }

    /// <summary>
    /// For forwarded messages, chat of the original message.
    /// </summary>
    [JsonPropertyName("forward_from_chat")]
    public BleChat? ForwardFromChat { get; set; }

    /// <summary>
    /// For forwarded messages, date the original message was sent.
    /// </summary>
    [JsonPropertyName("forward_date")]
    public int? ForwardDate { get; set; }

    /// <summary>
    /// For replies, the original message being replied to.
    /// </summary>
    [JsonPropertyName("reply_to_message")]
    public BleMessage? ReplyToMessage { get; set; }

    /// <summary>
    /// Date the message was last edited in Unix time.
    /// </summary>
    [JsonPropertyName("edit_date")]
    public int? EditDate { get; set; }

    /// <summary>
    /// For text messages, the actual UTF-8 text of the message.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// For messages with a caption, the caption text.
    /// </summary>
    [JsonPropertyName("caption")]
    public string? Caption { get; set; }

    /// <summary>
    /// Media group ID for albums (messages sent together as a group).
    /// </summary>
    [JsonPropertyName("media_group_id")]
    public string? MediaGroupId { get; set; }

    /// <summary>
    /// Message is a document (file attachment).
    /// </summary>
    [JsonPropertyName("document")]
    public BleDocument? Document { get; set; }

    /// <summary>
    /// Message is a photo.
    /// </summary>
    [JsonPropertyName("photo")]
    public BlePhotoSize[]? Photo { get; set; }

    /// <summary>
    /// Message is a video.
    /// </summary>
    [JsonPropertyName("video")]
    public BleVideo? Video { get; set; }

    /// <summary>
    /// Message is an audio file (music).
    /// </summary>
    [JsonPropertyName("audio")]
    public BleAudio? Audio { get; set; }

    /// <summary>
    /// Message is a voice message.
    /// </summary>
    [JsonPropertyName("voice")]
    public BleVoice? Voice { get; set; }

    /// <summary>
    /// Message is a sticker.
    /// </summary>
    [JsonPropertyName("sticker")]
    public BleSticker? Sticker { get; set; }

    /// <summary>
    /// Message is an animation (GIF).
    /// </summary>
    [JsonPropertyName("animation")]
    public BleAnimation? Animation { get; set; }

    /// <summary>
    /// Message is a contact.
    /// </summary>
    [JsonPropertyName("contact")]
    public BleContact? Contact { get; set; }

    /// <summary>
    /// Message is a location.
    /// </summary>
    [JsonPropertyName("location")]
    public BleLocation? Location { get; set; }

    /// <summary>
    /// Message is a venue.
    /// </summary>
    [JsonPropertyName("venue")]
    public BleVenue? Venue { get; set; }

    /// <summary>
    /// Message is a poll.
    /// </summary>
    [JsonPropertyName("poll")]
    public BlePoll? Poll { get; set; }

    /// <summary>
    /// Message is a dice (random value game).
    /// </summary>
    [JsonPropertyName("dice")]
    public BleDice? Dice { get; set; }

    /// <summary>
    /// Message is a game.
    /// </summary>
    [JsonPropertyName("game")]
    public BleGame? Game { get; set; }

    /// <summary>
    /// New chat members that were added to the group.
    /// </summary>
    [JsonPropertyName("new_chat_members")]
    public BleUser[]? NewChatMembers { get; set; }

    /// <summary>
    /// A member was removed from the group.
    /// </summary>
    [JsonPropertyName("left_chat_member")]
    public BleUser? LeftChatMember { get; set; }

    /// <summary>
    /// Chat title was changed.
    /// </summary>
    [JsonPropertyName("new_chat_title")]
    public string? NewChatTitle { get; set; }

    /// <summary>
    /// Chat photo was changed.
    /// </summary>
    [JsonPropertyName("new_chat_photo")]
    public BlePhotoSize[]? NewChatPhoto { get; set; }

    /// <summary>
    /// Service message: chat photo was deleted.
    /// </summary>
    [JsonPropertyName("delete_chat_photo")]
    public bool? DeleteChatPhoto { get; set; }

    /// <summary>
    /// Service message: group was created.
    /// </summary>
    [JsonPropertyName("group_chat_created")]
    public bool? GroupChatCreated { get; set; }

    /// <summary>
    /// Service message: supergroup was created.
    /// </summary>
    [JsonPropertyName("supergroup_chat_created")]
    public bool? SupergroupChatCreated { get; set; }

    /// <summary>
    /// Service message: channel was created.
    /// </summary>
    [JsonPropertyName("channel_chat_created")]
    public bool? ChannelChatCreated { get; set; }

    /// <summary>
    /// Service message: message was pinned.
    /// </summary>
    [JsonPropertyName("pinned_message")]
    public BleMessage? PinnedMessage { get; set; }

    /// <summary>
    /// Gets the DateTime of the message (converted from Unix timestamp).
    /// </summary>
    public DateTime SentAt => DateTimeOffset.FromUnixTimeSeconds(Date).LocalDateTime;

    /// <summary>
    /// Gets the type of the message content.
    /// </summary>
    public BleMessageType ContentType
    {
        get
        {
            if (Text != null) return BleMessageType.Text;
            if (Photo != null && Photo.Length > 0) return BleMessageType.Photo;
            if (Video != null) return BleMessageType.Video;
            if (Document != null) return BleMessageType.Document;
            if (Audio != null) return BleMessageType.Audio;
            if (Voice != null) return BleMessageType.Voice;
            if (Sticker != null) return BleMessageType.Sticker;
            if (Animation != null) return BleMessageType.Animation;
            if (Contact != null) return BleMessageType.Contact;
            if (Location != null) return BleMessageType.Location;
            if (Poll != null) return BleMessageType.Poll;
            if (Game != null) return BleMessageType.Game;
            if (Dice != null) return BleMessageType.Dice;
            if (NewChatMembers != null || LeftChatMember != null || !string.IsNullOrEmpty(NewChatTitle))
                return BleMessageType.Service;
            return BleMessageType.Text;
        }
    }
}