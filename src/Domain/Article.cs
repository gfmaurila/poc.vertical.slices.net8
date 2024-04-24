using poc.core.api.net8;
using poc.core.api.net8.api.net8.api.net8.Abstractions;

namespace poc.vertical.slices.net8.Domain;

public class Article : BaseEntity, IAggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
}
