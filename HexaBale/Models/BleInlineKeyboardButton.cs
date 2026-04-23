using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a single button in an inline keyboard.
/// </summary>
/// <remarks>
/// Buttons can perform various actions: send callback data, open URLs, launch Web Apps, etc.
/// </remarks>
public class BleInlineKeyboardButton
{
    /// <summary>
    /// Label text displayed on the button.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// HTTP URL to be opened when the button is pressed.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Data to be sent in a callback query to the bot when pressed.
    /// </summary>
    [JsonPropertyName("callback_data")]
    public string? CallbackData { get; set; }

    /// <summary>
    /// Web App that will be launched when the button is pressed.
    /// </summary>
    [JsonPropertyName("web_app")]
    public BleWebAppInfo? WebApp { get; set; }

    /// <summary>
    /// Login URL for authentication.
    /// </summary>
    [JsonPropertyName("login_url")]
    public BleLoginUrl? LoginUrl { get; set; }

    /// <summary>
    /// Switches to inline query mode with the specified text.
    /// </summary>
    [JsonPropertyName("switch_inline_query")]
    public string? SwitchInlineQuery { get; set; }

    /// <summary>
    /// Switches to inline query mode in the current chat.
    /// </summary>
    [JsonPropertyName("switch_inline_query_current_chat")]
    public string? SwitchInlineQueryCurrentChat { get; set; }

    /// <summary>
    /// Launches a game.
    /// </summary>
    [JsonPropertyName("callback_game")]
    public object? CallbackGame { get; set; }

    /// <summary>
    /// Copies the specified text to the user's clipboard.
    /// </summary>
    [JsonPropertyName("copy_text")]
    public BleCopyTextButton? CopyText { get; set; }

    /// <summary>
    /// Indicates whether the button is pay-enabled (for paid content).
    /// </summary>
    [JsonPropertyName("pay")]
    public bool? Pay { get; set; }
}