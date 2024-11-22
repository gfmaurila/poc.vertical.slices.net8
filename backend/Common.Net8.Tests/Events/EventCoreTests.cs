using Common.Net8.Events;

namespace Common.Net8.Tests.Events;

public class EventCoreTests
{
    [Fact]
    public void EventCoreProperties_AreCorrectlyInitialized()
    {
        // Arrange
        var aggregateId = Guid.NewGuid();
        var messageType = "SampleEvent";
        var data = "{\"key\":\"value\"}";

        // Act
        var eventCore = new EventCore(aggregateId, messageType, data);

        // Assert
        Assert.Equal(aggregateId, eventCore.AggregateId);
        Assert.Equal(messageType, eventCore.MessageType);
        Assert.Equal(data, eventCore.Data);
        Assert.NotEqual(Guid.Empty, eventCore.Id);
    }

    [Fact]
    public void EventCoreIds_AreUnique()
    {
        // Arrange & Act
        var event1 = new EventCore(Guid.NewGuid(), "Event1", "{}");
        var event2 = new EventCore(Guid.NewGuid(), "Event2", "{}");

        // Assert
        Assert.NotEqual(event1.Id, event2.Id);
    }
}
