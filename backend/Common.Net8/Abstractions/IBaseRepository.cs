namespace Common.Net8.Abstractions;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task Remove(T obj);
    Task<T> Get(Guid id);
    Task<List<T>> Get();
}

