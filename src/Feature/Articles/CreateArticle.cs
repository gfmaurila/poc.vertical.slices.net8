using Ardalis.Result;
using FluentValidation;
using MediatR;
using poc.core.api.net8;
using poc.vertical.slices.net8.Database;
using poc.vertical.slices.net8.Domain;

namespace poc.vertical.slices.net8.Feature.Articles;
public static class CreateArticle
{
    public class Command : IRequest<Result<ArticleResponse>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
    }

    public class ArticleResponse : BaseResponse
    {
        public ArticleResponse(Guid id) => Id = id;

        public Guid Id { get; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<ArticleResponse>>
    {
        private readonly EFSqlServerContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(EFSqlServerContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        public async Task<Result<ArticleResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Invalid(validationResult.Errors.Select(e => new ValidationError
                {
                    ErrorMessage = e.ErrorMessage
                }).ToList());

            var article = new Article
            {
                Title = request.Title,
                Description = request.Description,
                CreatedOnUtc = DateTime.UtcNow,
            };

            _dbContext.Add(article);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(new ArticleResponse(article.Id), "Registro cadastrado");
        }
    }
}
