using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Pfs.Notification.Implementation.Common;
using Pfs.Notification.Implementation.Providers.MedianaSmsProvider.DTOs;

namespace Pfs.Notification.Implementation.Providers.MedianaSmsProvider;

internal class MedianaSmsProvider : ISmsProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<MedianaSmsProvider> _logger;

    public MedianaSmsProvider(IHttpClientFactory httpClientFactory, ILogger<MedianaSmsProvider> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task SendSms(string recipientNumber, string message, CancellationToken cancellationToken)
    {
        string apiRoute = "sms/v1/send/sms";

        MedianaSendSmsRequest request = new()
        {
            Type = "Informational",
            MessageText = message,
            Recipients = new List<string> { recipientNumber }
        };

        try
        {
            HttpClient httpClient = CreateHttpClient();

            string json = JsonSerializer.Serialize(request);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, apiRoute)
            {
                Content = new StringContent(json, Encoding.UTF8, mediaType: ConstantValues.ApplicationJsonMediaType)
            };

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    message: "Failed to send SMS via Mediana Provider, ResponseStatusCode: {StatusCode}, Response: {Response}",
                    httpResponse.StatusCode, responseContent);

                throw new Exception("Failed to send SMS via Provider");
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, message: "An exception occurred while sending sms!");
            throw;
        }
    }

    private HttpClient CreateHttpClient() => _httpClientFactory.CreateClient(name: ConstantValues.MedianaSmsProviderHttpClientName);
}