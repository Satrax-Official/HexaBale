using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a venue shared in a message.
/// </summary>
/// <remarks>
/// Venues combine a location with a name and address.
/// </remarks>
public class BleVenue
{
    /// <summary>
    /// Location of the venue (coordinates).
    /// </summary>
    [JsonPropertyName("location")]
    public BleLocation? Location { get; set; }

    /// <summary>
    /// Name of the venue.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Address of the venue.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Foursquare identifier of the venue (optional).
    /// </summary>
    [JsonPropertyName("foursquare_id")]
    public string? FoursquareId { get; set; }

    /// <summary>
    /// Foursquare type of the venue (optional).
    /// </summary>
    [JsonPropertyName("foursquare_type")]
    public string? FoursquareType { get; set; }

    /// <summary>
    /// Google Places identifier of the venue (optional).
    /// </summary>
    [JsonPropertyName("google_place_id")]
    public string? GooglePlaceId { get; set; }

    /// <summary>
    /// Google Places type of the venue (optional).
    /// </summary>
    [JsonPropertyName("google_place_type")]
    public string? GooglePlaceType { get; set; }
}