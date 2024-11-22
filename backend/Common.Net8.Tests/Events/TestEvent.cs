using Common.Net8.Events;

namespace Common.Net8.Tests.Events;

public class TestEvent : Event
{
    public TestEvent(string messageType, Guid aggregateId)
    {
        MessageType = messageType;
        AggregateId = aggregateId;
    }
}
