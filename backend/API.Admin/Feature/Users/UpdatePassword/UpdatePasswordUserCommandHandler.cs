using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Extensions;
using Common.Net8.Response;
using MediatR;

namespace API.Admin.Feature.Users.UpdatePassword;

public class UpdatePasswordUserCommandHandler : IRequestHandler<UpdatePasswordUserCommand, ApiResult<UpdatePasswordUserResponse>>
{
    private readonly UpdatePasswordUserCommandValidator _validator;
    private readonly IUserRepository _repo;
    private readonly ILogger<UpdatePasswordUserCommandHandler> _logger;
    private readonly IMediator _mediator;
    public UpdatePasswordUserCommandHandler(ILogger<UpdatePasswordUserCommandHandler> logger,
                                    IUserRepository repo,
                                    UpdatePasswordUserCommandValidator validator,
                                    IMediator mediator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<ApiResult<UpdatePasswordUserResponse>> Handle(UpdatePasswordUserCommand request, CancellationToken cancellationToken)
    {
        // Validanto a requisição.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResult<UpdatePasswordUserResponse>.CreateError(
                validationResult.Errors.Select(e => new ErrorDetail(e.ErrorMessage)).ToList(),
                400);

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.Id);
        if (entity == null)
            if (entity == null)
                return ApiResult<UpdatePasswordUserResponse>.CreateError(
                    new List<ErrorDetail> {
                    new ErrorDetail($"Nenhum registro encontrado pelo Id: {request.Id}")
                    },
                    400);

        entity.ChangePassword(Password.ComputeSha256Hash(request.Password));

        await _repo.Update(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return ApiResult<UpdatePasswordUserResponse>.CreateSuccess(new UpdatePasswordUserResponse(entity.Id), "Atualizado com sucesso!");
    }
}
