using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Common.Net8.Extensions;

[ExcludeFromCodeCoverage]
public static class ConfigurationExtensions
{
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string configSectionPath)
        where TOptions : BaseOptions
    {
        return configuration
            .GetSection(configSectionPath)
            .Get<TOptions>(binderOptions => binderOptions.BindNonPublicProperties = true);
    }
}