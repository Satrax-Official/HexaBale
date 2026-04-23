using System.Text;
using HexaBale.Enums;

namespace HexaBale.Extensions;

/// <summary>
/// Extension methods for string manipulation in Bale messages.
/// </summary>
/// <remarks>
/// Provides utilities for escaping special characters and formatting text for different parse modes.
/// </remarks>
public static class BleStringExtensions
{
    /// <summary>
    /// Escapes special characters for MarkdownV2 parse mode.
    /// </summary>
    /// <param name="text">The text to escape.</param>
    /// <returns>The escaped text safe for MarkdownV2.</returns>
    /// <remarks>
    /// MarkdownV2 requires escaping of these characters: _ * [ ] ( ) ~ ` > # + - = | { } . !
    /// </remarks>
    /// <example>
    /// <code>
    /// var safeText = "Hello (world)".EscapeMarkdownV2();
    /// // Result: "Hello \(world\)"
    /// </code>
    /// </example>
    public static string EscapeMarkdownV2(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text ?? string.Empty;

        var specialChars = new[] { '_', '*', '[', ']', '(', ')', '~', '`', '>', '#', '+', '-', '=', '|', '{', '}', '.', '!' };
        var sb = new StringBuilder(text);

        foreach (var ch in specialChars)
        {
            sb.Replace(ch.ToString(), $"\\{ch}");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Escapes HTML special characters for HTML parse mode.
    /// </summary>
    /// <param name="text">The text to escape.</param>
    /// <returns>The escaped text safe for HTML.</returns>
    /// <remarks>
    /// HTML requires escaping of: &amp;, &lt;, &gt;, &quot;
    /// </remarks>
    /// <example>
    /// <code>
    /// var safeText = "Hello & World".EscapeHtml();
    /// // Result: "Hello &amp; World"
    /// </code>
    /// </example>
    public static string EscapeHtml(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text ?? string.Empty;

        return text
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;");
    }

    /// <summary>
    /// Applies the appropriate escaping based on the parse mode.
    /// </summary>
    /// <param name="text">The text to format.</param>
    /// <param name="parseMode">The parse mode to use for escaping.</param>
    /// <returns>The escaped text appropriate for the specified parse mode.</returns>
    public static string ApplyParseMode(this string text, BleParseMode parseMode)
    {
        if (string.IsNullOrEmpty(text))
            return text ?? string.Empty;

        return parseMode switch
        {
            BleParseMode.Html => text.EscapeHtml(),
            BleParseMode.MarkdownV2 => text.EscapeMarkdownV2(),
            _ => text
        };
    }

    /// <summary>
    /// Creates a user mention link for the specified username.
    /// </summary>
    /// <param name="username">The username of the user (without @ symbol).</param>
    /// <param name="displayName">The display name to show for the mention.</param>
    /// <param name="parseMode">The parse mode to use for formatting.</param>
    /// <returns>A formatted mention string.</returns>
    /// <example>
    /// <code>
    /// var mention = "john_doe".ToUserMention("John", BleParseMode.Html);
    /// // Result: "&lt;a href=\"https://t.me/john_doe\"&gt;John&lt;/a&gt;"
    /// </code>
    /// </example>
    public static string ToUserMention(this string username, string displayName, BleParseMode parseMode = BleParseMode.Html)
    {
        if (string.IsNullOrEmpty(username))
            return displayName;

        var escapedName = displayName.ApplyParseMode(parseMode);

        return parseMode switch
        {
            BleParseMode.Html => $"<a href=\"https://t.me/{username}\">{escapedName}</a>",
            BleParseMode.MarkdownV2 => $"[{escapedName}](https://t.me/{username})",
            _ => $"@{username}"
        };
    }

    /// <summary>
    /// Creates a channel link for the specified channel username.
    /// </summary>
    /// <param name="channelUsername">The channel username (without @ symbol).</param>
    /// <param name="displayName">The display name to show for the link.</param>
    /// <param name="parseMode">The parse mode to use for formatting.</param>
    /// <returns>A formatted channel link string.</returns>
    public static string ToChannelLink(this string channelUsername, string displayName, BleParseMode parseMode = BleParseMode.Html)
    {
        if (string.IsNullOrEmpty(channelUsername))
            return displayName;

        var escapedName = displayName.ApplyParseMode(parseMode);

        return parseMode switch
        {
            BleParseMode.Html => $"<a href=\"https://t.me/{channelUsername}\">{escapedName}</a>",
            BleParseMode.MarkdownV2 => $"[{escapedName}](https://t.me/{channelUsername})",
            _ => $"@{channelUsername}"
        };
    }

    /// <summary>
    /// Truncates text to a maximum length and adds ellipsis if needed.
    /// </summary>
    /// <param name="text">The text to truncate.</param>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <returns>Truncated text with ellipsis if necessary.</returns>
    public static string Truncate(this string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text ?? string.Empty;

        return text[..(maxLength - 3)] + "...";
    }
}