using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HexaBale.Models;
using HexaBale.Enums;
using HexaBale.Exceptions;

namespace HexaBale.Core;

/// <summary>
/// Main client for Bale Messenger Bot API.
/// Provides methods to interact with the Bale Bot API.
/// </summary>
/// <remarks>
/// Create a new instance using your bot token from @BotFather on Bale.
/// 
/// Example:
/// <code>
/// var client = new HexaBaleClient("YOUR_BOT_TOKEN");
/// var me = await client.GetMeAsync();
/// Console.WriteLine($"Bot name: {me.FirstName}");
/// </code>
/// </remarks>
public class HexaBaleClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly HexaBaleOptions _options;

    /// <summary>
    /// Initializes a new instance of the HexaBaleClient class.
    /// </summary>
    /// <param name="token">Bot token obtained from @BotFather on Bale.</param>
    /// <param name="options">Optional configuration options for the client.</param>
    /// <param name="httpClient">Optional custom HttpClient instance.</param>
    /// <exception cref="ArgumentException">Thrown when token is null or empty.</exception>
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
    /// Returns basic information about the bot.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>User object containing bot information.</returns>
    /// <example>
    /// <code>
    /// var botInfo = await client.GetMeAsync();
    /// Console.WriteLine($"Bot name: {botInfo.FirstName}");
    /// </code>
    /// </example>
    public async Task<BleUser?> GetMeAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<BleUser>("getMe", cancellationToken);
    }

    /// <summary>
    /// Sends a text message to a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="text">Text of the message to be sent.</param>
    /// <param name="parseMode">Mode for parsing entities in the message text.</param>
    /// <param name="replyToMessageId">Identifier of the message to reply to.</param>
    /// <param name="disableNotification">Sends the message silently.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendMessageAsync(
        long chatId,
        string text,
        BleParseMode? parseMode = null,
        int? replyToMessageId = null,
        bool disableNotification = false,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            text,
            parse_mode = parseMode?.ToString(),
            reply_to_message_id = replyToMessageId,
            disable_notification = disableNotification
        };
        return await PostAsync<BleMessage>("sendMessage", request, cancellationToken);
    }

    /// <summary>
    /// Sends a photo to a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="photo">File ID or URL of the photo.</param>
    /// <param name="caption">Photo caption (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendPhotoAsync(
        long chatId,
        string photo,
        string? caption = null,
        BleParseMode? parseMode = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            photo,
            caption,
            parse_mode = parseMode?.ToString()
        };
        return await PostAsync<BleMessage>("sendPhoto", request, cancellationToken);
    }

    /// <summary>
    /// Sends a document to a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="document">File ID or URL of the document.</param>
    /// <param name="caption">Document caption (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendDocumentAsync(
        long chatId,
        string document,
        string? caption = null,
        BleParseMode? parseMode = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            document,
            caption,
            parse_mode = parseMode?.ToString()
        };
        return await PostAsync<BleMessage>("sendDocument", request, cancellationToken);
    }

    /// <summary>
    /// Sends a video to a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="video">File ID or URL of the video.</param>
    /// <param name="caption">Video caption (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the video in seconds.</param>
    /// <param name="width">Video width.</param>
    /// <param name="height">Video height.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendVideoAsync(
        long chatId,
        string video,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        int? width = null,
        int? height = null,
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
            height
        };
        return await PostAsync<BleMessage>("sendVideo", request, cancellationToken);
    }

    /// <summary>
    /// Sends an audio file to a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="audio">File ID or URL of the audio.</param>
    /// <param name="caption">Audio caption (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the audio in seconds.</param>
    /// <param name="performer">Performer of the audio.</param>
    /// <param name="title">Title of the audio.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendAudioAsync(
        long chatId,
        string audio,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        string? performer = null,
        string? title = null,
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
            title
        };
        return await PostAsync<BleMessage>("sendAudio", request, cancellationToken);
    }

    /// <summary>
    /// Sends a voice message to a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="voice">File ID or URL of the voice message.</param>
    /// <param name="caption">Voice message caption (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the voice message in seconds.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendVoiceAsync(
        long chatId,
        string voice,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            voice,
            caption,
            parse_mode = parseMode?.ToString(),
            duration
        };
        return await PostAsync<BleMessage>("sendVoice", request, cancellationToken);
    }

    /// <summary>
    /// Sends an animation (GIF) to a chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="animation">File ID or URL of the animation.</param>
    /// <param name="caption">Animation caption (0-1024 characters).</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="duration">Duration of the animation in seconds.</param>
    /// <param name="width">Animation width.</param>
    /// <param name="height">Animation height.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendAnimationAsync(
        long chatId,
        string animation,
        string? caption = null,
        BleParseMode? parseMode = null,
        int? duration = null,
        int? width = null,
        int? height = null,
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
            height
        };
        return await PostAsync<BleMessage>("sendAnimation", request, cancellationToken);
    }

    /// <summary>
    /// Sends a message with an inline keyboard.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="text">Text of the message.</param>
    /// <param name="replyMarkup">Inline keyboard markup.</param>
    /// <param name="parseMode">Mode for parsing entities in the message text.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The sent message object.</returns>
    public async Task<BleMessage?> SendMessageWithKeyboardAsync(
        long chatId,
        string text,
        BleInlineKeyboardMarkup replyMarkup,
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
    /// Gets updates using long polling.
    /// </summary>
    /// <param name="offset">Identifier of the first update to be returned.</param>
    /// <param name="limit">Limits the number of updates to be retrieved.</param>
    /// <param name="timeout">Timeout in seconds for long polling.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>List of updates.</returns>
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
    /// Deletes the webhook.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>True if successful, otherwise false.</returns>
    public async Task<bool> DeleteWebhookAsync(CancellationToken cancellationToken = default)
    {
        return await PostAsync<bool>("deleteWebhook", null, cancellationToken);
    }

    /// <summary>
    /// Sets a webhook URL.
    /// </summary>
    /// <param name="url">HTTPS URL to send updates to.</param>
    /// <param name="maxConnections">Maximum number of simultaneous connections.</param>
    /// <param name="allowedUpdates">List of update types to receive.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>True if successful, otherwise false.</returns>
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
    /// Gets current webhook information.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>Webhook information.</returns>
    public async Task<BleWebhookInfo?> GetWebhookInfoAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<BleWebhookInfo>("getWebhookInfo", cancellationToken);
    }

    /// <summary>
    /// Gets information about a chat member.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="userId">Unique identifier of the target user.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>Chat member information.</returns>
    public async Task<BleChatMember?> GetChatMemberAsync(
        long chatId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        var request = new { chat_id = chatId, user_id = userId };
        return await PostAsync<BleChatMember>("getChatMember", request, cancellationToken);
    }

    /// <summary>
    /// Answers a callback query (button click).
    /// </summary>
    /// <param name="callbackQueryId">Unique identifier for the query to answer.</param>
    /// <param name="text">Text of the notification (0-200 characters).</param>
    /// <param name="showAlert">If true, an alert will be shown instead of a notification.</param>
    /// <param name="url">URL to be opened by the user's client.</param>
    /// <param name="cacheTime">Maximum time in seconds that the result can be cached.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>True if successful, otherwise false.</returns>
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

    /// <summary>
    /// Edits the text of a message.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="messageId">Identifier of the message to edit.</param>
    /// <param name="text">New text of the message.</param>
    /// <param name="parseMode">Mode for parsing entities in the message text.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The edited message object.</returns>
    public async Task<BleMessage?> EditMessageTextAsync(
        long chatId,
        int messageId,
        string text,
        BleParseMode? parseMode = null,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            chat_id = chatId,
            message_id = messageId,
            text,
            parse_mode = parseMode?.ToString()
        };
        return await PostAsync<BleMessage>("editMessageText", request, cancellationToken);
    }

    /// <summary>
    /// Edits the reply markup (buttons) of a message.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="messageId">Identifier of the message to edit.</param>
    /// <param name="replyMarkup">New inline keyboard markup.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>True if successful, otherwise false.</returns>
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
    /// Deletes a message.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="messageId">Identifier of the message to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>True if successful, otherwise false.</returns>
    public async Task<bool> DeleteMessageAsync(
        long chatId,
        int messageId,
        CancellationToken cancellationToken = default)
    {
        var request = new { chat_id = chatId, message_id = messageId };
        return await PostAsync<bool>("deleteMessage", request, cancellationToken);
    }

    /// <summary>
    /// Forwards a message to another chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original message is.</param>
    /// <param name="messageId">Message identifier in the chat specified in fromChatId.</param>
    /// <param name="disableNotification">Sends the message silently.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The forwarded message object.</returns>
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
    /// Copies a message to another chat.
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat.</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original message is.</param>
    /// <param name="messageId">Message identifier in the chat specified in fromChatId.</param>
    /// <param name="caption">New caption for the copied message.</param>
    /// <param name="parseMode">Mode for parsing entities in the caption.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The copied message object.</returns>
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

    private async Task<T?> GetAsync<T>(string method, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}{method}", cancellationToken);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new BleApiException(response.StatusCode, json);

        var apiResponse = JsonSerializer.Deserialize<BleApiResponse<T>>(json, _jsonOptions);
        return apiResponse?.Ok == true ? apiResponse.Result : default;
    }

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