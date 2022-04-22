using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public NotificationRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<Notification> InsertNotification(Notification notification,string UserId)
        {
            try
            {
                Notification obj = new Notification()
                {
                    UserId = UserId,
                    Title = notification.Title,
                    Description = notification.Description,
                    StartAt = DateTime.Now,
                    IsFullDay = notification.IsFullDay,
                    ReadNotifcation = false
                };
                var result = await db.Notification.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Notification> UserNotificationInsert(string Title,string Description,bool IsFullDay, string UserId)
        {
            try
            {
                Notification obj = new Notification()
                {
                    UserId = UserId,
                    Title = Title,
                    Description = Description,
                    StartAt = DateTime.Now,
                    IsFullDay = IsFullDay,
                    ReadNotifcation = false
                };
                var result = await db.Notification.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Notification> UpdateNotification(string UserId,int EventId)
        {
            try
            {
                var result = await db.Notification.FirstOrDefaultAsync(x => x.EventId == EventId && x.UserId==UserId);
                if (result != null)
                {
                    result.ReadNotifcation = true;
                    result.EndAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Notification>> GetNotificationByUserId(string UserId)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Notification
                                 where a.UserId==UserId
                                 orderby a.EventId descending
                                 select new Notification
                                 {
                                     EventId=a.EventId,
                                     UserId = a.UserId,
                                     Title = a.Title,
                                     Description = a.Description,
                                     StartAt = a.StartAt,
                                     EndAt = a.EndAt,
                                     IsFullDay = a.IsFullDay,
                                     ReadNotifcation = a.ReadNotifcation
                                 });
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<string> DeleteNotification(int EventId)
        {
            try
            {
                var result = await db.Notification.FirstOrDefaultAsync(x => x.EventId == EventId);
                if (result != null)
                {
                    db.Remove(result);
                    await db.SaveChangesAsync();
                    return "Data delete successfully";
                }
                return "Data does not exist in the current context";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Notification>> GetNotification()
        {
            if (db != null)
            {
                var query = (from a in db.Notification
                             select new Notification
                             {
                                 UserId = a.UserId,
                                 Title = a.Title,
                                 Description = a.Description,
                                 StartAt = a.StartAt,
                                 EndAt = a.EndAt,
                                 IsFullDay = a.IsFullDay,
                                 ReadNotifcation = a.ReadNotifcation
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<int> GetNotificationcount(string UserId)
        {
            if (db != null)
            {
                var query = (from a in db.Notification
                             where a.UserId==UserId && a.ReadNotifcation==false
                             select a).CountAsync();
                return await query;

            }
            return 0;
        }
    }
}
