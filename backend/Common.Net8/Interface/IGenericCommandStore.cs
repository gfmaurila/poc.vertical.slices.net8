using System.Linq.Expressions;

namespace Common.Net8.Interface;

public interface IGenericCommandStore<TEntity>
{
    Task<TEntity> GetById(string id);
    Task<TEntity> Create(TEntity entity);
    Task Delete(string id);
    Task Update(string id, TEntity entity);
    Task<List<TEntity>> List(Expression<Func<TEntity, bool>> filterExpression);
}
