using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Pfs.Notification.Abstraction;
using Pfs.Notification.Implementation.Common;
using Pfs.Notification.Implementation.Providers.MedianaSmsProvider;

namespace Pfs.Notification.Implementation.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ISmsProvider, MedianaSmsProvider>();

        services.AddHttpClient(
            name: ConstantValues.MedianaSmsProviderHttpClientName,
            configureClient: config =>
            {
                config.BaseAddress = new Uri("https://api.mediana.ir");
                config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    scheme: "Bearer",
                    parameter: "Yt2NjrtRr57i60tPVfMUBETpPlhTCR7Vpl7Xyal5hE=");
            });
    }
}