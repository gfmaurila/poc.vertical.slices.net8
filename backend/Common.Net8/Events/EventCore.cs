namespace Common.Net8.Events;

/// <summary>
/// A classe de armazenamento de evento.
/// </summary>
public class EventCore : Event
{
    public EventCore(Guid aggregateId, string messageType, string data)
    {
        AggregateId = aggregateId;
        MessageType = messageType;
        Data = data;
    }

    private EventCore() { } // ORM

    /// <summary>
    /// ID do evento.
    /// </summary>
    public Guid Id { get; private init; } = Guid.NewGuid();

    /// <summary>
    /// O dadso do evento serializado em JSON.
    /// </summary>
    public string Data { get; private init; }
}