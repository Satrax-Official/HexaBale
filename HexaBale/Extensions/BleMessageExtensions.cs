using HexaBale.Models;
using HexaBale.Enums;

namespace HexaBale.Extensions;

/// <summary>
/// Extension methods for BleMessage objects to simplify common operations.
/// </summary>
/// <remarks>
/// Provides helper methods for analyzing message content and extracting information.
/// </remarks>
public static class BleMessageExtensions
{
    /// <summary>
    /// Gets the type of the message based on its content.
    /// </summary>
    /// <param name="message">The message to analyze.</param>
    /// <returns>The detected message type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when message is null.</exception>
    public static BleMessageType GetMessageType(this BleMessage message)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        if (message.Photo != null && message.Photo.Length > 0)
            return BleMessageType.Photo;

        if (message.Video != null)
            return BleMessageType.Video;

        if (message.Document != null)
            return BleMessageType.Document;

        if (message.Audio != null)
            return BleMessageType.Audio;

        if (message.Voice != null)
            return BleMessageType.Voice;

        if (message.Sticker != null)
            return BleMessageType.Sticker;

        if (message.Animation != null)
            return BleMessageType.Animation;

        if (message.Contact != null)
            return BleMessageType.Contact;

        if (message.Location != null)
            return BleMessageType.Location;

        if (message.Venue != null)
            return BleMessageType.Venue;

        if (message.Poll != null)
            return BleMessageType.Poll;

        if (message.Dice != null)
            return BleMessageType.Dice;

        if (message.Game != null)
            return BleMessageType.Game;

        if (message.NewChatMembers != null ||
            message.LeftChatMember != null ||
            !string.IsNullOrEmpty(message.NewChatTitle))
            return BleMessageType.Service;

        if (!string.IsNullOrEmpty(message.Text))
            return BleMessageType.Text;

        return BleMessageType.Text;
    }

    /// <summary>
    /// Checks if the message contains any media (photo, video, document, etc.).
    /// </summary>
    /// <param name="message">The message to check.</param>
    /// <returns>True if the message contains media, otherwise false.</returns>
    public static bool HasMedia(this BleMessage message)
    {
        if (message is null)
            return false;

        return message.Photo != null && message.Photo.Length > 0 ||
               message.Video != null ||
               message.Document != null ||
               message.Audio != null ||
               message.Voice != null ||
               message.Sticker != null ||
               message.Animation != null;
    }

    /// <summary>
    /// Gets the file ID of the media in the message, if any.
    /// </summary>
    /// <param name="message">The message to extract file ID from.</param>
    /// <returns>The file ID, or null if no media found.</returns>
    public static string? GetFileId(this BleMessage message)
    {
        if (message is null)
            return null;

        if (message.Document != null)
            return message.Document.FileId;

        if (message.Video != null)
            return message.Video.FileId;

        if (message.Audio != null)
            return message.Audio.FileId;

        if (message.Voice != null)
            return message.Voice.FileId;

        if (message.Sticker != null)
            return message.Sticker.FileId;

        if (message.Animation != null)
            return message.Animation.FileId;

        if (message.Photo != null && message.Photo.Length > 0)
            return message.Photo.Last().FileId;

        return null;
    }

    /// <summary>
    /// Gets the file size of the media in the message, if any.
    /// </summary>
    /// <param name="message">The message to extract file size from.</param>
    /// <returns>The file size in bytes, or null if no media found.</returns>
    public static long? GetFileSize(this BleMessage message)
    {
        if (message is null)
            return null;

        if (message.Document != null)
            return message.Document.FileSize;

        if (message.Video != null)
            return message.Video.FileSize;

        if (message.Audio != null)
            return message.Audio.FileSize;

        if (message.Voice != null)
            return message.Voice.FileSize;

        if (message.Sticker != null)
            return message.Sticker.FileSize;

        if (message.Animation != null)
            return message.Animation.FileSize;

        if (message.Photo != null && message.Photo.Length > 0)
            return message.Photo.Last().FileSize;

        return null;
    }

    /// <summary>
    /// Gets the file name of the media in the message, if any.
    /// </summary>
    /// <param name="message">The message to extract file name from.</param>
    /// <returns>The file name, or null if not available.</returns>
    public static string? GetFileName(this BleMessage message)
    {
        if (message is null)
            return null;

        if (message.Document?.FileName != null)
            return message.Document.FileName;

        if (message.Animation?.FileName != null)
            return message.Animation.FileName;

        if (message.Video != null)
            return $"video_{message.Video.FileUniqueId}.mp4";

        if (message.Audio != null)
            return $"audio_{message.Audio.FileUniqueId}.mp3";

        if (message.Photo != null && message.Photo.Length > 0)
            return $"photo_{message.Photo.Last().FileUniqueId}.jpg";

        return null;
    }

    /// <summary>
    /// Gets the display text of the message (text or caption).
    /// </summary>
    /// <param name="message">The message to get text from.</param>
    /// <returns>The message text or caption, or null if not available.</returns>
    public static string? GetDisplayText(this BleMessage message)
    {
        if (message is null)
            return null;

        return !string.IsNullOrEmpty(message.Text)
            ? message.Text
            : message.Caption;
    }

    /// <summary>
    /// Checks if the message is part of a media group (album).
    /// </summary>
    /// <param name="message">The message to check.</param>
    /// <returns>True if the message is part of an album, otherwise false.</returns>
    public static bool IsPartOfAlbum(this BleMessage message)
    {
        if (message is null)
            return false;

        return !string.IsNullOrEmpty(message.MediaGroupId);
    }

    /// <summary>
    /// Checks if the message is a reply to another message.
    /// </summary>
    /// <param name="message">The message to check.</param>
    /// <returns>True if the message is a reply, otherwise false.</returns>
    public static bool IsReply(this BleMessage message)
    {
        if (message is null)
            return false;

        return message.ReplyToMessage != null;
    }

    /// <summary>
    /// Gets the chat ID of the message.
    /// </summary>
    /// <param name="message">The message to get chat ID from.</param>
    /// <returns>The chat ID, or null if not available.</returns>
    public static long? GetChatId(this BleMessage message)
    {
        if (message is null)
            return null;

        return message.Chat?.Id;
    }

    /// <summary>
    /// Gets the user ID of the message sender.
    /// </summary>
    /// <param name="message">The message to get user ID from.</param>
    /// <returns>The user ID, or null if not available (e.g., channel posts).</returns>
    public static long? GetUserId(this BleMessage message)
    {
        if (message is null)
            return null;

        return message.From?.Id;
    }
}