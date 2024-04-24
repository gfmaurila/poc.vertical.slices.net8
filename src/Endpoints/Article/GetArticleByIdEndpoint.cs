using Carter;
using MediatR;
using poc.vertical.slices.net8.Feature.Articles;

namespace poc.vertical.slices.net8.Endpoints.Article;

public class GetArticleByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/articles/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetArticleById.Query { Id = id };
            var result = await sender.Send(query);

            if (!result.IsSuccess)
                return Results.BadRequest(result.Errors);

            return Results.Ok(result.Value);
        });
    }
}
