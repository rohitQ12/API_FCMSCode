using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class Doctor_SchedulehistoryRepository : IDoctor_Schedulehistory
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Doctor_SchedulehistoryRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        //get
        public async Task<List<Schedule_historyModel>> GetDoctor_Schedulehistory()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Doctor_Schedule_history select a);
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //get by id

        public async Task<List<Schedule_historyModel>> GetDoctor_Schedulehistory(int id)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Doctor_Schedule_history
                                 where a.Doc_schedule_history_Id == id
                                 select a);
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
