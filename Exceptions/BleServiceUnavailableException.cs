using System.Net;

namespace HexaBale.Exceptions;

/// <summary>
/// Exception thrown when the Bale API service is unavailable (HTTP 503).
/// </summary>
/// <remarks>
/// This exception occurs when the Bale API server is temporarily unavailable or under maintenance.
/// The bot should retry the request after a delay.
/// </remarks>
public class BleServiceUnavailableException : BleApiException
{
    /// <summary>
    /// Gets the retry after value if provided by the server.
    /// </summary>
    public int? RetryAfter { get; }

    /// <summary>
    /// Initializes a new instance of the BleServiceUnavailableException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="retryAfter">Seconds to wait before retrying.</param>
    public BleServiceUnavailableException(string message, int? retryAfter = null)
        : base(message, HttpStatusCode.ServiceUnavailable)
    {
        RetryAfter = retryAfter;
    }

    /// <summary>
    /// Returns a string representation of the exception.
    /// </summary>
    public override string ToString()
    {
        var result = $"BleServiceUnavailableException: {Message}";
        if (RetryAfter.HasValue)
            result += $"\nRetry After: {RetryAfter} seconds";
        return result;
    }
}