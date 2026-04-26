using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HexaBale.Models;
using HexaBale.Enums;
using HexaBale.Exceptions;

namespace HexaBale.Core;

/// <summary>
/// Main client for Bale Messenger Bot API.
/// Provides methods to interact with the Bale Bot API including sending messages, media, keyboards,
/// managing webhooks, handling updates, and performing chat operations.
/// </summary>
/// <remarks>
/// <para>
/// This client handles all communication with the Bale Bot API endpoints.
/// All methods are asynchronous and support cancellation tokens for proper resource management.
/// </para>
/// <para>
/// The client automatically serializes requests using snake_case naming convention and
/// ignores null properties to optimize payload size.
/// </para>
/// <para>
/// Example usage:
/// <code>
/// var client = new HexaBaleClient("YOUR_BOT_TOKEN");
/// var me = await client.GetMeAsync();
/// var message = await client.SendMessageAsync(123456789, "Hello World!");
/// </code>
/// </para>
/// </remarks>
public class HexaBaleClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly HexaBaleOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="HexaBaleClient"/> class.
    /// </summary>
    /// <param name="token">Bot authentication token obtained from @BotFather on Bale.</param>
    /// <param name="options">Optional configuration settings for the client. If null, default options are used.</param>
    /// <param name="httpClient">Optional custom HTTP client. If null, a new instance is created.</param>
    /// <exception cref="ArgumentException">Thrown when token is null, empty, or contains only whitespace.</exception>
    /// <remarks>
    /// The base URL is constructed as: https://tapi.bale.ai/bot{token}/
    /// Timeout is set from options.TimeoutSeconds (default: 100 seconds).
    /// JSON serialization uses snake_case naming and ignores null properties.
    /// </remarks>
    public HexaBaleClient(string token, HexaBaleOptions? options = null, HttpClient? httpClient = null)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Bot token is required", nameof(token));

        _options = options ?? new HexaBaleOptions();
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
        _baseUrl = $"https://tapi.bale.ai/bot{token}/";

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        };
    }

    #region Core Methods

    /// <summary>
    /// Returns basic information about the bot in form of a <see cref="BleUser"/> object.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleUser"/> object containing bot information, or null if the request fails.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// This is a test method to check the bot's authentication and get basic bot info.
    /// Useful for verifying that the token is correct and the bot is working.
    /// </remarks>
    public async Task<BleUser?> GetMeAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<BleUser>("getMe", cancellationToken);
    }

    #endregion

    #region Send Message Methods (All with Inline Keyboard Support)

    /// <summary>
    /// Sends a text message to the specified chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="text">Text of the message to be sent (1-4096 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the message text. Can be HTML, Markdown, or MarkdownV2.</param>
    /// <param name="replyToMessageId">ID of the original message to reply to.</param>
    /// <param name="disableNotification">If true, sends the message silently without notification for the user.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons below the message.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// The text can include emojis, mentions (using @username), and hashtags.
    /// When using parse mode, you can format text with bold, italic, links, etc.
    /// </remarks>
    public async Task<BleMessage?> SendMessageAsync(
        long chatId,
        string text,
        BleParseMode? parseMode = null,
        int? replyToMessageId = null,
        bool disableNotification = false,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            text,
            parse_mode = parseMode?.ToString(),
            reply_to_message_id = replyToMessageId,
            disable_notification = disableNotification,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendMessage", request, cancellationToken);
    }

    /// <summary>
    /// Sends a photo message to the specified chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="photo">Photo to send. Can be a file_id, URL, or local path (if using multipart).</param>
    /// <param name="caption">Optional caption for the photo (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// The photo must be at most 10 MB in size. Supported formats: JPEG, PNG, GIF (static), BMP, WEBP.
    /// You can use an existing file_id from a previously sent photo to avoid re-uploading.
    /// </remarks>
    public async Task<BleMessage?> SendPhotoAsync(
        long chatId,
        string photo,
        string? caption = null,
        BleParseMode? parseMode = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            photo,
            caption,
            parse_mode = parseMode?.ToString(),
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendPhoto", request, cancellationToken);
    }

    /// <summary>
    /// Sends a document file to the specified chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="document">Document to send. Can be a file_id, URL, or local path.</param>
    /// <param name="caption">Optional caption for the document (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Documents up to 50 MB are supported. Thumbnails may be generated for some file types.
    /// Common document types: PDF, TXT, ZIP, etc.
    /// </remarks>
    public async Task<BleMessage?> SendDocumentAsync(
        long chatId,
        string document,
        string? caption = null,
        BleParseMode? parseMode = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            document,
            caption,
            parse_mode = parseMode?.ToString(),
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendDocument", request, cancellationToken);
    }

    /// <summary>
    /// Sends a video message to the specified chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="video">Video to send. Can be a file_id, URL, or local path.</param>
    /// <param name="caption">Optional caption for the video (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the video in seconds.</param>
    /// <param name="width">Video width in pixels.</param>
    /// <param name="height">Video height in pixels.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Video files up to 50 MB are supported. Supported formats: MP4, AVI, MKV, MOV.
    /// The video will be compressed by Bale if it exceeds certain size limits.
    /// </remarks>
    public async Task<BleMessage?> SendVideoAsync(
        long chatId,
        string video,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        int? width = null,
        int? height = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            video,
            caption,
            parse_mode = parseMode?.ToString(),
            duration,
            width,
            height,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendVideo", request, cancellationToken);
    }

    /// <summary>
    /// Sends an audio file to the specified chat (music or spoken word).
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="audio">Audio file to send. Can be a file_id, URL, or local path.</param>
    /// <param name="caption">Optional caption for the audio (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the audio in seconds.</param>
    /// <param name="performer">Performer/singer name of the audio track.</param>
    /// <param name="title">Title of the audio track.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Audio files up to 50 MB are supported. Supported formats: MP3, M4A, OGG, WAV.
    /// The performer and title will be displayed in the music player interface.
    /// </remarks>
    public async Task<BleMessage?> SendAudioAsync(
        long chatId,
        string audio,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        string? performer = null,
        string? title = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            audio,
            caption,
            parse_mode = parseMode?.ToString(),
            duration,
            performer,
            title,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendAudio", request, cancellationToken);
    }

    /// <summary>
    /// Sends a voice message (recorded audio without music player interface).
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="voice">Voice message to send. Can be a file_id, URL, or local path.</param>
    /// <param name="caption">Optional caption for the voice message (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the voice message in seconds.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Voice messages are displayed differently from audio files (no album art, simple player).
    /// Supported formats: OGG (OPUS) encoded. Maximum size: 50 MB.
    /// </remarks>
    public async Task<BleMessage?> SendVoiceAsync(
        long chatId,
        string voice,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            voice,
            caption,
            parse_mode = parseMode?.ToString(),
            duration,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendVoice", request, cancellationToken);
    }

    /// <summary>
    /// Sends an animation (GIF or H.264/MPEG-4 AVC video without sound).
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="animation">Animation to send. Can be a file_id, URL, or local path.</param>
    /// <param name="caption">Optional caption for the animation (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the animation in seconds.</param>
    /// <param name="width">Animation width in pixels.</param>
    /// <param name="height">Animation height in pixels.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Animations are displayed with a play button and loop automatically.
    /// Maximum size: 50 MB. Supported formats: GIF, MPEG4.
    /// </remarks>
    public async Task<BleMessage?> SendAnimationAsync(
        long chatId,
        string animation,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        int? width = null,
        int? height = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            animation,
            caption,
            parse_mode = parseMode?.ToString(),
            duration,
            width,
            height,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendAnimation", request, cancellationToken);
    }

    /// <summary>
    /// Sends a geographic location to the specified chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="latitude">Latitude of the location (between -90 and 90).</param>
    /// <param name="longitude">Longitude of the location (between -180 and 180).</param>
    /// <param name="horizontalAccuracy">Radius of uncertainty for the location in meters (0-1500).</param>
    /// <param name="livePeriod">Period in seconds for which the location will be updated (live location).</param>
    /// <param name="heading">Direction in which the user is moving (0-360).</param>
    /// <param name="proximityAlertRadius">Distance in meters for proximity alert (1-100000).</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// The location appears as a map preview with a pin at the specified coordinates.
    /// Live locations can be updated by calling this method again with the same live_period.
    /// </remarks>
    public async Task<BleMessage?> SendLocationAsync(
        long chatId,
        float latitude,
        float longitude,
        float? horizontalAccuracy = null,
        int? livePeriod = null,
        int? heading = null,
        int? proximityAlertRadius = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            latitude,
            longitude,
            horizontal_accuracy = horizontalAccuracy,
            live_period = livePeriod,
            heading,
            proximity_alert_radius = proximityAlertRadius,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendLocation", request, cancellationToken);
    }

    /// <summary>
    /// Sends information about a venue (physical location with a name and address).
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="latitude">Latitude of the venue.</param>
    /// <param name="longitude">Longitude of the venue.</param>
    /// <param name="title">Name of the venue (e.g., "Starbucks").</param>
    /// <param name="address">Address of the venue (e.g., "123 Main Street").</param>
    /// <param name="foursquareId">Foursquare identifier of the venue.</param>
    /// <param name="foursquareType">Foursquare type of the venue (e.g., "coffee_shop").</param>
    /// <param name="googlePlaceId">Google Places identifier of the venue.</param>
    /// <param name="googlePlaceType">Google Places type of the venue.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Venues appear as location pins with the title and address displayed prominently.
    /// Foursquare and Google Place IDs help provide additional venue information.
    /// </remarks>
    public async Task<BleMessage?> SendVenueAsync(
        long chatId,
        float latitude,
        float longitude,
        string title,
        string address,
        string? foursquareId = null,
        string? foursquareType = null,
        string? googlePlaceId = null,
        string? googlePlaceType = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            latitude,
            longitude,
            title,
            address,
            foursquare_id = foursquareId,
            foursquare_type = foursquareType,
            google_place_id = googlePlaceId,
            google_place_type = googlePlaceType,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendVenue", request, cancellationToken);
    }

    /// <summary>
    /// Sends a phone contact to the specified chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="phoneNumber">Contact's phone number (with international format recommended).</param>
    /// <param name="firstName">Contact's first name.</param>
    /// <param name="lastName">Optional contact's last name.</param>
    /// <param name="vcard">Optional vCard representation of the contact (4.0 format).</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// The contact appears as a shareable contact card that users can save to their address book.
    /// If vCard is provided, it overrides the phone_number, first_name, and last_name fields.
    /// </remarks>
    public async Task<BleMessage?> SendContactAsync(
        long chatId,
        string phoneNumber,
        string firstName,
        string? lastName = null,
        string? vcard = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            phone_number = phoneNumber,
            first_name = firstName,
            last_name = lastName,
            vcard,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendContact", request, cancellationToken);
    }

    /// <summary>
    /// Sends a native poll to the specified chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="question">Poll question (1-300 characters).</param>
    /// <param name="options">List of answer options (2-10 options, each 1-100 characters).</param>
    /// <param name="isAnonymous">If true, votes are anonymous (default: true).</param>
    /// <param name="type">Poll type: "quiz" or "regular" (default: "regular").</param>
    /// <param name="allowsMultipleAnswers">If true, users can select multiple answers (regular polls only).</param>
    /// <param name="correctOptionId">0-based identifier of the correct answer (quiz polls only).</param>
    /// <param name="explanation">Text shown when a user selects an incorrect answer (quiz polls only).</param>
    /// <param name="explanationParseMode">Parse mode for the explanation text.</param>
    /// <param name="openPeriod">Amount of time in seconds the poll will be active (5-600).</param>
    /// <param name="closeDate">Point in time (Unix timestamp) when the poll will be automatically closed.</param>
    /// <param name="isClosed">If true, the poll is closed immediately.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// Regular polls allow multiple answers and show vote percentages. Quiz polls have one correct answer
    /// and can show explanations when users answer incorrectly.
    /// </para>
    /// <para>
    /// Polls can be configured to close automatically after open_period seconds or at a specific close_date.
    /// </para>
    /// </remarks>
    public async Task<BleMessage?> SendPollAsync(
        long chatId,
        string question,
        List<string> options,
        bool? isAnonymous = null,
        string? type = null,
        bool? allowsMultipleAnswers = null,
        int? correctOptionId = null,
        string? explanation = null,
        string? explanationParseMode = null,
        int? openPeriod = null,
        int? closeDate = null,
        bool? isClosed = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            question,
            options = options.Select(o => new { text = o }),
            is_anonymous = isAnonymous,
            type,
            allows_multiple_answers = allowsMultipleAnswers,
            correct_option_id = correctOptionId,
            explanation,
            explanation_parse_mode = explanationParseMode,
            open_period = openPeriod,
            close_date = closeDate,
            is_closed = isClosed,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendPoll", request, cancellationToken);
    }

    /// <summary>
    /// Sends a random dice result with an animated emoji.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="emoji">Emoji for the dice: "🎲", "🎯", "🏀", "⚽", "🎳", or "🎰".</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// Different emojis produce different animated results:
    /// - 🎲: Random value 1-6
    /// - 🎯: Random value 1-6 (bullseye is 6)
    /// - 🏀/⚽: Random value 1-5 (basket goes in at 5)
    /// - 🎳: Random value 1-6 (strike is 6)
    /// - 🎰: Random values for slots
    /// </para>
    /// </remarks>
    public async Task<BleMessage?> SendDiceAsync(
        long chatId,
        string? emoji = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            emoji,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendDice", request, cancellationToken);
    }

    /// <summary>
    /// Sends a media group (album) with an optional inline keyboard message.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="media">List of media items to send as an album (2-10 items).</param>
    /// <param name="replyMarkup">Optional inline keyboard to send as a separate message after the album.</param>
    /// <param name="keyboardMessage">Text for the keyboard message (default: "📌 Options:").</param>
    /// <param name="disableNotification">If true, sends the album silently.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item><description>MediaMessages: List of sent media messages or null if failed</description></item>
    /// <item><description>KeyboardMessage: Sent keyboard message or null if no keyboard was provided</description></item>
    /// </list>
    /// </returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// This method sends a media group (album) first, then waits 500ms and sends a separate message
    /// with the inline keyboard. This is useful when you want to attach a keyboard to an album since
    /// albums don't natively support inline keyboards.
    /// </remarks>
    public async Task<(List<BleMessage>? MediaMessages, BleMessage? KeyboardMessage)> SendMediaGroupWithKeyboardAsync(
        long chatId,
        List<BleInputMedia> media,
        BleInlineKeyboardMarkup? replyMarkup = null,
        string? keyboardMessage = "📌 Options:",
        bool disableNotification = false,
        CancellationToken cancellationToken = default)
    {
        var mediaMessages = await SendMediaGroupAsync(chatId, media, disableNotification, null, cancellationToken);

        BleMessage? keyboardMsg = null;
        if (replyMarkup != null && mediaMessages != null && mediaMessages.Any())
        {
            await Task.Delay(500, cancellationToken);
            keyboardMsg = await SendMessageAsync(chatId, keyboardMessage ?? "📌 Options:", null, null, false, replyMarkup, cancellationToken);
        }

        return (mediaMessages, keyboardMsg);
    }

    /// <summary>
    /// Sends a media group (album) with multiple photos, videos, or documents.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="media">List of media items to send as an album (2-10 items).</param>
    /// <param name="disableNotification">If true, sends the message silently.</param>
    /// <param name="replyMarkup">Inline keyboard markup (note: albums don't support inline keyboards on Bale).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>List of <see cref="BleMessage"/> objects for each media item, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// Media groups (albums) allow sending up to 10 photos or videos at once.
    /// All media must be of the same type (all photos or all videos).
    /// </para>
    /// <para>
    /// Note: Inline keyboards are not supported directly on media groups in Bale API.
    /// Use <see cref="SendMediaGroupWithKeyboardAsync"/> for keyboard support.
    /// </para>
    /// </remarks>
    public async Task<List<BleMessage>?> SendMediaGroupAsync(
        long chatId,
        List<BleInputMedia> media,
        bool disableNotification = false,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            chat_id = chatId,
            media = media.Select(m => new
            {
                type = m.Type,
                media = m.Media,
                caption = m.Caption,
                parse_mode = m.ParseMode?.ToString()
            }),
            disable_notification = disableNotification,
            reply_markup = replyMarkup
        };

        return await PostAsync<List<BleMessage>>("sendMediaGroup", payload, cancellationToken);
    }

    #endregion

    #region Reply Keyboard Methods (Custom Keyboard that replaces user's keyboard)

    /// <summary>
    /// Sends a message with a custom reply keyboard that replaces the user's default keyboard.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="text">Text of the message to send.</param>
    /// <param name="replyMarkup">Custom reply keyboard markup with buttons to display.</param>
    /// <param name="parseMode">Mode for parsing entities in the message text.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// Reply keyboards replace the user's default keyboard with custom buttons.
    /// When a button is pressed, the button text is sent as a message to the bot.
    /// </para>
    /// <para>
    /// Unlike inline keyboards, reply keyboards are persistent until removed with
    /// <see cref="RemoveReplyKeyboardAsync"/> or the user closes them manually.
    /// </para>
    /// </remarks>
    public async Task<BleMessage?> SendReplyKeyboardAsync(
        long chatId,
        string text,
        BleReplyKeyboardMarkup replyMarkup,
        BleParseMode? parseMode = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            text,
            parse_mode = parseMode?.ToString(),
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendMessage", request, cancellationToken);
    }

    /// <summary>
    /// Removes the custom reply keyboard and restores the user's default keyboard.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="text">Text to display when removing the keyboard (default: "Keyboard removed").</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Sends a message with the remove_keyboard flag set to true.
    /// After this method is called, the user will see their normal keyboard again.
    /// </remarks>
    public async Task<BleMessage?> RemoveReplyKeyboardAsync(
        long chatId,
        string text = "Keyboard removed",
        CancellationToken cancellationToken = default)
    {
        var replyMarkup = new BleReplyKeyboardMarkup
        {
            RemoveKeyboard = true
        };

        var request = new
        {
            chat_id = chatId,
            text,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendMessage", request, cancellationToken);
    }

    /// <summary>
    /// Forces the user to reply to this message by showing a reply interface.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="text">Text of the message to send.</param>
    /// <param name="forceReply">If true, forces the user to reply (default: true).</param>
    /// <param name="inputFieldPlaceholder">Optional placeholder text in the reply input field.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// When force_reply is used, the user's keyboard is replaced with a reply interface
    /// that targets the bot's message. The user must reply to proceed.
    /// </remarks>
    public async Task<BleMessage?> ForceReplyAsync(
        long chatId,
        string text,
        bool forceReply = true,
        string? inputFieldPlaceholder = null,
        CancellationToken cancellationToken = default)
    {
        var replyMarkup = new BleForceReply
        {
            ForceReply = forceReply,
            InputFieldPlaceholder = inputFieldPlaceholder
        };

        var request = new
        {
            chat_id = chatId,
            text,
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("sendMessage", request, cancellationToken);
    }

    #endregion

    #region Message with Inline Keyboard (Legacy/Convenience)

    /// <summary>
    /// Convenience method for sending a message with an inline keyboard.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="text">Text of the message to send.</param>
    /// <param name="replyMarkup">Inline keyboard markup for interactive buttons.</param>
    /// <param name="parseMode">Mode for parsing entities in the message text.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleMessage"/> object containing information about the sent message, or null if failed.</returns>
    /// <remarks>
    /// This is a convenience wrapper around <see cref="SendMessageAsync"/>.
    /// For new code, prefer using <see cref="SendMessageAsync"/> directly.
    /// </remarks>
    public async Task<BleMessage?> SendMessageWithKeyboardAsync(
        long chatId,
        string text,
        BleInlineKeyboardMarkup replyMarkup,
        BleParseMode? parseMode = null,
        CancellationToken cancellationToken = default)
    {
        return await SendMessageAsync(chatId, text, parseMode, null, false, replyMarkup, cancellationToken);
    }

    #endregion

    #region Updates & Webhook

    /// <summary>
    /// Retrieves new updates (messages, callback queries, etc.) from the bot using long polling.
    /// </summary>
    /// <param name="offset">Identifier of the first update to be returned (exclusive).</param>
    /// <param name="limit">Limits the number of updates to be retrieved (1-100, default: 100).</param>
    /// <param name="timeout">Timeout in seconds for long polling (1-30, default: 30).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>List of <see cref="BleUpdate"/> objects containing new updates, or empty list if none.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// Use this method when you don't have a webhook configured (polling method).
    /// The method will wait up to 'timeout' seconds for new updates before returning.
    /// </para>
    /// <para>
    /// To avoid receiving the same update multiple times, set 'offset' to the highest
    /// update_id you've already processed + 1.
    /// </para>
    /// </remarks>
    public async Task<List<BleUpdate>> GetUpdatesAsync(
        int offset = 0,
        int limit = 100,
        int timeout = 30,
        CancellationToken cancellationToken = default)
    {
        var request = new { offset, limit, timeout };
        var result = await PostAsync<BleUpdate[]>("getUpdates", request, cancellationToken);
        return result?.ToList() ?? new List<BleUpdate>();
    }

    /// <summary>
    /// Deletes the current webhook configuration.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>True if the webhook was successfully deleted, false otherwise.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// After deleting the webhook, you can start using <see cref="GetUpdatesAsync"/> for polling.
    /// Returns true even if no webhook was set.
    /// </remarks>
    public async Task<bool> DeleteWebhookAsync(CancellationToken cancellationToken = default)
    {
        return await PostAsync<bool>("deleteWebhook", null, cancellationToken);
    }

    /// <summary>
    /// Sets a webhook URL to receive incoming updates via HTTP POST requests.
    /// </summary>
    /// <param name="url">HTTPS URL to send updates to (must be valid and reachable).</param>
    /// <param name="maxConnections">Maximum number of simultaneous connections (1-100, default: 40).</param>
    /// <param name="allowedUpdates">List of update types to receive (null = receive all types).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>True if the webhook was successfully set, false otherwise.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// The webhook URL must use HTTPS and can contain a path (e.g., https://example.com/bot).
    /// After setting a webhook, polling with <see cref="GetUpdatesAsync"/> will not work.
    /// </para>
    /// <para>
    /// Allowed updates examples: "message", "callback_query", "edited_message", etc.
    /// Setting allowed_updates = null means your bot will receive all update types.
    /// </para>
    /// </remarks>
    public async Task<bool> SetWebhookAsync(
        string url,
        int? maxConnections = null,
        List<string>? allowedUpdates = null,
        CancellationToken cancellationToken = default)
    {
        var request = new { url, max_connections = maxConnections, allowed_updates = allowedUpdates };
        return await PostAsync<bool>("setWebhook", request, cancellationToken);
    }

    /// <summary>
    /// Gets information about the current webhook configuration.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleWebhookInfo"/> object containing webhook details, or null if not configured.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Returns information including the webhook URL, pending update count, last error date and message,
    /// and custom certificate status if one was provided.
    /// </remarks>
    public async Task<BleWebhookInfo?> GetWebhookInfoAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<BleWebhookInfo>("getWebhookInfo", cancellationToken);
    }

    #endregion

    #region Chat & User Methods

    /// <summary>
    /// Gets information about a member of a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="userId">Unique identifier of the target user.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A <see cref="BleChatMember"/> object containing member information, or null if not found.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Returns information about the user's status in the chat (creator, administrator, member, restricted, left, or kicked).
    /// Also returns custom title for administrators and restrictions for restricted users.
    /// </remarks>
    public async Task<BleChatMember?> GetChatMemberAsync(
        long chatId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        var request = new { chat_id = chatId, user_id = userId };
        return await PostAsync<BleChatMember>("getChatMember", request, cancellationToken);
    }

    #endregion

    #region Callback Query

    /// <summary>
    /// Sends an answer to a callback query sent from an inline keyboard button.
    /// </summary>
    /// <param name="callbackQueryId">Unique identifier of the callback query to answer.</param>
    /// <param name="text">Text of the notification (0-200 characters).</param>
    /// <param name="showAlert">If true, shows the notification as an alert instead of a toast.</param>
    /// <param name="url">URL to be opened by the user's client.</param>
    /// <param name="cacheTime">Maximum time in seconds that the result can be cached (0-86400).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>True if the answer was successfully sent, false otherwise.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// After the user presses an inline keyboard button, your bot receives a callback_query.
    /// You must answer it within a few seconds to prevent the client from showing a timeout.
    /// </para>
    /// <para>
    /// If showAlert is false (default), the notification appears as a toast message at the top of the screen.
    /// If showAlert is true, it appears as an alert dialog that requires user dismissal.
    /// </para>
    /// </remarks>
    public async Task<bool> AnswerCallbackQueryAsync(
        string callbackQueryId,
        string? text = null,
        bool showAlert = false,
        string? url = null,
        int? cacheTime = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            callback_query_id = callbackQueryId,
            text,
            show_alert = showAlert,
            url,
            cache_time = cacheTime
        };
        return await PostAsync<bool>("answerCallbackQuery", request, cancellationToken);
    }

    #endregion

    #region Edit & Delete Methods

    /// <summary>
    /// Edits the text of a message previously sent by the bot.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="messageId">Identifier of the message to edit.</param>
    /// <param name="text">New text for the message (1-4096 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the new text.</param>
    /// <param name="replyMarkup">New inline keyboard markup (or null to keep current).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>The edited <see cref="BleMessage"/> object, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// This method can edit any message sent by the bot, including those with inline keyboards.
    /// The message text can be edited multiple times.
    /// </para>
    /// <para>
    /// If parse_mode is not provided, the current parse mode of the message is preserved.
    /// </para>
    /// </remarks>
    public async Task<BleMessage?> EditMessageTextAsync(
        long chatId,
        int messageId,
        string text,
        BleParseMode? parseMode = null,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            message_id = messageId,
            text,
            parse_mode = parseMode?.ToString(),
            reply_markup = replyMarkup
        };
        return await PostAsync<BleMessage>("editMessageText", request, cancellationToken);
    }

    /// <summary>
    /// Edits only the inline keyboard of a message (preserves the text).
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="messageId">Identifier of the message to edit.</param>
    /// <param name="replyMarkup">New inline keyboard markup (or null to remove keyboard).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>True if the keyboard was successfully edited, false otherwise.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Use this method when you only want to change the keyboard buttons without modifying the message text.
    /// This is useful for pagination, loading states, or updating button states after user interaction.
    /// </remarks>
    public async Task<bool> EditMessageReplyMarkupAsync(
        long chatId,
        int messageId,
        BleInlineKeyboardMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            message_id = messageId,
            reply_markup = replyMarkup
        };
        return await PostAsync<bool>("editMessageReplyMarkup", request, cancellationToken);
    }

    /// <summary>
    /// Deletes a message sent by the bot or a message in a chat where the bot is an administrator.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="messageId">Identifier of the message to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>True if the message was successfully deleted, false otherwise.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// The bot can delete its own messages in any chat without being an administrator.
    /// To delete other users' messages, the bot must be an administrator in the chat.
    /// </para>
    /// <para>
    /// In groups and supergroups, messages can only be deleted if the bot was sent 
    /// less than 48 hours ago.
    /// </para>
    /// </remarks>
    public async Task<bool> DeleteMessageAsync(
        long chatId,
        int messageId,
        CancellationToken cancellationToken = default)
    {
        var request = new { chat_id = chatId, message_id = messageId };
        return await PostAsync<bool>("deleteMessage", request, cancellationToken);
    }

    #endregion

    #region Forward & Copy Methods

    /// <summary>
    /// Forwards a message from one chat to another.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="fromChatId">Unique identifier for the source chat.</param>
    /// <param name="messageId">Identifier of the message to forward.</param>
    /// <param name="disableNotification">If true, sends the forwarded message silently.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>The forwarded <see cref="BleMessage"/> object, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// Forwarded messages show the original sender's name and a "forwarded from" label.
    /// The bot must be a member of both the source and target chats.
    /// </remarks>
    public async Task<BleMessage?> ForwardMessageAsync(
        long chatId,
        long fromChatId,
        int messageId,
        bool disableNotification = false,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            from_chat_id = fromChatId,
            message_id = messageId,
            disable_notification = disableNotification
        };
        return await PostAsync<BleMessage>("forwardMessage", request, cancellationToken);
    }

    /// <summary>
    /// Copies a message from one chat to another without showing the forward origin.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="fromChatId">Unique identifier for the source chat.</param>
    /// <param name="messageId">Identifier of the message to copy.</param>
    /// <param name="caption">New caption for the copied message (overrides original).</param>
    /// <param name="parseMode">Parse mode for the new caption.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>The copied <see cref="BleMessage"/> object, or null if failed.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    /// <remarks>
    /// <para>
    /// Unlike forwarding, copying creates a new message without any indication it came from another chat.
    /// The bot must be a member of both the source and target chats.
    /// </para>
    /// <para>
    /// If caption is provided, it overrides the original message's caption.
    /// </para>
    /// </remarks>
    public async Task<BleMessage?> CopyMessageAsync(
        long chatId,
        long fromChatId,
        int messageId,
        string? caption = null,
        BleParseMode? parseMode = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            from_chat_id = fromChatId,
            message_id = messageId,
            caption,
            parse_mode = parseMode?.ToString()
        };
        return await PostAsync<BleMessage>("copyMessage", request, cancellationToken);
    }

    #endregion

    #region Private Helpers

    /// <summary>
    /// Performs a GET request to the Bale API.
    /// </summary>
    /// <typeparam name="T">The expected return type.</typeparam>
    /// <param name="method">API method name (e.g., "getMe").</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>The deserialized result of type T, or default if unsuccessful.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    private async Task<T?> GetAsync<T>(string method, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}{method}", cancellationToken);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new BleApiException(response.StatusCode, json);

        var apiResponse = JsonSerializer.Deserialize<BleApiResponse<T>>(json, _jsonOptions);
        return apiResponse?.Ok == true ? apiResponse.Result : default;
    }

    /// <summary>
    /// Performs a POST request to the Bale API with JSON payload.
    /// </summary>
    /// <typeparam name="T">The expected return type.</typeparam>
    /// <param name="method">API method name (e.g., "sendMessage").</param>
    /// <param name="data">Data to serialize as JSON request body.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>The deserialized result of type T, or default if unsuccessful.</returns>
    /// <exception cref="BleApiException">Thrown when the API returns an error response.</exception>
    private async Task<T?> PostAsync<T>(string method, object? data, CancellationToken cancellationToken = default)
    {
        HttpContent? content = null;
        if (data != null)
        {
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.PostAsync($"{_baseUrl}{method}", content, cancellationToken);
        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new BleApiException(response.StatusCode, responseJson);

        var apiResponse = JsonSerializer.Deserialize<BleApiResponse<T>>(responseJson, _jsonOptions);
        return apiResponse?.Ok == true ? apiResponse.Result : default;
    }

    #endregion
}