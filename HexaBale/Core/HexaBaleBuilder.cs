using Microsoft.Extensions.DependencyInjection;
using HexaBale.Core;

namespace HexaBale.Core;

/// <summary>
/// Builder class for configuring HexaBaleClient during dependency injection registration.
/// </summary>
/// <remarks>
/// This builder provides a fluent interface for customizing the HexaBaleClient configuration.
/// </remarks>
public class HexaBaleBuilder
{
    /// <summary>
    /// Gets the service collection being configured.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Gets the bot token being used for configuration.
    /// </summary>
    public string Token { get; }

    /// <summary>
    /// Initializes a new instance of the HexaBaleBuilder class.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="token">The bot token.</param>
    public HexaBaleBuilder(IServiceCollection services, string token)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }

    /// <summary>
    /// Configures the HexaBaleClient options.
    /// </summary>
    /// <param name="configure">Action to configure HexaBaleOptions.</param>
    /// <returns>The same builder instance for chaining.</returns>
    public HexaBaleBuilder ConfigureOptions(Action<HexaBaleOptions> configure)
    {
        if (configure is null)
            throw new ArgumentNullException(nameof(configure));

        Services.Configure(configure);
        return this;
    }

    /// <summary>
    /// Configures the HttpClient used by HexaBaleClient.
    /// </summary>
    /// <param name="configure">Action to configure HttpClient.</param>
    /// <returns>The same builder instance for chaining.</returns>
    public HexaBaleBuilder ConfigureHttpClient(Action<HttpClient> configure)
    {
        if (configure is null)
            throw new ArgumentNullException(nameof(configure));

        Services.AddHttpClient<HexaBaleClient>((sp, client) => configure(client));
        return this;
    }

    /// <summary>
    /// Adds a custom handler to the HttpClient pipeline.
    /// </summary>
    /// <typeparam name="T">Type of the handler to add.</typeparam>
    /// <returns>The same builder instance for chaining.</returns>
    public HexaBaleBuilder AddHttpHandler<T>() where T : DelegatingHandler
    {
        Services.AddTransient<T>();
        Services.AddHttpClient<HexaBaleClient>().AddHttpMessageHandler<T>();
        return this;
    }
}