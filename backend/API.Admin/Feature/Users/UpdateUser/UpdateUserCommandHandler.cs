using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Response;
using MediatR;

namespace API.Admin.Feature.Users.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResult<UpdateUserResponse>>
{
    private readonly UpdateUserCommandValidator _validator;
    private readonly IUserRepository _repo;
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IMediator _mediator;
    public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
                                    IUserRepository repo,
                                    UpdateUserCommandValidator validator,
                                    IMediator mediator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<ApiResult<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Validanto a requisição.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResult<UpdateUserResponse>.CreateError(
                validationResult.Errors.Select(e => new ErrorDetail(e.ErrorMessage)).ToList(),
                400);

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.Id);
        if (entity == null)
            return ApiResult<UpdateUserResponse>.CreateError(
                new List<ErrorDetail> {
                    new ErrorDetail($"Nenhum registro encontrado pelo Id: {request.Id}")
                },
                400);

        entity.Update(request);

        await _repo.Update(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return ApiResult<UpdateUserResponse>.CreateSuccess(new UpdateUserResponse(entity.Id), "Atualizado com sucesso!");
    }
}
