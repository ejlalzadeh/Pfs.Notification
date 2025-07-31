namespace Pfs.Notification.Implementation;

internal interface ISmsProvider
{
    Task SendSms(string recipientNumber, string message, CancellationToken cancellationToken);
}