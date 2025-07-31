namespace Pfs.Notification.Implementation.Providers.MedianaSmsProvider.DTOs;

internal class MedianaSendSmsRequest
{
    public string Type { get; set; } = null!;
    public List<string> Recipients { get; set; } = null!;
    public string MessageText { get; set; } = null!;
}