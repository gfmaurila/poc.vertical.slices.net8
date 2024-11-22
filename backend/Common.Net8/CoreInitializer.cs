using Common.Net8.API.Auth;
using Common.Net8.Handle;
using Common.Net8.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Common.Net8;

[ExcludeFromCodeCoverage]
public class CoreInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        services.AddTransient<INotificationHandle, NotificationHandle>();
        services.AddTransient<IAuthService, AuthService>();
    }
}
