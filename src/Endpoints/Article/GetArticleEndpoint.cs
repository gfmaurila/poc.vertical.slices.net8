using Carter;
using MediatR;
using poc.vertical.slices.net8.Feature.Articles;

namespace poc.vertical.slices.net8.Endpoints.Article;

public class GetArticleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/articles", async (ISender sender) =>
        {
            var result = await sender.Send(new GetArticle.Query());
            return Results.Ok(result.Value);
        });
    }
}
