using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents information about a Web App button.
/// </summary>
/// <remarks>
/// Web Apps are mini-applications that run within Bale Messenger.
/// </remarks>
public class BleWebAppInfo
{
    /// <summary>
    /// URL of the Web App to launch.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}