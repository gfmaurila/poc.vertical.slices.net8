using Ardalis.Result;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using poc.vertical.slices.net8.Contracts.Article;
using poc.vertical.slices.net8.Database;

namespace poc.vertical.slices.net8.Feature.Articles;
public static class GetArticleById
{
    public class Query : IRequest<Result<ArticleResponse>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, Result<ArticleResponse>>
    {
        private readonly EFSqlServerContext _dbContext;

        public Handler(EFSqlServerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ArticleResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var article = await _dbContext.Article
                                           .Where(a => a.Id == request.Id)
                                           .Select(a => new ArticleResponse
                                           {
                                               Id = a.Id,
                                               Title = a.Title,
                                               Description = a.Description,
                                               CreatedOnUtc = a.CreatedOnUtc,
                                               PublishedOnUtc = a.PublishedOnUtc
                                           })
                                           .FirstOrDefaultAsync(cancellationToken);

            if (article is null)
                return Result.Error("Id não encontrado");

            return article;
        }
    }
}