using Carter;
using MediatR;
using poc.vertical.slices.net8.Feature.Articles;

namespace poc.vertical.slices.net8.Endpoints.Article;

public class DeleteArticleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/articles/{id}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteArticle.Command { Id = id };
            var result = await sender.Send(query);

            if (!result.IsSuccess)
                return Results.BadRequest(result.Errors);

            return Results.Ok(result.Value);
        });
    }
}
