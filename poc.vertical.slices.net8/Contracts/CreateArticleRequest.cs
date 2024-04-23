namespace poc.vertical.slices.net8.Contracts;

public class CreateArticleRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}
