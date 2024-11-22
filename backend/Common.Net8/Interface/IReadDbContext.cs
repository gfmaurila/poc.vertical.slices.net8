using Common.Net8.Abstractions;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Common.Net8.Interface;

public interface IReadDbContext : IDisposable
{
    string ConnectionString { get; }
    IMongoCollection<TQueryModel> GetCollection<TQueryModel>() where TQueryModel : IQueryModel;
    Task UpsertAsync<TQueryModel>(TQueryModel queryModel, Expression<Func<TQueryModel, bool>> upsertFilter) where TQueryModel : IQueryModel;
    Task DeleteAsync<TQueryModel>(Expression<Func<TQueryModel, bool>> deleteFilter) where TQueryModel : IQueryModel;
    Task CreateCollectionsAsync();
}
