using System.Diagnostics.CodeAnalysis;

namespace Common.Net8.AppSettings;

[ExcludeFromCodeCoverage]
public class CacheOptions
{
    public const string ConfigSectionPath = nameof(CacheOptions);
    public int AbsoluteExpirationInHours { get; set; }
    public int SlidingExpirationInSeconds { get; set; }
    public int DbIndex { get; set; }
}