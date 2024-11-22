using Common.Net8.Model;

namespace Common.Net8.Interface;

public interface INotificationHandle
{
    bool IsNotification();
    List<Notification> GetNotification();
    void Handle(Notification notification);
}
