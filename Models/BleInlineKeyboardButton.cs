using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a button in an inline keyboard that appears below a message.
/// </summary>
public class BleInlineKeyboardButton
{
    /// <summary>
    /// Gets or sets the text displayed on the button.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the callback data to be sent to the bot when the button is pressed.
    /// </summary>
    [JsonPropertyName("callback_data")]
    public string? CallbackData { get; set; }

    /// <summary>
    /// Gets or sets the URL to be opened when the button is pressed.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the login URL for OAuth2 authentication.
    /// </summary>
    [JsonPropertyName("login_url")]
    public BleLoginUrl? LoginUrl { get; set; }

    /// <summary>
    /// Gets or sets the Web App information for launching a mini-app.
    /// </summary>
    [JsonPropertyName("web_app")]
    public BleWebAppInfo? WebApp { get; set; }

    /// <summary>
    /// Gets or sets the copy text button configuration.
    /// </summary>
    [JsonPropertyName("copy_text")]
    public BleCopyTextButton? CopyText { get; set; }

    /// <summary>
    /// Gets or sets the query to be sent when switching to inline mode.
    /// </summary>
    [JsonPropertyName("switch_inline_query")]
    public string? SwitchInlineQuery { get; set; }

    /// <summary>
    /// Gets or sets the query to be sent when switching to inline mode in the current chat.
    /// </summary>
    [JsonPropertyName("switch_inline_query_current_chat")]
    public string? SwitchInlineQueryCurrentChat { get; set; }

    /// <summary>
    /// Gets or sets game data for callback games.
    /// </summary>
    [JsonPropertyName("callback_game")]
    public object? CallbackGame { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the button sends a payment request.
    /// </summary>
    [JsonPropertyName("pay")]
    public bool? Pay { get; set; }

    // Constructors
    public BleInlineKeyboardButton()
    {
    }

    public BleInlineKeyboardButton(string text)
    {
        Text = text;
    }

    public BleInlineKeyboardButton(string text, string callbackData)
    {
        Text = text;
        CallbackData = callbackData;
    }

    public BleInlineKeyboardButton(string text, string? callbackData = null, string? url = null)
    {
        Text = text;
        CallbackData = callbackData;
        Url = url;
    }
}

