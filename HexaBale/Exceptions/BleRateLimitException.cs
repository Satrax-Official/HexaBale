using System.Net;

namespace HexaBale.Exceptions;

/// <summary>
/// Exception thrown when rate limit is hit (HTTP 429).
/// </summary>
/// <remarks>
/// This exception occurs when too many requests are sent to the API in a short period.
/// The bot should wait for the specified RetryAfter seconds before retrying.
/// </remarks>
public class BleRateLimitException : BleApiException
{
    /// <summary>
    /// Gets the number of seconds to wait before retrying.
    /// </summary>
    public int RetryAfterSeconds { get; }

    /// <summary>
    /// Initializes a new instance of the BleRateLimitException class.
    /// </summary>
    /// <param name="retryAfter">Number of seconds to wait.</param>
    /// <param name="responseBody">Raw response body from the API.</param>
    public BleRateLimitException(int retryAfter, string? responseBody)
        : base(HttpStatusCode.TooManyRequests, responseBody)
    {
        RetryAfterSeconds = retryAfter;
    }

    /// <summary>
    /// Returns a string representation of the exception.
    /// </summary>
    public override string ToString()
    {
        return $"[Rate Limited] Wait {RetryAfterSeconds} seconds before retrying.\n{base.ToString()}";
    }
}