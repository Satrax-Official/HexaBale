using System.Net;

namespace HexaBale.Exceptions;

/// <summary>
/// Exception thrown when access is forbidden (HTTP 403).
/// </summary>
/// <remarks>
/// This exception occurs when the bot does not have permission to perform an action.
/// Common scenarios: bot is not admin, bot is blocked by user, or bot cannot access a resource.
/// </remarks>
public class BleForbiddenException : BleApiException
{
    /// <summary>
    /// Gets the chat ID that access was denied for.
    /// </summary>
    public long? ChatId { get; }

    /// <summary>
    /// Initializes a new instance of the BleForbiddenException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BleForbiddenException(string message)
        : base(message, HttpStatusCode.Forbidden)
    {
    }

    /// <summary>
    /// Initializes a new instance with chat ID.
    /// </summary>
    /// <param name="chatId">Chat ID that access was denied for.</param>
    /// <param name="message">The error message.</param>
    public BleForbiddenException(long chatId, string message)
        : base(message, HttpStatusCode.Forbidden)
    {
        ChatId = chatId;
    }
}