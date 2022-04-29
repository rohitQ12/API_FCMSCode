using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface INotificationRepository
    {
        Task<Notification> InsertNotification(string Title, string Description, bool IsFullDay, string UserId);
        Task<Notification> UpdateNotification(string UserId, int EventId);
        Task<List<Notification>> GetNotificationByUserId(string UserId);
        Task<string> DeleteNotification(int EventId);
        Task<List<Notification>> GetNotification();
        Task<int> GetNotificationcount(string UserId);
    }
}
