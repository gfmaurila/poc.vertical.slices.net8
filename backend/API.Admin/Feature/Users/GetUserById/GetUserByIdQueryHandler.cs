using API.Admin.Feature.Users.GetUser;
using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Response;
using MediatR;

namespace API.Admin.Feature.Users.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResult<UserQueryModel>>
{
    private readonly IUserRepository _repo;
    private readonly GetUserByIdQueryValidator _validator;
    public GetUserByIdQueryHandler(IUserRepository repo,
                                   GetUserByIdQueryValidator validator)
    {
        _repo = repo;
        _validator = validator;
    }

    public async Task<ApiResult<UserQueryModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResult<UserQueryModel>.CreateError(
                validationResult.Errors.Select(e => new ErrorDetail(e.ErrorMessage)).ToList(),
                400);

        var entity = await _repo.GetByIdAsync(request.Id);

        // DB SQL Server
        if (entity is not null)
            return ApiResult<UserQueryModel>.CreateSuccess(
                await _repo.GetByIdAsync(request.Id),
                "Usuário recuperado com sucesso.");

        return ApiResult<UserQueryModel>.CreateSuccess(entity, "Nenhum registro encontrado!");
    }

}