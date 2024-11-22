using Common.Net8.Handle;
using Common.Net8.Model;

namespace Common.Net8.Tests.Handle;


public class NotificationHandleTests
{
    [Fact]
    public void NotificationHandle_StartsEmpty()
    {
        // Arrange
        var handler = new NotificationHandle();

        // Act
        var notifications = handler.GetNotification();

        // Assert
        Assert.Empty(notifications);
    }

    [Fact]
    public void Handle_AddsNotification()
    {
        // Arrange
        var handler = new NotificationHandle();
        var notification = new Notification("Teste"); // Supondo que existe uma classe de notificação definida em algum lugar.

        // Act
        handler.Handle(notification);

        // Assert
        Assert.Single(handler.GetNotification());
        Assert.Contains(notification, handler.GetNotification());
    }

    [Fact]
    public void IsNotification_ReturnsTrue_WhenNotificationsExist()
    {
        // Arrange
        var handler = new NotificationHandle();
        var notification = new Notification("Teste");

        // Act
        handler.Handle(notification);

        // Assert
        Assert.True(handler.IsNotification());
    }

    [Fact]
    public void IsNotification_ReturnsFalse_WhenNoNotifications()
    {
        // Arrange
        var handler = new NotificationHandle();

        // Act
        bool result = handler.IsNotification();

        // Assert
        Assert.False(result);
    }


    [Fact]
    public void Constructor_SetsMessageProperty()
    {
        // Arrange
        var expectedMessage = "Test message";

        // Act
        var notification = new Notification(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, notification.Message);
    }

}