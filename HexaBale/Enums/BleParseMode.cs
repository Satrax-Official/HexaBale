using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HexaBale.Enums;

/// <summary>
/// Represents the parsing mode for message text formatting.
/// </summary>
/// <remarks>
/// Parse modes allow you to format text with bold, italic, links, and other styling.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BleParseMode
{
    /// <summary>
    /// HTML formatting tags for message styling.
    /// </summary>
    /// <remarks>
    /// Supported tags: &lt;b&gt;, &lt;i&gt;, &lt;a href="url"&gt;, &lt;code&gt;, &lt;pre&gt;, etc.
    /// </remarks>
    [EnumMember(Value = "HTML")]
    [JsonPropertyName("HTML")]
    Html,

    /// <summary>
    /// MarkdownV2 formatting with special characters that must be escaped.
    /// </summary>
    /// <remarks>
    /// Supports: *bold*, _italic_, [link](url), `code`, ```pre```, etc.
    /// Special characters: _ * [ ] ( ) ~ ` > # + - = | { } . ! must be escaped with \
    /// </remarks>
    [EnumMember(Value = "MarkdownV2")]
    [JsonPropertyName("MarkdownV2")]
    MarkdownV2,

    /// <summary>
    /// Legacy Markdown formatting (deprecated, use MarkdownV2 instead).
    /// </summary>
    /// <remarks>
    /// This mode is maintained for backward compatibility. New bots should use MarkdownV2.
    /// </remarks>
    [EnumMember(Value = "Markdown")]
    [JsonPropertyName("Markdown")]
    Markdown
}