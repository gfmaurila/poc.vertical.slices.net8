using poc.core.api.net8.api.net8.api.net8.Abstractions;
using poc.core.api.net8;

namespace poc.vertical.slices.net8.Domain;

public class Article : BaseEntity, IAggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
}
