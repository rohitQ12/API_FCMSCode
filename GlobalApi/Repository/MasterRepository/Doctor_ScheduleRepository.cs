using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class Doctor_ScheduleRepository : IDoctor_ScheduleInterface
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Doctor_ScheduleRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        //get
        public async Task<List<Doctor_ScheduleModule>> GetDoctor_Schedule()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Doctor_Schedules
                                 where a.Delete_status == 0 && a.Is_active == 1
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

        //get by id

        public async Task<List<Doctor_ScheduleModule>> GetDoctor_ScheduleById(int id)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Doctor_Schedules
                                 where a.Delete_status == 0 && a.Doc_schedule_Id == id && a.Is_active == 1
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

        public async Task<string> Insert_DoctorSchedule(Doctor_ScheduleModule Sc)
        {
            try
            {

                var data = await db.Doctor_Schedules.FirstOrDefaultAsync(x => x.Doc_schedule_Id == Sc.Doc_schedule_Id);
                if (data == null)
                {
                    DateTime Doc_Sched_tf = Convert.ToDateTime(Sc.Time_from);
                    DateTime Doc_Sched_tt = Convert.ToDateTime(Sc.Time_to);


                    if (Doc_Sched_tf < Doc_Sched_tt)
                    {
                        int _new = await primarykeyvalue.primary_key("Doctor_Schedule");
                        Doctor_ScheduleModule obj = new Doctor_ScheduleModule()
                        {
                            Doc_schedule_Id = _new,
                            DO_Id_FK = Sc.DO_Id_FK,
                            Do_Scd_day = Sc.Do_Scd_day,
                            Time_from = Sc.Time_from,
                            Time_to = Sc.Time_to,
                            Added_date = DateTime.Now,
                            Delete_status = 0,
                            Added_by = 1,
                            Is_active = 1

                        };
                        var result = await db.Doctor_Schedules.AddAsync(obj);
                        await db.SaveChangesAsync();

                        db.Entry(obj).GetDatabaseValues();
                        int id = obj.Doc_schedule_Id;
                        int _new1 = await primarykeyvalue.primary_key("Doctor_Schedule_history");
                        Schedule_historyModel obj1 = new Schedule_historyModel()
                        {
                            Doc_schedule_history_Id = _new1,
                            Doc_schedule_Id = id,
                            DO_Id_FK = Sc.DO_Id_FK,
                            Do_Scd_day = Sc.Do_Scd_day,
                            Time_from = Sc.Time_from,
                            Time_to = Sc.Time_to,
                            Added_date = DateTime.Now,
                            Delete_status = 0,
                            Added_by = 1,
                            Is_active = 1

                        };

                        await db.Doctor_Schedule_history.AddAsync(obj1);
                        await db.SaveChangesAsync();
                        return "Successfuly insert data";
                    }

                    return "Select End-Time More Than Start-Time ";

                }
                return "Alerady exits";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UpdateDoctor_Schedule(Doctor_ScheduleModule Su)
        {
            try
            {
                var result = await db.Doctor_Schedules.FirstOrDefaultAsync(x => x.Doc_schedule_Id == Su.Doc_schedule_Id);
                if (result != null)
                {
                    DateTime Doc_Sched_tf = Convert.ToDateTime(Su.Time_from);
                    DateTime Doc_Sched_tt = Convert.ToDateTime(Su.Time_to);

                    if (Doc_Sched_tf < Doc_Sched_tt)
                    {
                        result.Doc_schedule_Id = Su.Doc_schedule_Id;
                        result.DO_Id_FK = Su.DO_Id_FK;
                        result.Do_Scd_day = Su.Do_Scd_day;
                        result.Time_from = Su.Time_from;
                        result.Time_to = Su.Time_to;
                        result.Is_active = 1;
                        result.Modified_date = DateTime.Now;
                        result.Modified_by = 1;

                        await db.SaveChangesAsync();

                        db.Entry(Su).GetDatabaseValues();
                        int id = Su.Doc_schedule_Id;
                        int _new1 = await primarykeyvalue.primary_key("Doctor_Schedule_history");
                        Schedule_historyModel obj1 = new Schedule_historyModel()
                        {
                            Doc_schedule_history_Id = _new1,
                            Doc_schedule_Id = id,
                            DO_Id_FK = Su.DO_Id_FK,
                            Do_Scd_day = Su.Do_Scd_day,
                            Time_from = Su.Time_from,
                            Time_to = Su.Time_to,
                            Added_date = Su.Added_date,
                            Modified_date = DateTime.Now,
                            Modified_by = 1,
                            Delete_status = 0,
                            Added_by = 1,
                            Is_active = 1

                        };

                        await db.Doctor_Schedule_history.AddAsync(obj1);
                        await db.SaveChangesAsync();
                        return "Successfuly updated";
                    }
                    return "Select End-Time More Than Start-Time ";
                }
                return "Alerady exits";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Doctor_ScheduleModule> DeleteDoctor_Schedule(int Id)
        {
            try
            {
                var result = await db.Doctor_Schedules.FirstOrDefaultAsync(x => x.Doc_schedule_Id == Id);
                if (result != null)
                {
                    result.Doc_schedule_Id = Id;
                    result.Modified_date = DateTime.Now;
                    result.Delete_status = 1;
                    result.Deleted_date = DateTime.Now;
                    result.Is_active = 0;

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

        //public Task<Doctor_ScheduleModule> DeleteDoctor_Schedule(Doctor_ScheduleModule Id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
