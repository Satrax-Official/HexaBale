using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents the standard API response wrapper from Bale.
/// </summary>
/// <typeparam name="T">The type of the result data.</typeparam>
/// <remarks>
/// All Bale API responses follow this structure with an 'ok' status and either a 'result' or error information.
/// </remarks>
public class BleApiResponse<T>
{
    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    [JsonPropertyName("ok")]
    public bool Ok { get; set; }

    /// <summary>
    /// The result data of the successful request.
    /// </summary>
    [JsonPropertyName("result")]
    public T? Result { get; set; }

    /// <summary>
    /// The error code if the request failed.
    /// </summary>
    [JsonPropertyName("error_code")]
    public int? ErrorCode { get; set; }

    /// <summary>
    /// Description of the error if the request failed.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Additional parameters for error responses (e.g., retry_after).
    /// </summary>
    [JsonPropertyName("parameters")]
    public BleResponseParameters? Parameters { get; set; }
}