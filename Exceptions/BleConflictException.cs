using System.Net;

namespace HexaBale.Exceptions;

/// <summary>
/// Exception thrown when there is a conflict with the current state (HTTP 409).
/// </summary>
/// <remarks>
/// This exception occurs when an operation conflicts with the current resource state.
/// Examples: trying to send a message to a chat that the bot left, or setting a webhook when one already exists.
/// </remarks>
public class BleConflictException : BleApiException
{
    /// <summary>
    /// Initializes a new instance of the BleConflictException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BleConflictException(string message)
        : base(message, HttpStatusCode.Conflict)
    {
    }
}