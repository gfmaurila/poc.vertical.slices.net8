using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using Common.Net8.Extensions;
using Common.Net8.Interface;
using Common.Net8.Response;
using MediatR;

namespace API.Admin.Feature.Auth.Login;

public class AuthCommandHandler : IRequestHandler<AuthCommand, ApiResult<AuthTokenResponse>>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _repo;
    private readonly IMediator _mediator;
    private readonly AuthCommandValidator _validator;

    public AuthCommandHandler(IAuthService authService,
                              IUserRepository repo,
                              IMediator mediator,
                              AuthCommandValidator validator)
    {
        _authService = authService;
        _repo = repo;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<ApiResult<AuthTokenResponse>> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return ApiResult<AuthTokenResponse>.CreateError(
                validationResult.Errors.Select(e => new ErrorDetail(e.ErrorMessage)).ToList(),
                400);

        var auth = await _repo.GetAuthByEmailPassword(request.Email, Password.ComputeSha256Hash(request.Password));

        //Se não existir, erro no login
        if (auth is null)
            return ApiResult<AuthTokenResponse>.CreateError(
                new List<ErrorDetail> {
                    new ErrorDetail("Usuário ou senha inválidos.")
                },
                400);

        var token = _authService.GenerateJwtToken(auth.Id.ToString(), auth.Email.Address);

        auth.AuthEvent(auth.Id.ToString(), auth.LastName, auth.Email.Address, token);

        // Executa eventos
        foreach (var domainEvent in auth.DomainEvents)
            await _mediator.Publish(domainEvent);

        auth.ClearDomainEvents();

        return ApiResult<AuthTokenResponse>.CreateSuccess(new AuthTokenResponse(token), "Sucesso!");
    }
}
