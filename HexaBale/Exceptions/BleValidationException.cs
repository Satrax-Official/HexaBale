using System.Net;
using System.Text;

namespace HexaBale.Exceptions;

/// <summary>
/// Exception thrown when validation fails (e.g., invalid parameters).
/// </summary>
/// <remarks>
/// This exception occurs when request parameters are invalid or malformed.
/// Check the ValidationErrors dictionary for specific field errors.
/// </remarks>
public class BleValidationException : BleApiException
{
    /// <summary>
    /// Gets the validation errors dictionary with field names and error messages.
    /// </summary>
    public Dictionary<string, string> ValidationErrors { get; } = new();

    /// <summary>
    /// Initializes a new instance of the BleValidationException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BleValidationException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// Initializes a new instance with a parameter name.
    /// </summary>
    /// <param name="parameterName">Name of the invalid parameter.</param>
    /// <param name="message">The error message.</param>
    public BleValidationException(string parameterName, string message)
        : base(message, parameterName, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// Adds a validation error for a specific field.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="errorMessage">Error message for the field.</param>
    public void AddValidationError(string fieldName, string errorMessage)
    {
        ValidationErrors[fieldName] = errorMessage;
    }

    /// <summary>
    /// Returns a string representation of the exception.
    /// </summary>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"BleValidationException: {Message}");

        if (ValidationErrors.Count > 0)
        {
            sb.AppendLine("Validation Errors:");
            foreach (var error in ValidationErrors)
            {
                sb.AppendLine($"  - {error.Key}: {error.Value}");
            }
        }

        sb.Append(base.ToString());
        return sb.ToString();
    }
}