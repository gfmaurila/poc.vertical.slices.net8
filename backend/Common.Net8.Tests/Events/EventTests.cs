namespace Common.Net8.Tests.Events;

public class EventTests
{
    [Fact]
    public void EventProperties_AreCorrectlyInitialized()
    {
        // Arrange
        var messageType = "TestEvent";
        var aggregateId = Guid.NewGuid();
        var beforeCreation = DateTime.Now;

        // Act
        var eventInstance = new TestEvent(messageType, aggregateId);
        var afterCreation = DateTime.Now;

        // Assert
        Assert.Equal(messageType, eventInstance.MessageType);
        Assert.Equal(aggregateId, eventInstance.AggregateId);
        Assert.True(eventInstance.OccurredOn >= beforeCreation && eventInstance.OccurredOn <= afterCreation);
    }
}
