namespace poc.vertical.slices.net8.Contracts.Article;

public class UpdateArticleRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
