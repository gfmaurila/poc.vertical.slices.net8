using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Response;
using MediatR;

namespace API.Admin.Feature.Users.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResult<DeleteUserResponse>>
{
    private readonly DeleteUserCommandValidator _validator;
    private readonly IUserRepository _repo;
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IMediator _mediator;
    public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger,
                                    IUserRepository repo,
                                    DeleteUserCommandValidator validator,
                                    IMediator mediator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<ApiResult<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Validanto a requisição.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResult<DeleteUserResponse>.CreateError(
                validationResult.Errors.Select(e => new ErrorDetail(e.ErrorMessage)).ToList(),
                400);

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.Id);
        if (entity == null)
            return ApiResult<DeleteUserResponse>.CreateError(
                new List<ErrorDetail> {
                    new ErrorDetail($"Nenhum registro encontrado pelo Id: {request.Id}")
                },
                400);

        entity.Delete();

        await _repo.Remove(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return ApiResult<DeleteUserResponse>.CreateSuccess(new DeleteUserResponse(request.Id), "Removido com sucesso!");
    }
}
