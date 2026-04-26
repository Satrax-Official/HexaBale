using System.Text.Json.Serialization;

namespace HexaBale.Models;

/// <summary>
/// Represents a custom reply keyboard that replaces the user's default keyboard.
/// Use this to provide predefined button options that the user can tap to respond.
/// </summary>
/// <remarks>
/// <para>
/// Reply keyboards are persistent and remain visible until the user closes them or
/// the bot sends a new keyboard or removes it.
/// </para>
/// <para>
/// When a user taps a button, the button's text is sent as a regular message to the bot.
/// </para>
/// </remarks>
public class BleReplyKeyboardMarkup
{
    /// <summary>
    /// Array of button rows, each containing an array of keyboard buttons.
    /// Maximum: 12 rows, each row up to 8 buttons.
    /// </summary>
    [JsonPropertyName("keyboard")]
    public List<List<BleKeyboardButton>> Keyboard { get; set; } = new();

    /// <summary>
    /// If true, the keyboard will be resized to fit the device's screen.
    /// Default is false (keyboard uses full width).
    /// </summary>
    [JsonPropertyName("resize_keyboard")]
    public bool? ResizeKeyboard { get; set; }

    /// <summary>
    /// If true, the keyboard will be hidden after the first use.
    /// Default is false (keyboard remains visible until manually closed).
    /// </summary>
    [JsonPropertyName("one_time_keyboard")]
    public bool? OneTimeKeyboard { get; set; }

    /// <summary>
    /// Placeholder text to be displayed in the input field when the keyboard is active.
    /// Maximum 64 characters.
    /// </summary>
    [JsonPropertyName("input_field_placeholder")]
    public string? InputFieldPlaceholder { get; set; }

    /// <summary>
    /// If true, the user's client will show a "select" button instead of a send button,
    /// which only sends the message when the user explicitly confirms.
    /// Use this for forms or important actions.
    /// </summary>
    [JsonPropertyName("selective")]
    public bool? Selective { get; set; }

    /// <summary>
    /// If true, removes the current custom keyboard and restores the default keyboard.
    /// When this is true, all other properties are ignored.
    /// </summary>
    [JsonPropertyName("remove_keyboard")]
    public bool RemoveKeyboard { get; set; }

    /// <summary>
    /// Creates a new empty reply keyboard markup.
    /// </summary>
    public BleReplyKeyboardMarkup()
    {
    }

    /// <summary>
    /// Creates a new reply keyboard markup with the specified buttons.
    /// </summary>
    /// <param name="buttons">2D array of button texts or button objects.</param>
    public BleReplyKeyboardMarkup(List<List<BleKeyboardButton>> buttons)
    {
        Keyboard = buttons;
    }

    /// <summary>
    /// Creates a simple keyboard from a single row of button texts.
    /// </summary>
    /// <param name="buttonTexts">Texts for buttons in a single row.</param>
    /// <returns>A new <see cref="BleReplyKeyboardMarkup"/> instance.</returns>
    public static BleReplyKeyboardMarkup CreateSimpleKeyboard(params string[] buttonTexts)
    {
        var keyboard = new BleReplyKeyboardMarkup();
        var row = new List<BleKeyboardButton>();
        foreach (var text in buttonTexts)
        {
            row.Add(new BleKeyboardButton(text));
        }
        keyboard.Keyboard.Add(row);
        return keyboard;
    }

    /// <summary>
    /// Creates a keyboard with multiple rows from button texts.
    /// </summary>
    /// <param name="rows">Array of rows, each containing button texts.</param>
    /// <returns>A new <see cref="BleReplyKeyboardMarkup"/> instance.</returns>
    public static BleReplyKeyboardMarkup CreateKeyboard(params string[][] rows)
    {
        var keyboard = new BleReplyKeyboardMarkup();
        foreach (var row in rows)
        {
            var buttonRow = new List<BleKeyboardButton>();
            foreach (var text in row)
            {
                buttonRow.Add(new BleKeyboardButton(text));
            }
            keyboard.Keyboard.Add(buttonRow);
        }
        return keyboard;
    }

    /// <summary>
    /// Adds a row of buttons to the keyboard.
    /// </summary>
    /// <param name="buttons">Buttons to add as a new row.</param>
    /// <returns>The same instance for method chaining.</returns>
    public BleReplyKeyboardMarkup AddRow(params BleKeyboardButton[] buttons)
    {
        Keyboard.Add(buttons.ToList());
        return this;
    }

