using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Forces the user to reply to the bot's message by showing a reply interface.
/// </summary>
/// <remarks>
/// <para>
/// When a message with force_reply is sent, the user's keyboard is temporarily replaced
/// with a reply input field that targets the bot's message. The user must reply to proceed.
/// </para>
/// <para>
/// This is useful for multi-step interactions where you need user input for each step.
/// </para>
/// </remarks>
public class BleForceReply
{
    /// <summary>
    /// Must be true to force a reply to the message.
    /// </summary>
    [JsonPropertyName("force_reply")]
    public bool ForceReply { get; set; } = true;

    /// <summary>
    /// Placeholder text to be displayed in the reply input field while waiting for user input.
    /// Maximum 64 characters.
    /// </summary>
    [JsonPropertyName("input_field_placeholder")]
    public string? InputFieldPlaceholder { get; set; }

    /// <summary>
    /// If true, the force reply will only apply to users mentioned in the message.
    /// Use this when the message is a reply to a specific user or contains mentions.
    /// </summary>
    [JsonPropertyName("selective")]
    public bool? Selective { get; set; }

    /// <summary>
    /// Creates a new force reply instance.
    /// </summary>
    public BleForceReply()
    {
    }

    /// <summary>
    /// Creates a new force reply instance with a placeholder text.
    /// </summary>
    /// <param name="placeholder">Text to show in the input field while waiting for reply.</param>
    /// <param name="selective">If true, only applies to mentioned users.</param>
    public BleForceReply(string placeholder, bool selective = false)
    {
        ForceReply = true;
        InputFieldPlaceholder = placeholder;
        Selective = selective;
    }

    /// <summary>
    /// Creates a simple force reply without placeholder.
    /// </summary>
    /// <returns>A new <see cref="BleForceReply"/> instance.</returns>
    public static BleForceReply Create()
    {
        return new BleForceReply { ForceReply = true };
    }

    /// <summary>
    /// Creates a force reply with a custom placeholder text.
    /// </summary>
    /// <param name="placeholder">Text to show in the input field.</param>
    /// <returns>A new <see cref="BleForceReply"/> instance.</returns>
    public static BleForceReply CreateWithPlaceholder(string placeholder)
    {
        return new BleForceReply { ForceReply = true, InputFieldPlaceholder = placeholder };
    }

    /// <summary>
    /// Sets the placeholder text for the reply input field.
    /// </summary>
    /// <param name="placeholder">Placeholder text to display.</param>
    /// <returns>The same instance for method chaining.</returns>
    public BleForceReply WithPlaceholder(string placeholder)
    {
        InputFieldPlaceholder = placeholder;
        return this;
    }

    /// <summary>
    /// Makes the force reply selective (only applies to mentioned users).
    /// </summary>
    /// <returns>The same instance for method chaining.</returns>
    public BleForceReply SelectiveOnly()
    {
        Selective = true;
        return this;
    }
}