using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Response;
using MediatR;

namespace API.Admin.Feature.Users.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApiResult<List<UserQueryModel>>>
{
    private readonly IUserRepository _repo;
    public GetUserQueryHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResult<List<UserQueryModel>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _repo.GetAllAsync();

        if (users is not null && users.Any())
            return ApiResult<List<UserQueryModel>>.CreateSuccess(users, "Usuários recuperados com sucesso.");

        return ApiResult<List<UserQueryModel>>.CreateSuccess(new List<UserQueryModel>(), "Nenhum usuário encontrado.");
    }
}
