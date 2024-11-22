namespace Common.Net8.Interface;

public interface IMessageBusService
{
    void Publish(string queue, byte[] message);
}
