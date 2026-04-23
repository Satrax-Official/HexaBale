using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HexaBale.Enums;

/// <summary>
/// Represents the type of action an inline keyboard button performs.
/// </summary>
/// <remarks>
/// Different button types trigger different behaviors when clicked.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BleInlineKeyboardButtonType
{
    /// <summary>
    /// Button sends a callback query to the bot when clicked.
    /// </summary>
    /// <remarks>
    /// Use this for interactive menus, pagination, and any action that requires bot response.
    /// </remarks>
    [EnumMember(Value = "callback_data")]
    [JsonPropertyName("callback_data")]
    Callback,

    /// <summary>
    /// Button opens a URL in the user's browser.
    /// </summary>
    /// <remarks>
    /// The URL must be a valid HTTP/HTTPS link.
    /// </remarks>
    [EnumMember(Value = "url")]
    [JsonPropertyName("url")]
    Url,

    /// <summary>
    /// Button switches to inline query mode for the user.
    /// </summary>
    /// <remarks>
    /// The user can type a query and select an inline result.
    /// </remarks>
    [EnumMember(Value = "switch_inline_query")]
    [JsonPropertyName("switch_inline_query")]
    SwitchInlineQuery,

    /// <summary>
    /// Button switches to inline query mode in the current chat.
    /// </summary>
    /// <remarks>
    /// Similar to SwitchInlineQuery but works within the current chat context.
    /// </remarks>
    [EnumMember(Value = "switch_inline_query_current_chat")]
    [JsonPropertyName("switch_inline_query_current_chat")]
    SwitchInlineQueryCurrentChat,

    /// <summary>
    /// Button launches a Web App within Bale Messenger.
    /// </summary>
    /// <remarks>
    /// The Web App is a mini-application that runs inside the chat.
    /// </remarks>
    [EnumMember(Value = "web_app")]
    [JsonPropertyName("web_app")]
    WebApp,

    /// <summary>
    /// Button opens a login URL for authentication.
    /// </summary>
    /// <remarks>
    /// Supports automatic authorization via Telegram/Bale login widget.
    /// </remarks>
    [EnumMember(Value = "login_url")]
    [JsonPropertyName("login_url")]
    LoginUrl,

    /// <summary>
    /// Button copies specified text to the user's clipboard.
    /// </summary>
    /// <remarks>
    /// Useful for sharing codes, links, or any text that user might want to copy.
    /// </remarks>
    [EnumMember(Value = "copy_text")]
    [JsonPropertyName("copy_text")]
    CopyText,

    /// <summary>
    /// Button launches a game.
    /// </summary>
    /// <remarks>
    /// Used for HTML5 games integrated with Bale Messenger.
    /// </remarks>
    [EnumMember(Value = "callback_game")]
    [JsonPropertyName("callback_game")]
    CallbackGame,

    /// <summary>
    /// Button that requires payment to interact.
    /// </summary>
    /// <remarks>
    /// Used for premium content or paid features.
    /// </remarks>
    [EnumMember(Value = "pay")]
    [JsonPropertyName("pay")]
    Pay
}