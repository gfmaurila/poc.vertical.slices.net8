using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Response;
using Common.Net8.ValueObjects;
using MediatR;

namespace API.Admin.Feature.Users.UpdateEmail;

public class UpdateEmailUserCommandHandler : IRequestHandler<UpdateEmailUserCommand, ApiResult<UpdateEmailUserResponse>>
{
    private readonly UpdateEmailUserCommandValidator _validator;
    private readonly IUserRepository _repo;
    private readonly ILogger<UpdateEmailUserCommandHandler> _logger;
    private readonly IMediator _mediator;
    public UpdateEmailUserCommandHandler(ILogger<UpdateEmailUserCommandHandler> logger,
                                    IUserRepository repo,
                                    UpdateEmailUserCommandValidator validator,
                                    IMediator mediator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<ApiResult<UpdateEmailUserResponse>> Handle(UpdateEmailUserCommand request, CancellationToken cancellationToken)
    {
        // Validanto a requisição.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResult<UpdateEmailUserResponse>.CreateError(
                validationResult.Errors.Select(e => new ErrorDetail(e.ErrorMessage)).ToList(),
                400);

        // Obtendo o registro da base.
        var entity = await _repo.Get(request.Id);
        if (entity == null)
            if (entity == null)
                return ApiResult<UpdateEmailUserResponse>.CreateError(
                    new List<ErrorDetail> {
                    new ErrorDetail($"Nenhum registro encontrado pelo Id: {request.Id}")
                    },
                    400);

        // Instanciando o VO Email.
        var newEmail = new Email(request.Email);

        // Verificiando se já existe um cliente com o endereço de e-mail.
        if (await _repo.ExistsByEmailAsync(newEmail, entity.Id))
            return ApiResult<UpdateEmailUserResponse>.CreateError(
                new List<ErrorDetail> {
                    new ErrorDetail("O endereço de e-mail informado já está sendo utilizado.")
                },
                400);

        entity.ChangeEmail(newEmail);

        await _repo.Update(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);

        entity.ClearDomainEvents();

        return ApiResult<UpdateEmailUserResponse>.CreateSuccess(new UpdateEmailUserResponse(entity.Id), "Atualizado com sucesso!");
    }
}
