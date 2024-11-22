using API.Admin.Domain.User;
using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Extensions;
using Common.Net8.Response;
using Common.Net8.ValueObjects;
using MediatR;

namespace API.Admin.Feature.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResult<CreateUserResponse>>
{
    private readonly CreateUserCommandValidator _validator;
    private readonly IUserRepository _repo;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IMediator _mediator;
    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger,
                                    IUserRepository repo,
                                    IMediator mediator,
                                    CreateUserCommandValidator validator)
    {
        _repo = repo;
        _logger = logger;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<ApiResult<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResult<CreateUserResponse>.CreateError(
                validationResult.Errors.Select(e => new ErrorDetail(e.ErrorMessage)).ToList(),
                400);

        var email = new Email(request.Email);
        var phone = new PhoneNumber(request.Phone);

        if (await _repo.ExistsByEmailAsync(email))
            return ApiResult<CreateUserResponse>.CreateError(
                new List<ErrorDetail> {
                    new ErrorDetail("O endereço de e-mail informado já está sendo utilizado.")
                },
                400);

        var entity = new UserEntity(request.FirstName,
            request.LastName,
            email,
            phone,
            Password.ComputeSha256Hash(request.Password),
            request.DateOfBirth);

        await _repo.Create(entity);

        // Executa eventos
        foreach (var domainEvent in entity.DomainEvents)
            await _mediator.Publish(domainEvent);
        entity.ClearDomainEvents();

        return ApiResult<CreateUserResponse>.CreateSuccess(new CreateUserResponse(entity.Id), "Cadastrado com sucesso!");
    }
}
