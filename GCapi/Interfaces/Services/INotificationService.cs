
namespace gcapi.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendToTopicAsync(string topic, string title, string body);
    }
}
