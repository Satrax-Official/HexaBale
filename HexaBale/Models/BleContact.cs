using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a contact shared in a message.
/// </summary>
/// <remarks>
/// Contacts contain phone number and name information of a user.
/// </remarks>
public class BleContact
{
    /// <summary>
    /// Contact's phone number.
    /// </summary>
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Contact's first name.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Contact's last name (optional).
    /// </summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Contact's user identifier (if known by the bot).
    /// </summary>
    [JsonPropertyName("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Additional data about the contact in vCard format.
    /// </summary>
    [JsonPropertyName("vcard")]
    public string? Vcard { get; set; }
}