    /// <summary>
    /// Adds a row of text buttons to the keyboard.
    /// </summary>
    /// <param name="buttonTexts">Texts for buttons in the new row.</param>
    /// <returns>The same instance for method chaining.</returns>
    public BleReplyKeyboardMarkup AddRow(params string[] buttonTexts)
    {
        var row = buttonTexts.Select(t => new BleKeyboardButton(t)).ToList();
        Keyboard.Add(row);
        return this;
    }

    /// <summary>
    /// Configures the keyboard to be resized to fit the device screen.
    /// </summary>
    /// <returns>The same instance for method chaining.</returns>
    public BleReplyKeyboardMarkup WithResize()
    {
        ResizeKeyboard = true;
        return this;
    }

    /// <summary>
    /// Configures the keyboard to be hidden after first use.
    /// </summary>
    /// <returns>The same instance for method chaining.</returns>
    public BleReplyKeyboardMarkup WithOneTimeUse()
    {
        OneTimeKeyboard = true;
        return this;
    }

    /// <summary>
    /// Sets a placeholder text for the input field.
    /// </summary>
    /// <param name="placeholder">Placeholder text to display.</param>
    /// <returns>The same instance for method chaining.</returns>
    public BleReplyKeyboardMarkup WithPlaceholder(string placeholder)
    {
        InputFieldPlaceholder = placeholder;
        return this;
    }
}

/// <summary>
/// Represents a single button in a custom reply keyboard.
/// </summary>
public class BleKeyboardButton
{
    /// <summary>
    /// Text displayed on the button.
    /// When pressed, this text is sent as a message to the bot.
    /// Maximum 64 characters.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// If true, pressing the button will request the user's phone number.
    /// The bot will receive the contact in a <see cref="BleContact"/> object.
    /// </summary>
    [JsonPropertyName("request_contact")]
    public bool? RequestContact { get; set; }

    /// <summary>
    /// If true, pressing the button will request the user's current location.
    /// The bot will receive the location in a <see cref="BleLocation"/> object.
    /// </summary>
    [JsonPropertyName("request_location")]
    public bool? RequestLocation { get; set; }

    /// <summary>
    /// If specified, pressing the button will open a poll creation interface.
    /// The user can create and send a poll with the specified type.
    /// </summary>
    [JsonPropertyName("request_poll")]
    public BleKeyboardButtonPollType? RequestPoll { get; set; }

    /// <summary>
    /// Creates a new keyboard button with the specified text.
    /// </summary>
    /// <param name="text">Button text (maximum 64 characters).</param>
    public BleKeyboardButton(string text)
    {
        Text = text;
    }

    /// <summary>
    /// Creates a phone number request button.
    /// </summary>
    /// <param name="text">Button text.</param>
    /// <returns>A button configured to request the user's phone number.</returns>
    public static BleKeyboardButton CreateContactRequestButton(string text)
    {
        return new BleKeyboardButton(text)
        {
            RequestContact = true
        };
    }

    /// <summary>
    /// Creates a location request button.
    /// </summary>
    /// <param name="text">Button text.</param>
    /// <returns>A button configured to request the user's location.</returns>
    public static BleKeyboardButton CreateLocationRequestButton(string text)
    {
        return new BleKeyboardButton(text)
        {
            RequestLocation = true
        };
    }

    /// <summary>
    /// Creates a poll request button.
    /// </summary>
    /// <param name="text">Button text.</param>
    /// <param name="pollType">Type of poll to request ("quiz" or "regular").</param>
    /// <returns>A button configured to request a poll creation.</returns>
    public static BleKeyboardButton CreatePollRequestButton(string text, string pollType = "regular")
    {
        return new BleKeyboardButton(text)
        {
            RequestPoll = new BleKeyboardButtonPollType { Type = pollType }
        };
    }
}

/// <summary>
/// Specifies the type of poll that can be requested via a keyboard button.
/// </summary>
public class BleKeyboardButtonPollType
{
    /// <summary>
    /// Type of poll: "quiz" for quiz polls with a correct answer,
    /// "regular" for regular polls without a correct answer.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

/// <summary>
/// Represents a command to remove the current custom reply keyboard.
/// </summary>
public class BleReplyKeyboardRemove
{
    /// <summary>
    /// Must be true to remove the keyboard.
    /// </summary>
    [JsonPropertyName("remove_keyboard")]
    public bool RemoveKeyboard => true;

    /// <summary>
    /// If true, the keyboard removal will only apply to users mentioned in the message.
    /// </summary>
    [JsonPropertyName("selective")]
    public bool? Selective { get; set; }
}