using Common.Net8.Interface;
using Common.Net8.Model;

namespace Common.Net8.Handle;

public class NotificationHandle : INotificationHandle
{
    private List<Notification> _notification;

    public NotificationHandle()
    {
        _notification = new List<Notification>();
    }

    public void Handle(Notification notification)
    {
        _notification.Add(notification);
    }

    public List<Notification> GetNotification()
    {
        return _notification;
    }

    public bool IsNotification()
    {
        return _notification.Any();
    }
}
