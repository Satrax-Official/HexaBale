using System.Text.Json;
using HexaBale.Models;
using HexaBale.Enums;

namespace HexaBale.Extensions;

/// <summary>
/// Extension methods for building and manipulating inline keyboards.
/// </summary>
/// <remarks>
/// Provides a fluent interface for creating complex inline keyboards with buttons.
/// </remarks>
public static class BleInlineKeyboardExtensions
{
    /// <summary>
    /// Creates an inline keyboard markup from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string representing the inline keyboard.</param>
    /// <returns>A new <see cref="BleInlineKeyboardMarkup"/> instance populated from the JSON.</returns>
    /// <remarks>
    /// Expected JSON format:
    /// <code>
    /// {
    ///     "inline_keyboard": [
    ///         [
    ///             {"text": "👍 Like", "callback_data": "like"},
    ///             {"text": "👎 Dislike", "callback_data": "dislike"}
    ///         ]
    ///     ]
    /// }
    /// </code>
    /// </remarks>
    /// <example>
    /// <code>
    /// var json = "{\"inline_keyboard\":[[{\"text\":\"Like\",\"callback_data\":\"like\"}]]}";
    /// var keyboard = json.FromJson();
    /// </code>
    /// </example>
    public static BleInlineKeyboardMarkup FromJson(this string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        return JsonSerializer.Deserialize<BleInlineKeyboardMarkup>(json, options)
               ?? new BleInlineKeyboardMarkup();
    }

