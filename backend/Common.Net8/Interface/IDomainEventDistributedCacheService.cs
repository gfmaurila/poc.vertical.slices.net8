using Common.Net8.Model;

namespace Common.Net8.Interface;

public interface IDomainEventDistributedCacheService
{
    Task<DomainEventDistributedCache> Create(DomainEventDistributedCache entity);
    Task<DomainEventDistributedCache> Update(DomainEventDistributedCache entity);
    Task<DomainEventDistributedCache> Delete(string id);
    Task<DomainEventDistributedCache> Get(string id);
    Task<List<DomainEventDistributedCache>> Get();
}
