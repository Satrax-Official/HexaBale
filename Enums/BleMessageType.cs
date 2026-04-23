using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HexaBale.Enums;

/// <summary>
/// Represents the type of content in a message.
/// </summary>
/// <remarks>
/// Use this enum to quickly identify what kind of media or content a message contains.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BleMessageType
{
    /// <summary>
    /// Regular text message.
    /// </summary>
    [EnumMember(Value = "text")]
    [JsonPropertyName("text")]
    Text,

    /// <summary>
    /// Photo/image message.
    /// </summary>
    [EnumMember(Value = "photo")]
    [JsonPropertyName("photo")]
    Photo,

    /// <summary>
    /// Video message.
    /// </summary>
    [EnumMember(Value = "video")]
    [JsonPropertyName("video")]
    Video,

    /// <summary>
    /// Document/file message (PDF, ZIP, etc.).
    /// </summary>
    [EnumMember(Value = "document")]
    [JsonPropertyName("document")]
    Document,

    /// <summary>
    /// Audio message (music file).
    /// </summary>
    [EnumMember(Value = "audio")]
    [JsonPropertyName("audio")]
    Audio,

    /// <summary>
    /// Voice message (recorded voice).
    /// </summary>
    [EnumMember(Value = "voice")]
    [JsonPropertyName("voice")]
    Voice,

    /// <summary>
    /// Sticker message.
    /// </summary>
    [EnumMember(Value = "sticker")]
    [JsonPropertyName("sticker")]
    Sticker,

    /// <summary>
    /// Animation/GIF message.
    /// </summary>
    [EnumMember(Value = "animation")]
    [JsonPropertyName("animation")]
    Animation,

    /// <summary>
    /// Contact sharing message.
    /// </summary>
    [EnumMember(Value = "contact")]
    [JsonPropertyName("contact")]
    Contact,

    /// <summary>
    /// Location sharing message.
    /// </summary>
    [EnumMember(Value = "location")]
    [JsonPropertyName("location")]
    Location,

    /// <summary>
    /// Venue sharing message.
    /// </summary>
    [EnumMember(Value = "venue")]
    [JsonPropertyName("venue")]
    Venue,

    /// <summary>
    /// Poll message.
    /// </summary>
    [EnumMember(Value = "poll")]
    [JsonPropertyName("poll")]
    Poll,

    /// <summary>
    /// Dice message (random value).
    /// </summary>
    [EnumMember(Value = "dice")]
    [JsonPropertyName("dice")]
    Dice,

    /// <summary>
    /// Game message.
    /// </summary>
    [EnumMember(Value = "game")]
    [JsonPropertyName("game")]
    Game,

    /// <summary>
    /// Service message (member joined, left, title changed, etc.).
    /// </summary>
    [EnumMember(Value = "service")]
    [JsonPropertyName("service")]
    Service
}