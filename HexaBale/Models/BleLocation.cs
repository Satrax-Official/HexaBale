using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a location shared in a message.
/// </summary>
/// <remarks>
/// Contains geographical coordinates and optional live location information.
/// </remarks>
public class BleLocation
{
    /// <summary>
    /// Longitude of the location.
    /// </summary>
    [JsonPropertyName("longitude")]
    public float Longitude { get; set; }

    /// <summary>
    /// Latitude of the location.
    /// </summary>
    [JsonPropertyName("latitude")]
    public float Latitude { get; set; }

    /// <summary>
    /// Horizontal accuracy of the location in meters (optional).
    /// </summary>
    [JsonPropertyName("horizontal_accuracy")]
    public float? HorizontalAccuracy { get; set; }

    /// <summary>
    /// Period in seconds for which the location can be updated (live location).
    /// </summary>
    [JsonPropertyName("live_period")]
    public int? LivePeriod { get; set; }

    /// <summary>
    /// Direction in which the user is moving (for live location).
    /// </summary>
    [JsonPropertyName("heading")]
    public int? Heading { get; set; }

    /// <summary>
    /// Maximum distance for proximity alerts (for live location).
    /// </summary>
    [JsonPropertyName("proximity_alert_radius")]
    public int? ProximityAlertRadius { get; set; }
}