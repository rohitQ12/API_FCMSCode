using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GlobalApi.Repository.MasterRepository
{
    public class ReVisitRepository : IReVisit
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public ReVisitRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<ReVisit> InsertReVisit(ReVisit lead)
        {
            try
            {
                var duplicate = await db.ReVisit.FirstOrDefaultAsync(x => x.RV_Id == lead.RV_Id);
                //var datet = DateTime.Parse(lead.Date);
                //var date = datet.ToString("yyyy-MM-dd");
                //var datet = DateTime.Parse(lead.RV_Date);
                //var datetim = datet.ToString("yyyy-MM-dd");

                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("ReVisit");
                    ReVisit obj = new ReVisit()
                    {
                        RV_Id = id,
                        CON_Id = lead.CON_Id,
                        Date = lead.Date,
                        Doctor_Id = lead.Doctor_Id,
                        RV_Date = lead.RV_Date,
                        RV_Time = DateTime.ParseExact(lead.RV_Time, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Remarks = lead.Remarks,
                        Created_by = 1,
                        Created_date = DateTime.Now,
                        Delete_flag = false,
                        Status = 1
                    };
                    var result = await db.ReVisit.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public async Task<ReVisit> UpdateReVisit(ReVisit lead)
        //{
        //    try
        //    {
        //        var result = await db.ReVisit.FirstOrDefaultAsync(x => x.RV_Id == lead.RV_Id);
        //        if (result != null)
        //        {
        //            result.RV_Id = lead.RV_Id;
        //            result.CON_Id = lead.CON_Id;
        //            result.Date = lead.Date;
        //            result.Doctor_Id = lead.Doctor_Id;
        //            result.RV_Date = lead.RV_Date;
        //            result.RV_Time = lead.RV_Time;
        //            result.Remarks = lead.Remarks;
        //            result.Modified_by = 2;
        //            result.Modified_date = DateTime.Now;
        //            result.Delete_flag = false;
        //            result.Status = 2;
        //            await db.SaveChangesAsync();
        //            return result;
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        public async Task<List<GetAllReVisit>> GetAllReVisit()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.ReVisit
                                 join b in db.Doctor on a.Doctor_Id equals b.DO_Id into blist
                                 from b in blist.DefaultIfEmpty()
                                 orderby a.RV_Id descending
                                 select new GetAllReVisit
                                 {
                                     RV_Id = a.RV_Id,
                                     CON_Id = a.CON_Id,
                                     Date = a.Date,
                                     Doctor_Id = a.Doctor_Id,
                                     Doctor_Name = string.Concat(b.DO_FirstName,b.DO_LastName),
                                     RV_Date = a.RV_Date,
                                     RV_Time = a.RV_Time,
                                     Remarks = a.Remarks,
                                     Delete_flag = a.Delete_flag,
                                     Status = a.Status
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
        public async Task<ReVisit> DeleteReVisit(int RV_Id)
        {
            try
            {
                var result = await db.ReVisit.FirstOrDefaultAsync(x => x.RV_Id == RV_Id);
                if (result != null)
                {
                    result.RV_Id = RV_Id;
                    result.Deleted_by = 1;
                    result.Deleted_date = DateTime.Now;
                    result.Delete_flag = true;
                    result.Status = 6;
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
        public async Task<GetAllReVisit> GetReVisitByCON_Id(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.ReVisit
                             join b in db.Doctor on a.Doctor_Id equals b.DO_Id into blist
                             from b in blist.DefaultIfEmpty()
                             where a.CON_Id == CON_Id
                             select new GetAllReVisit
                             {
                                 RV_Id = a.RV_Id,
                                 CON_Id = a.CON_Id,
                                 Date = a.Date,
                                 Doctor_Id = a.Doctor_Id,
                                 Doctor_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                 RV_Date = a.RV_Date,
                                 RV_Time = a.RV_Time,
                                 Remarks = a.Remarks,
                                 Delete_flag = a.Delete_flag,
                                 Status = a.Status

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<GetAllReVisit> GetReVisitById(int RV_Id)
        {
            if (db != null)
            {
                var query = (from a in db.ReVisit
                             join b in db.Doctor on a.Doctor_Id equals b.DO_Id into blist
                             from b in blist.DefaultIfEmpty()
                             where a.RV_Id == RV_Id
                             select new GetAllReVisit
                             {
                                 RV_Id = a.RV_Id,
                                 CON_Id = a.CON_Id,
                                 Date = a.Date,
                                 Doctor_Id = a.Doctor_Id,
                                 Doctor_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                 RV_Date = a.RV_Date,
                                 RV_Time = a.RV_Time,
                                 Remarks = a.Remarks,
                                 Delete_flag = a.Delete_flag,
                                 Status = a.Status

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }


    }
}