    /// <summary>
    /// Converts an inline keyboard markup to a JSON string.
    /// </summary>
    /// <param name="keyboard">The inline keyboard markup to convert.</param>
    /// <returns>A JSON string representation of the keyboard.</returns>
    /// <example>
    /// <code>
    /// var keyboard = new BleInlineKeyboardMarkup();
    /// var json = keyboard.ToJson();
    /// </code>
    /// </example>
    public static string ToJson(this BleInlineKeyboardMarkup keyboard)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = false
        };

        return JsonSerializer.Serialize(keyboard, options);
    }

    /// <summary>
    /// Adds a callback button to the current row of the keyboard.
    /// </summary>
    /// <param name="keyboard">The keyboard to add the button to.</param>
    /// <param name="text">The button text.</param>
    /// <param name="callbackData">The callback data to send when clicked.</param>
    /// <returns>The same keyboard instance for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when keyboard is null.</exception>
    /// <example>
    /// <code>
    /// var keyboard = new BleInlineKeyboardMarkup()
    ///     .AddCallbackButton("Click Me", "button_clicked")
    ///     .AddCallbackButton("Cancel", "cancel");
    /// </code>
    /// </example>
    public static BleInlineKeyboardMarkup AddCallbackButton(
        this BleInlineKeyboardMarkup keyboard,
        string text,
        string callbackData)
    {
        if (keyboard is null)
            throw new ArgumentNullException(nameof(keyboard));

        var button = new BleInlineKeyboardButton
        {
            Text = text,
            CallbackData = callbackData
        };

        if (keyboard.InlineKeyboard.Count == 0)
            keyboard.InlineKeyboard.Add(new List<BleInlineKeyboardButton>());

        keyboard.InlineKeyboard.Last().Add(button);
        return keyboard;
    }

    /// <summary>
    /// Adds a URL button to the current row of the keyboard.
    /// </summary>
    /// <param name="keyboard">The keyboard to add the button to.</param>
    /// <param name="text">The button text.</param>
    /// <param name="url">The URL to open when clicked.</param>
    /// <returns>The same keyboard instance for method chaining.</returns>
    public static BleInlineKeyboardMarkup AddUrlButton(
        this BleInlineKeyboardMarkup keyboard,
        string text,
        string url)
    {
        if (keyboard is null)
            throw new ArgumentNullException(nameof(keyboard));

        var button = new BleInlineKeyboardButton
        {
            Text = text,
            Url = url
        };

        if (keyboard.InlineKeyboard.Count == 0)
            keyboard.InlineKeyboard.Add(new List<BleInlineKeyboardButton>());

        keyboard.InlineKeyboard.Last().Add(button);
        return keyboard;
    }

    /// <summary>
    /// Adds a Web App button to the current row of the keyboard.
    /// </summary>
    /// <param name="keyboard">The keyboard to add the button to.</param>
    /// <param name="text">The button text.</param>
    /// <param name="webAppUrl">The Web App URL to launch.</param>
    /// <returns>The same keyboard instance for method chaining.</returns>
    public static BleInlineKeyboardMarkup AddWebAppButton(
        this BleInlineKeyboardMarkup keyboard,
        string text,
        string webAppUrl)
    {
        if (keyboard is null)
            throw new ArgumentNullException(nameof(keyboard));

        var button = new BleInlineKeyboardButton
        {
            Text = text,
            WebApp = new BleWebAppInfo { Url = webAppUrl }
        };

        if (keyboard.InlineKeyboard.Count == 0)
            keyboard.InlineKeyboard.Add(new List<BleInlineKeyboardButton>());

        keyboard.InlineKeyboard.Last().Add(button);
        return keyboard;
    }

    /// <summary>
    /// Adds a Login URL button to the current row of the keyboard.
    /// </summary>
    /// <param name="keyboard">The keyboard to add the button to.</param>
    /// <param name="text">The button text.</param>
    /// <param name="loginUrl">The login URL.</param>
    /// <param name="botUsername">Optional bot username for authentication.</param>
    /// <returns>The same keyboard instance for method chaining.</returns>
    public static BleInlineKeyboardMarkup AddLoginButton(
        this BleInlineKeyboardMarkup keyboard,
        string text,
        string loginUrl,
        string? botUsername = null)
    {
        if (keyboard is null)
            throw new ArgumentNullException(nameof(keyboard));

        var button = new BleInlineKeyboardButton
        {
            Text = text,
            LoginUrl = new BleLoginUrl { Url = loginUrl, BotUsername = botUsername }
        };

        if (keyboard.InlineKeyboard.Count == 0)
            keyboard.InlineKeyboard.Add(new List<BleInlineKeyboardButton>());

        keyboard.InlineKeyboard.Last().Add(button);
        return keyboard;
    }

    /// <summary>
    /// Adds a Copy Text button to the current row of the keyboard.
    /// </summary>
    /// <param name="keyboard">The keyboard to add the button to.</param>
    /// <param name="text">The button text.</param>
    /// <param name="copyText">The text to copy to clipboard.</param>
    /// <returns>The same keyboard instance for method chaining.</returns>
    public static BleInlineKeyboardMarkup AddCopyButton(
        this BleInlineKeyboardMarkup keyboard,
        string text,
        string copyText)
    {
        if (keyboard is null)
            throw new ArgumentNullException(nameof(keyboard));

        var button = new BleInlineKeyboardButton
        {
            Text = text,
            CopyText = new BleCopyTextButton { Text = copyText }
        };

        if (keyboard.InlineKeyboard.Count == 0)
            keyboard.InlineKeyboard.Add(new List<BleInlineKeyboardButton>());

        keyboard.InlineKeyboard.Last().Add(button);
        return keyboard;
    }

    /// <summary>
    /// Starts a new row in the keyboard.
    /// </summary>
    /// <param name="keyboard">The keyboard to add a new row to.</param>
    /// <returns>The same keyboard instance for method chaining.</returns>
    public static BleInlineKeyboardMarkup NewRow(this BleInlineKeyboardMarkup keyboard)
    {
        if (keyboard is null)
            throw new ArgumentNullException(nameof(keyboard));

        keyboard.InlineKeyboard.Add(new List<BleInlineKeyboardButton>());
        return keyboard;
    }

    /// <summary>
    /// Creates a simple inline keyboard with a single callback button.
    /// </summary>
    /// <param name="text">The button text.</param>
    /// <param name="callbackData">The callback data.</param>
    /// <returns>A new inline keyboard markup with one button.</returns>
    public static BleInlineKeyboardMarkup SingleButton(string text, string callbackData)
    {
        var keyboard = new BleInlineKeyboardMarkup();
        keyboard.AddCallbackButton(text, callbackData);
        return keyboard;
    }

    /// <summary>
    /// Creates an inline keyboard with multiple buttons in a single row.
    /// </summary>
    /// <param name="buttons">Array of (text, callbackData) tuples for each button.</param>
    /// <returns>A new inline keyboard markup with buttons in one row.</returns>
    public static BleInlineKeyboardMarkup RowButtons(params (string Text, string CallbackData)[] buttons)
    {
        var keyboard = new BleInlineKeyboardMarkup();
        var row = new List<BleInlineKeyboardButton>();

        foreach (var (text, callbackData) in buttons)
        {
            row.Add(new BleInlineKeyboardButton { Text = text, CallbackData = callbackData });
        }

        keyboard.InlineKeyboard.Add(row);
        return keyboard;
    }

    /// <summary>
    /// Creates an inline keyboard with URL buttons in a single row.
    /// </summary>
    /// <param name="buttons">Array of (text, url) tuples for each button.</param>
    /// <returns>A new inline keyboard markup with URL buttons in one row.</returns>
    public static BleInlineKeyboardMarkup RowUrlButtons(params (string Text, string Url)[] buttons)
    {
        var keyboard = new BleInlineKeyboardMarkup();
        var row = new List<BleInlineKeyboardButton>();

        foreach (var (text, url) in buttons)
        {
            row.Add(new BleInlineKeyboardButton { Text = text, Url = url });
        }

        keyboard.InlineKeyboard.Add(row);
        return keyboard;
    }
}