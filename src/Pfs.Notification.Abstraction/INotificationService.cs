namespace Pfs.Notification.Abstraction;

public interface INotificationService
{
    Task SendSms(string recipientNumber, string message, CancellationToken cancellationToken);
    Task SendEmail(string receiverEmail, string message, CancellationToken cancellationToken);
}