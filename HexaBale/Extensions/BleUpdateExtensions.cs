using HexaBale.Models;
using HexaBale.Enums;

namespace HexaBale.Extensions;

/// <summary>
/// Extension methods for BleUpdate objects to simplify common operations.
/// </summary>
/// <remarks>
/// Provides helper methods for analyzing updates and extracting information.
/// </remarks>
public static class BleUpdateExtensions
{
    /// <summary>
    /// Gets the type of the update.
    /// </summary>
    /// <param name="update">The update to analyze.</param>
    /// <returns>The detected update type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when update is null.</exception>
    public static BleUpdateType GetUpdateType(this BleUpdate update)
    {
        if (update is null)
            throw new ArgumentNullException(nameof(update));

        if (update.Message != null)
            return BleUpdateType.Message;

        if (update.EditedMessage != null)
            return BleUpdateType.EditedMessage;

        if (update.ChannelPost != null)
            return BleUpdateType.ChannelPost;

        if (update.EditedChannelPost != null)
            return BleUpdateType.EditedChannelPost;

        if (update.CallbackQuery != null)
            return BleUpdateType.CallbackQuery;

        if (update.InlineQuery != null)
            return BleUpdateType.InlineQuery;

        if (update.ShippingQuery != null)
            return BleUpdateType.ShippingQuery;

        if (update.PreCheckoutQuery != null)
            return BleUpdateType.PreCheckoutQuery;

        if (update.Poll != null)
            return BleUpdateType.Poll;

        if (update.PollAnswer != null)
            return BleUpdateType.PollAnswer;

        if (update.MyChatMember != null)
            return BleUpdateType.MyChatMember;

        if (update.ChatMember != null)
            return BleUpdateType.ChatMember;

        if (update.ChatJoinRequest != null)
            return BleUpdateType.ChatJoinRequest;

        return BleUpdateType.Message;
    }

    /// <summary>
    /// Gets the chat ID from the update, if available.
    /// </summary>
    /// <param name="update">The update to extract chat ID from.</param>
    /// <returns>The chat ID, or null if not available.</returns>
    public static long? GetChatId(this BleUpdate update)
    {
        if (update is null)
            return null;

        if (update.Message?.Chat != null)
            return update.Message.Chat.Id;

        if (update.EditedMessage?.Chat != null)
            return update.EditedMessage.Chat.Id;

        if (update.ChannelPost?.Chat != null)
            return update.ChannelPost.Chat.Id;

        if (update.CallbackQuery?.Message?.Chat != null)
            return update.CallbackQuery.Message.Chat.Id;

        return null;
    }

    /// <summary>
    /// Gets the user ID from the update, if available.
    /// </summary>
    /// <param name="update">The update to extract user ID from.</param>
    /// <returns>The user ID, or null if not available.</returns>
    public static long? GetUserId(this BleUpdate update)
    {
        if (update is null)
            return null;

        if (update.Message?.From != null)
            return update.Message.From.Id;

        if (update.EditedMessage?.From != null)
            return update.EditedMessage.From.Id;

        if (update.CallbackQuery?.From != null)
            return update.CallbackQuery.From.Id;

        if (update.InlineQuery?.From != null)
            return update.InlineQuery.From.Id;

        return null;
    }

    /// <summary>
    /// Gets the message text from the update, if available.
    /// </summary>
    /// <param name="update">The update to extract message from.</param>
    /// <returns>The message text, or null if not available.</returns>
    public static string? GetMessageText(this BleUpdate update)
    {
        if (update is null)
            return null;

        if (update.Message != null)
            return update.Message.GetDisplayText();

        if (update.EditedMessage != null)
            return update.EditedMessage.GetDisplayText();

        if (update.ChannelPost != null)
            return update.ChannelPost.GetDisplayText();

        if (update.CallbackQuery?.Data != null)
            return update.CallbackQuery.Data;

        return null;
    }

    /// <summary>
    /// Checks if the update contains a callback query.
    /// </summary>
    /// <param name="update">The update to check.</param>
    /// <returns>True if the update is a callback query, otherwise false.</returns>
    public static bool IsCallbackQuery(this BleUpdate update)
    {
        if (update is null)
            return false;

        return update.CallbackQuery != null;
    }

    /// <summary>
    /// Checks if the update contains a message.
    /// </summary>
    /// <param name="update">The update to check.</param>
    /// <returns>True if the update contains a message, otherwise false.</returns>
    public static bool HasMessage(this BleUpdate update)
    {
        if (update is null)
            return false;

        return update.Message != null;
    }

    /// <summary>
    /// Checks if the update contains a channel post.
    /// </summary>
    /// <param name="update">The update to check.</param>
    /// <returns>True if the update contains a channel post, otherwise false.</returns>
    public static bool IsChannelPost(this BleUpdate update)
    {
        if (update is null)
            return false;

        return update.ChannelPost != null;
    }

    /// <summary>
    /// Gets the message object from the update, regardless of update type.
    /// </summary>
    /// <param name="update">The update to extract message from.</param>
    /// <returns>The message object, or null if not available.</returns>
    public static BleMessage? GetMessage(this BleUpdate update)
    {
        if (update is null)
            return null;

        return update.Message ?? update.EditedMessage ?? update.ChannelPost ?? update.EditedChannelPost;
    }
}