using System.Net;

namespace HexaBale.Exceptions;

/// <summary>
/// Exception thrown when authentication fails (HTTP 401).
/// </summary>
/// <remarks>
/// This exception occurs when the bot token is invalid, expired, or not authorized.
/// Verify that your bot token is correct and the bot is not blocked.
/// </remarks>
public class BleUnauthorizedException : BleApiException
{
    /// <summary>
    /// Initializes a new instance of the BleUnauthorizedException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BleUnauthorizedException(string message)
        : base(message, HttpStatusCode.Unauthorized)
    {
    }

    /// <summary>
    /// Initializes a new instance from token validation.
    /// </summary>
    /// <param name="token">The invalid token.</param>
    /// <param name="innerException">Optional inner exception.</param>
    public BleUnauthorizedException(string token, Exception? innerException = null)
        : base($"Invalid bot token: {token}", innerException ?? new Exception())
    {
        // StatusCode is set in base constructor
    }
}