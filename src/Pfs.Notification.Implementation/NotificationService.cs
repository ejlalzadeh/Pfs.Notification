using Microsoft.Extensions.Logging;
using Pfs.Notification.Abstraction;

namespace Pfs.Notification.Implementation;

internal class NotificationService : INotificationService
{
    private readonly ISmsProvider _smsProvider;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ISmsProvider smsProvider, ILogger<NotificationService> logger)
    {
        _smsProvider = smsProvider;
        _logger = logger;
    }

    public async Task SendSms(string recipientNumber, string message, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(recipientNumber))
                throw new ArgumentException("Recipient number cannot be null or empty.", nameof(recipientNumber));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));

            await _smsProvider.SendSms(recipientNumber, message, cancellationToken);
        }
        catch (Exception exception) when (exception is not ArgumentException)
        {
            _logger.LogError(
                exception,
                message: "An exception occurred while sending SMS, RecipientNumber: {RecipientNumber}",
                recipientNumber);

            throw;
        }
    }

    public Task SendEmail(string receiverEmail, string message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}