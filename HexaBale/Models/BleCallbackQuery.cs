using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a callback query from an inline keyboard button click.
/// </summary>
/// <remarks>
/// Callback queries are sent when a user clicks an inline keyboard button with callback_data.
/// The bot can respond using AnswerCallbackQueryAsync.
/// </remarks>
public class BleCallbackQuery
{
    /// <summary>
    /// Unique identifier for this query.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// User who sent the query.
    /// </summary>
    [JsonPropertyName("from")]
    public BleUser? From { get; set; }

    /// <summary>
    /// Message associated with the callback (if the button belongs to a message).
    /// </summary>
    [JsonPropertyName("message")]
    public BleMessage? Message { get; set; }

    /// <summary>
    /// Identifier of the message that the button belongs to (alternative to message).
    /// </summary>
    [JsonPropertyName("inline_message_id")]
    public string? InlineMessageId { get; set; }

    /// <summary>
    /// Data associated with the callback button.
    /// </summary>
    [JsonPropertyName("data")]
    public string? Data { get; set; }

    /// <summary>
    /// Short name of a Game to be returned (if the button is for a game).
    /// </summary>
    [JsonPropertyName("game_short_name")]
    public string? GameShortName { get; set; }

    /// <summary>
    /// Gets the chat ID from the associated message.
    /// </summary>
    public long? ChatId => Message?.Chat?.Id;
}