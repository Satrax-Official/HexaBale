using HexaBale.Models;
using HexaBale.Enums;

namespace HexaBale.Extensions;

/// <summary>
/// Extension methods for BleChat objects.
/// </summary>
/// <remarks>
/// Provides helper methods for chat-related operations.
/// </remarks>
public static class BleChatExtensions
{
    /// <summary>
    /// Checks if the chat is a private chat.
    /// </summary>
    /// <param name="chat">The chat to check.</param>
    /// <returns>True if the chat is a private chat, otherwise false.</returns>
    public static bool IsPrivate(this BleChat chat)
    {
        if (chat is null)
            return false;

        return chat.Type == BleChatType.Private;
    }

    /// <summary>
    /// Checks if the chat is a group or supergroup.
    /// </summary>
    /// <param name="chat">The chat to check.</param>
    /// <returns>True if the chat is a group or supergroup, otherwise false.</returns>
    public static bool IsGroup(this BleChat chat)
    {
        if (chat is null)
            return false;

        return chat.Type == BleChatType.Group || chat.Type == BleChatType.Supergroup;
    }

    /// <summary>
    /// Checks if the chat is a channel.
    /// </summary>
    /// <param name="chat">The chat to check.</param>
    /// <returns>True if the chat is a channel, otherwise false.</returns>
    public static bool IsChannel(this BleChat chat)
    {
        if (chat is null)
            return false;

        return chat.Type == BleChatType.Channel;
    }

    /// <summary>
    /// Gets the display name of the chat (title, username, or ID).
    /// </summary>
    /// <param name="chat">The chat to get display name from.</param>
    /// <returns>The display name of the chat.</returns>
    public static string GetDisplayName(this BleChat chat)
    {
        if (chat is null)
            return "Unknown";

        return chat.Title ?? chat.Username ?? chat.FirstName ?? chat.Id.ToString();
    }
}