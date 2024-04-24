using Ardalis.Result;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using poc.vertical.slices.net8.Database;

namespace poc.vertical.slices.net8.Feature.Articles;
public static class DeleteArticle
{
    public class Command : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result>
    {
        private readonly EFSqlServerContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(EFSqlServerContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Invalid(validationResult.Errors.Select(e => new ValidationError
                {
                    ErrorMessage = e.ErrorMessage
                }).ToList());

            var article = await _dbContext.Article
                                           .Where(a => a.Id == request.Id)
                                           .FirstOrDefaultAsync(cancellationToken);

            if (article is null)
                return Result.Error("Id não encontrado");

            _dbContext.Remove(article);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.SuccessWithMessage("Registro removido");
        }
    }
}
