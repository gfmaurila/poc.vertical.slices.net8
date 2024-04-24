using Carter;
using Mapster;
using MediatR;
using poc.vertical.slices.net8.Contracts.Article;
using poc.vertical.slices.net8.Feature.Articles;

namespace poc.vertical.slices.net8.Endpoints.Article;
public class UpdateArticleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/articles", async (UpdateArticleRequest request, ISender sender) =>
        {

            var command = request.Adapt<UpdateArticle.Command>();

            var result = await sender.Send(command);

            if (!result.IsSuccess)
                return Results.BadRequest(result.Errors);

            return Results.Ok(result.Value);
        });
    }
}

