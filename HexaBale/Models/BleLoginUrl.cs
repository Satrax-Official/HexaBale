using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a login URL button for authentication.
/// </summary>
/// <remarks>
/// Allows users to log in to external services using their Bale account.
/// </remarks>
public class BleLoginUrl
{
    /// <summary>
    /// URL to open for login.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Text to be shown to the user on the login button.
    /// </summary>
    [JsonPropertyName("forward_text")]
    public string? ForwardText { get; set; }

    /// <summary>
    /// Username of the bot that will handle the login.
    /// </summary>
    [JsonPropertyName("bot_username")]
    public string? BotUsername { get; set; }

    /// <summary>
    /// Whether the bot should request write access to the user's data.
    /// </summary>
    [JsonPropertyName("request_write_access")]
    public bool? RequestWriteAccess { get; set; }
}