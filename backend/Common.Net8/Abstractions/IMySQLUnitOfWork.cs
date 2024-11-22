namespace Common.Net8.Abstractions;

public interface IMySQLUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}
