using API.Admin.Feature.Users.UpdatePassword;
using Carter;
using Common.Net8.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace poc.vertical.slices.net8.Endpoints.User;

public class UpdatePasswordUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/user/updatepassword", HandleUpdatePassword)
            .WithName("UpdateUserPassword")
            .Produces<ApiResponse>(StatusCodes.Status200OK)
            .Produces<ApiResponse>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponse>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Atualizar senha de usuário",
                Description = "Atualiza a senha de um usuário existente. A requisição deve conter o ID do usuário e a nova senha.",
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

    private async Task<IResult> HandleUpdatePassword(UpdatePasswordUserCommand command, ISender sender)
    {
        var result = await sender.Send(command);

        if (!result.Success)
            return Results.BadRequest(result);

        return Results.Ok(result);
    }
}

