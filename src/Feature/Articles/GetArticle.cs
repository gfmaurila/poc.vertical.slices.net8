using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using poc.vertical.slices.net8.Contracts.Article;
using poc.vertical.slices.net8.Database;

namespace poc.vertical.slices.net8.Feature.Articles;
public static class GetArticle
{
    public class Query : IRequest<Result<List<ArticleResponse>>>
    {
    }

    internal sealed class Handler : IRequestHandler<Query, Result<List<ArticleResponse>>>
    {
        private readonly EFSqlServerContext _dbContext;

        public Handler(EFSqlServerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<ArticleResponse>>> Handle(Query request, CancellationToken cancellationToken)
            => Result.Success(await _dbContext.Article.Select(a => new ArticleResponse
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CreatedOnUtc = a.CreatedOnUtc,
                PublishedOnUtc = a.PublishedOnUtc
            }).ToListAsync());
    }
}