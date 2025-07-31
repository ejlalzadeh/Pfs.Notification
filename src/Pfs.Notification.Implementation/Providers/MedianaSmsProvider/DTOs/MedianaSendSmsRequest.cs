namespace Pfs.Notification.Implementation.Providers.MedianaSmsProvider.DTOs;

internal class MedianaSendSmsRequest
{
    public MedianaSmsType Type { get; set; }
    public List<string> Recipients { get; set; } = null!;
    public string MessageText { get; set; } = null!;
}