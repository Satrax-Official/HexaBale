using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents an inline keyboard that appears below the message.
/// </summary>
/// <remarks>
/// Inline keyboards are attached to messages and allow users to interact via buttons.
/// </remarks>
public class BleInlineKeyboardMarkup
{
    /// <summary>
    /// Array of button rows, each row containing buttons.
    /// </summary>
    [JsonPropertyName("inline_keyboard")]
    public List<List<BleInlineKeyboardButton>> InlineKeyboard { get; set; } = new();
}