namespace Common.Net8.Abstractions;

public interface IBaseWriteOnlyRepository<T> where T : BaseEntity
{
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task Remove(T obj);
}

