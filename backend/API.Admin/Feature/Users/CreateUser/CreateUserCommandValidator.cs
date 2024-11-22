using FluentValidation;

namespace API.Admin.Feature.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Email)
            .NotEmpty()
            .MaximumLength(254)
            .EmailAddress();

        RuleFor(command => command.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100)
            .Matches(@"(?=.*\d)") // Pelo menos um número
            .Matches(@"(?=.*[a-z])") // Pelo menos uma letra minúscula
            .Matches(@"(?=.*[A-Z])") // Pelo menos uma letra maiúscula
            .Matches(@"(?=.*\W)") // Pelo menos um caractere especial
            .Matches(@"^(?!.*123).*$") // Não permitir a sequência "123"
            .WithMessage("A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número, um caractere especial e não pode conter sequências simples como '123'.");

        RuleFor(command => command.ConfirmPassword)
            .Equal(command => command.Password)
            .WithMessage("A confirmação da senha deve ser igual à senha.");

        // Validação para DateOfBirth, garantindo que é uma data passada e o campo é obrigatório
        RuleFor(command => command.DateOfBirth)
            .NotEmpty()
            .WithMessage("Data de nascimento é obrigatória.")
            .LessThan(DateTime.Today)
            .WithMessage("Data de nascimento deve ser uma data passada.");

        RuleFor(command => command.DateOfBirth)
        .NotEmpty()
        .WithMessage("Data de nascimento é obrigatória.")
        .LessThan(DateTime.Today)
        .WithMessage("Data de nascimento deve ser uma data passada.")
        .LessThan(DateTime.Today) // Isso garante que a data de nascimento não é uma data futura.
        .Must(BeAtLeast18YearsOld) // Isso garante que o usuário tem pelo menos 18 anos de idade.
        .WithMessage("O usuário deve ter pelo menos 18 anos de idade.");


    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Today.AddYears(-18);
    }
}