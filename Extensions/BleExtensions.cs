using Microsoft.Extensions.DependencyInjection;
using HexaBale.Core;

namespace HexaBale.Extensions;

/// <summary>
/// Extension methods for registering HexaBale services in the dependency injection container.
/// </summary>
/// <remarks>
/// These methods simplify the setup of HexaBaleClient in ASP.NET Core applications.
/// </remarks>
public static class BleExtensions
{
    /// <summary>
    /// Adds HexaBaleClient to the service collection with the specified bot token.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the client to.</param>
    /// <param name="token">The bot token obtained from @BotFather on Bale.</param>
    /// <returns>A HexaBaleBuilder for further configuration.</returns>
    /// <example>
    /// <code>
    /// // In Program.cs
    /// builder.Services.AddHexaBale("YOUR_BOT_TOKEN");
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException">Thrown when token is null or empty.</exception>
    public static HexaBaleBuilder AddHexaBale(this IServiceCollection services, string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token), "Bot token is required");

        services.AddSingleton(x => new HexaBaleClient(token));
        services.AddHttpClient();
        return new HexaBaleBuilder(services, token);
    }

    /// <summary>
    /// Adds HexaBaleClient to the service collection with custom configuration options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the client to.</param>
    /// <param name="token">The bot token obtained from @BotFather on Bale.</param>
    /// <param name="configureOptions">Action to configure HexaBaleOptions.</param>
    /// <returns>A HexaBaleBuilder for further configuration.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddHexaBale("YOUR_BOT_TOKEN", options =>
    /// {
    ///     options.TimeoutSeconds = 60;
    ///     options.MaxRetries = 5;
    /// });
    /// </code>
    /// </example>
    public static HexaBaleBuilder AddHexaBale(
        this IServiceCollection services,
        string token,
        Action<HexaBaleOptions> configureOptions)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token), "Bot token is required");

        var options = new HexaBaleOptions();
        configureOptions(options);
        services.AddSingleton(x => new HexaBaleClient(token, options));
        services.AddHttpClient();
        return new HexaBaleBuilder(services, token);
    }

    /// <summary>
    /// Adds HexaBaleClient to the service collection using a token factory.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the client to.</param>
    /// <param name="tokenFactory">Factory function that provides the bot token.</param>
    /// <returns>A HexaBaleBuilder for further configuration.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddHexaBale(sp => configuration["BotToken"]);
    /// </code>
    /// </example>
    public static HexaBaleBuilder AddHexaBale(
        this IServiceCollection services,
        Func<IServiceProvider, string> tokenFactory)
    {
        if (tokenFactory is null)
            throw new ArgumentNullException(nameof(tokenFactory));

        services.AddSingleton(x => new HexaBaleClient(tokenFactory(x)));
        services.AddHttpClient();
        return new HexaBaleBuilder(services, tokenFactory(null!));
    }

    /// <summary>
    /// Adds HexaBaleClient to the service collection using a token factory with custom options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the client to.</param>
    /// <param name="tokenFactory">Factory function that provides the bot token.</param>
    /// <param name="configureOptions">Action to configure HexaBaleOptions.</param>
    /// <returns>A HexaBaleBuilder for further configuration.</returns>
    public static HexaBaleBuilder AddHexaBale(
        this IServiceCollection services,
        Func<IServiceProvider, string> tokenFactory,
        Action<HexaBaleOptions> configureOptions)
    {
        if (tokenFactory is null)
            throw new ArgumentNullException(nameof(tokenFactory));

        var options = new HexaBaleOptions();
        configureOptions(options);
        services.AddSingleton(x => new HexaBaleClient(tokenFactory(x), options));
        services.AddHttpClient();
        return new HexaBaleBuilder(services, tokenFactory(null!));
    }
}