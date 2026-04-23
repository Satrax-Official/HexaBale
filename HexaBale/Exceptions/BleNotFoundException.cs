using System.Net;

namespace HexaBale.Exceptions;

/// <summary>
/// Exception thrown when a resource is not found (HTTP 404).
/// </summary>
/// <remarks>
/// This exception occurs when the requested resource (user, chat, message, file, etc.) does not exist.
/// </remarks>
public class BleNotFoundException : BleApiException
{
    /// <summary>
    /// Gets the resource type that was not found.
    /// </summary>
    public string? ResourceType { get; }

    /// <summary>
    /// Gets the resource identifier that was not found.
    /// </summary>
    public string? ResourceId { get; }

    /// <summary>
    /// Initializes a new instance of the BleNotFoundException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BleNotFoundException(string message)
        : base(message, HttpStatusCode.NotFound)
    {
    }

    /// <summary>
    /// Initializes a new instance with resource details.
    /// </summary>
    /// <param name="resourceType">Type of resource not found (e.g., "User", "Chat", "Message").</param>
    /// <param name="resourceId">Identifier of the resource.</param>
    public BleNotFoundException(string resourceType, string resourceId)
        : base($"{resourceType} with ID '{resourceId}' was not found", HttpStatusCode.NotFound)
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    /// <summary>
    /// Initializes a new instance with resource details and inner exception.
    /// </summary>
    /// <param name="resourceType">Type of resource not found.</param>
    /// <param name="resourceId">Identifier of the resource.</param>
    /// <param name="innerException">The inner exception.</param>
    public BleNotFoundException(string resourceType, string resourceId, Exception innerException)
        : base($"{resourceType} with ID '{resourceId}' was not found", innerException)
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }
}