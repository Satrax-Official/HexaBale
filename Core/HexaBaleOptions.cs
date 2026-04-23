namespace HexaBale.Core;

/// <summary>
/// Configuration options for the HexaBaleClient.
/// </summary>
/// <remarks>
/// Use these options to customize the behavior of the Bale API client.
/// </remarks>
public class HexaBaleOptions
{
    /// <summary>
    /// Gets or sets the HTTP request timeout in seconds.
    /// </summary>
    /// <remarks>
    /// Default value is 30 seconds. Increase for long-polling operations.
    /// </remarks>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Gets or sets the maximum number of retry attempts on rate limit errors.
    /// </summary>
    /// <remarks>
    /// Default value is 3 retries. Set to 0 to disable retries.
    /// </remarks>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Gets or sets whether to automatically retry on rate limit (429) errors.
    /// </summary>
    /// <remarks>
    /// Default value is true. When enabled, the client will wait for the retry_after
    /// period and automatically retry the request.
    /// </remarks>
    public bool AutoRetryOnRateLimit { get; set; } = true;

    /// <summary>
    /// Gets or sets the base URL for the Bale API.
    /// </summary>
    /// <remarks>
    /// Default is "https://tapi.bale.ai/bot". Change only if Bale changes their API endpoint.
    /// </remarks>
    public string BaseUrl { get; set; } = "https://tapi.bale.ai/bot";

    /// <summary>
    /// Gets or sets the default parse mode for messages.
    /// </summary>
    /// <remarks>
    /// Default is null (no parsing). Can be set to Html or MarkdownV2.
    /// </remarks>
    public string? DefaultParseMode { get; set; }

    /// <summary>
    /// Gets or sets whether to throw exceptions for API errors.
    /// </summary>
    /// <remarks>
    /// Default is true. When false, the client will return null instead of throwing.
    /// </remarks>
    public bool ThrowOnApiError { get; set; } = true;
}