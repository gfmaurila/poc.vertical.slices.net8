using API.Admin.Feature.Users.GetUser;
using Carter;
using Common.Net8.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace poc.vertical.slices.net8.Endpoints.User;

public class GetAllUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/user", HandleGetAllUsers)
            .WithName("GetAllUsers")
            .Produces<ApiResponse<List<UserQueryModel>>>(StatusCodes.Status200OK)
            .Produces<ApiResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Buscar todos os usuários",
                Description = "Retorna uma lista com todos os usuários registrados no sistema.",
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

    private async Task<IResult> HandleGetAllUsers(ISender sender)
    {
        var result = await sender.Send(new GetUserQuery());

        if (!result.Success)
            return Results.BadRequest(result);

        return Results.Ok(result);
    }
}