using API.Admin.Feature.Users.GetUser;
using API.Admin.Feature.Users.GetUserById;
using Carter;
using Common.Net8.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace poc.vertical.slices.net8.Endpoints.User;
public class GetUserByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/user/{id}", HandleGetUserById)
            .WithName("GetUserById")
            .Produces<ApiResponse<UserQueryModel>>(StatusCodes.Status200OK)
            .Produces<ApiResponse>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponse>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Buscar usuário por ID",
                Description = "Retorna um usuário específico pelo seu ID",
                Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "Usuários"
                    }
                }
            })
            .RequireAuthorization(new AuthorizeAttribute());
    }
    private async Task<IResult> HandleGetUserById(Guid id, ISender sender)
    {
        var query = new GetUserByIdQuery(id);
        var result = await sender.Send(query);

        if (!result.Success)
            return Results.BadRequest(result);

        return Results.Ok(result);
    }
}