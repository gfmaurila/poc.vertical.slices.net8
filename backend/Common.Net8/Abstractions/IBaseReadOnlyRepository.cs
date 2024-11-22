namespace Common.Net8.Abstractions;
public interface IBaseReadOnlyRepository<T> where T : BaseEntity
{
    Task<T> Get(Guid id);
    Task<List<T>> Get();
}


