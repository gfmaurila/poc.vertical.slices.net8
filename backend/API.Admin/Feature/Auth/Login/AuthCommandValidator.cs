using FluentValidation;

namespace API.Admin.Feature.Auth.Login;

public class AuthCommandValidator : AbstractValidator<AuthCommand>
{
    public AuthCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage("O campo de e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("O e-mail fornecido não é válido.")
            .MaximumLength(200)
            .WithMessage("O e-mail não pode ter mais de 200 caracteres.");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("O campo de senha é obrigatório.");
    }
}
