using System.Net;
using System.Text;

namespace HexaBale.Exceptions;

/// <summary>
/// Represents errors that occur during Bale API requests.
/// </summary>
/// <remarks>
/// This is the base exception for all Bale API related errors.
/// It captures the HTTP status code and response body for debugging.
/// </remarks>
public class BleApiException : Exception
{
    /// <summary>
    /// Gets the HTTP status code returned by the API.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the raw response body from the API.
    /// </summary>
    public string? ResponseBody { get; }

    /// <summary>
    /// Gets the error code from the API response (if available).
    /// </summary>
    public int? ApiErrorCode { get; }

    /// <summary>
    /// Gets the retry after value for rate limit errors (429).
    /// </summary>
    public int? RetryAfter { get; }

    /// <summary>
    /// Gets the parameter name that caused the error (if applicable).
    /// </summary>
    public string? ParameterName { get; }

    /// <summary>
    /// Initializes a new instance of the BleApiException class.
    /// </summary>
    /// <param name="statusCode">HTTP status code from the response.</param>
    /// <param name="responseBody">Raw response body from the API.</param>
    public BleApiException(HttpStatusCode statusCode, string? responseBody)
        : base($"Bale API error: {(int)statusCode} - {statusCode}")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;

        // Try to parse additional error information from response
        if (!string.IsNullOrEmpty(responseBody))
        {
            try
            {
                using var doc = System.Text.Json.JsonDocument.Parse(responseBody);

                if (doc.RootElement.TryGetProperty("error_code", out var errorCode))
                    ApiErrorCode = errorCode.GetInt32();

                if (doc.RootElement.TryGetProperty("description", out var description))
                {
                    var desc = description.GetString();
                    if (!string.IsNullOrEmpty(desc))
                        UpdateMessage(desc);
                }

                if (doc.RootElement.TryGetProperty("parameters", out var parameters))
                {
                    if (parameters.TryGetProperty("retry_after", out var retryAfter))
                        RetryAfter = retryAfter.GetInt32();

                    if (parameters.TryGetProperty("migrate_to_chat_id", out var migrateTo))
                        Data["MigrateToChatId"] = migrateTo.GetInt64();
                }
            }
            catch
            {
                // Ignore JSON parsing errors
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the BleApiException class with a custom message.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">HTTP status code from the response.</param>
    public BleApiException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the BleApiException class with a parameter name.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="parameterName">Name of the parameter that caused the error.</param>
    /// <param name="statusCode">HTTP status code from the response.</param>
    public BleApiException(string message, string parameterName, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        StatusCode = statusCode;
        ParameterName = parameterName;
    }

    /// <summary>
    /// Initializes a new instance of the BleApiException class with an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public BleApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    private void UpdateMessage(string description)
    {
        var field = typeof(Exception).GetField("_message", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        if (field != null)
            field.SetValue(this, $"{Message}\nDetails: {description}");
    }

    /// <summary>
    /// Returns a string representation of the exception.
    /// </summary>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"BleApiException: {Message}");
        sb.AppendLine($"Status Code: {(int)StatusCode} ({StatusCode})");

        if (ApiErrorCode.HasValue)
            sb.AppendLine($"API Error Code: {ApiErrorCode}");

        if (RetryAfter.HasValue)
            sb.AppendLine($"Retry After: {RetryAfter} seconds");

        if (ParameterName != null)
            sb.AppendLine($"Parameter: {ParameterName}");

        if (ResponseBody != null)
            sb.AppendLine($"Response: {ResponseBody}");

        sb.Append(StackTrace);
        return sb.ToString();
    }
}