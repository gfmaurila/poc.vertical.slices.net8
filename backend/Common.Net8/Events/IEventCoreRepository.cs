namespace Common.Net8.Events;

/// <summary>
/// Repositório de armazenamento de eventos.
/// </summary>
public interface IEventCoreRepository : IDisposable
{
    /// <summary>
    /// Salva uma lista de eventos.
    /// </summary>
    /// <param name="eventStores">A lista de eventos.</param>
    Task StoreAsync(IEnumerable<EventCore> eventStores);
}