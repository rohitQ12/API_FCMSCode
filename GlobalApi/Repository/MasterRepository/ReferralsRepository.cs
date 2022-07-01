using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class ReferralsRepository : IReferrals
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public ReferralsRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Referrals> InsertReferrals(Referrals lead)
        {
            try
            {
                var duplicate = await db.Referrals.FirstOrDefaultAsync(x => x.Ref_Id == lead.Ref_Id);
                var datet = DateTime.Parse(lead.Ref_Date);
                var date = datet.ToString("yyyy-MM-dd");

                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("Referrals");
                    Referrals obj = new Referrals()
                    {
                        Ref_Id = id,
                        CON_Id = lead.CON_Id,
                        DO_Id = lead.DO_Id,
                        Ref_Date = date,
                        SplObs = lead.SplObs,
                        Remarks = lead.Remarks,
                        Created_by = 1,
                        Created_date = DateTime.Now,
                        Delete_flag = false,
                        Status = 1
                    };
                    var result = await db.Referrals.AddAsync(obj);
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
        public async Task<List<GetReferrals>> GetAllReferrals()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Referrals
                                 join b in db.Doctor on a.DO_Id equals b.DO_Id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Status on a.Status equals c.sts_id
                                 orderby a.Ref_Id descending
                                 select new GetReferrals
                                 {
                                     Ref_Id = a.Ref_Id,
                                     CON_Id = a.CON_Id,
                                     DO_Id = a.DO_Id,
                                     DO_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                     Ref_Date = a.Ref_Date,
                                     SplObs = a.SplObs,
                                     Remarks = a.Remarks,
                                     Delete_flag = a.Delete_flag,
                                     Status = a.Status,
                                     sts_name = c.sts_name,
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
        public async Task<Referrals> DeleteReferrals(int Ref_Id)
        {
            try
            {
                var result = await db.Referrals.FirstOrDefaultAsync(x => x.Ref_Id == Ref_Id);
                if (result != null)
                {
                    result.Ref_Id = Ref_Id;
                    result.Deleted_by = 3;
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
        public async Task<GetReferrals> GetReferralsByCON_Id(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Referrals
                             join b in db.Doctor on a.DO_Id equals b.DO_Id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Status on a.Status equals c.sts_id
                             where a.CON_Id == CON_Id
                             select new GetReferrals
                             {
                                 Ref_Id = a.Ref_Id,
                                 CON_Id = a.CON_Id,
                                 DO_Id = a.DO_Id,
                                 DO_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                 Ref_Date = a.Ref_Date,
                                 SplObs = a.SplObs,
                                 Remarks = a.Remarks,
                                 Delete_flag = a.Delete_flag,
                                 Status = a.Status,
                                 sts_name = c.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<GetReferrals> GetReferralsById(int Ref_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Referrals
                             join b in db.Doctor on a.DO_Id equals b.DO_Id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Status on a.Status equals c.sts_id
                             where a.Ref_Id == Ref_Id
                             select new GetReferrals
                             {
                                 Ref_Id = a.Ref_Id,
                                 CON_Id = a.CON_Id,
                                 DO_Id = a.DO_Id,
                                 DO_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                 Ref_Date = a.Ref_Date,
                                 SplObs = a.SplObs,
                                 Remarks = a.Remarks,
                                 Delete_flag = a.Delete_flag,
                                 Status = a.Status,
                                 sts_name = c.sts_name,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        //public async Task<ApprvReferrals> ApproveReferrals(int? AssistantId, string roleaction,ApprvReferrals lead)
        //{
        //    try
        //    {
        //        if(AssistantId != 0)
        //        {
        //            var result = await db.Referrals.Where(x => x.Ref_Id == lead.Ref_Id).FirstOrDefaultAsync();
        //            if (result != null)
        //            {
        //                result.Status = 3;
        //                await db.SaveChangesAsync();
        //                if (result.Status == 3)
        //                {
        //                    int pkId = await primarykeyvalue.primary_key("PatientAppointment");
        //                    var Refrls = await (from a in db.Referrals
        //                                        where a.Ref_Id == lead.Ref_Id
        //                                        select a).FirstOrDefaultAsync();
        //                    var Consltn = await (from b in db.Consultation
        //                                         where b.CON_Id == Refrls.CON_Id
        //                                         select b).FirstOrDefaultAsync();
        //                    var Doc = await (from c in db.Doctor
        //                                     where c.DO_Id == Refrls.DO_Id
        //                                     select c).FirstOrDefaultAsync();
        //                    AppointmentModel apptmod = new AppointmentModel()
        //                    {
        //                        Appt_Id = pkId,
        //                        Appt_PatientId_FK = Consltn.CON_PR_Id_FK,
        //                        CD_Id = Doc.DO_CD_Id_FK,
        //                        Appt_DO_Id_FK = Refrls.DO_Id,
        //                        Appt_DateTime = DateTime.Now,
        //                        Select_day = Refrls.Ref_Date,
        //                        Select_FrmTime = lead.Select_FrmTime,
        //                        Select_toTime = lead.Select_toTime,
        //                        Appt_Is_active = 1,
        //                        Appt_Type = "REFERRALS",
        //                        Assi_Id = AssistantId,
        //                        Ref_Id_FK = lead.Ref_Id,
        //                        created_by = 1,
        //                        created_date = DateTime.Now,
        //                        delete_flag = false,
        //                        status = 1

        //                    };
        //                    var result1 = await db.PatientAppointment.AddAsync(apptmod);
        //                    await db.SaveChangesAsync();

        //                }
        //            }
        //            return null;
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}
        public async Task<ApprvReferrals> ApproveReferrals(ApprvReferrals lead)
        {
            try
            {
                //if (AssistantId != 0)
                //{
                    var result = await db.Referrals.Where(x => x.Ref_Id == lead.Ref_Id).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.Status = 3;
                        await db.SaveChangesAsync();
                        if (result.Status == 3)
                        {
                            int pkId = await primarykeyvalue.primary_key("PatientAppointment");
                            var Refrls = await (from a in db.Referrals
                                                where a.Ref_Id == lead.Ref_Id
                                                select a).FirstOrDefaultAsync();
                            var Consltn = await (from b in db.Consultation
                                                 where b.CON_Id == Refrls.CON_Id
                                                 select b).FirstOrDefaultAsync();
                            var Doc = await (from c in db.Doctor
                                             where c.DO_Id == Refrls.DO_Id
                                             select c).FirstOrDefaultAsync();
                            AppointmentModel apptmod = new AppointmentModel()
                            {
                                Appt_Id = pkId,
                                Appt_PatientId_FK = Consltn.CON_PR_Id_FK,
                                CD_Id = Doc.DO_CD_Id_FK,
                                Appt_DO_Id_FK = Refrls.DO_Id,
                                Appt_DateTime = DateTime.Now,
                                Select_day = Refrls.Ref_Date,
                                Select_FrmTime = lead.Select_FrmTime,
                                Select_toTime = lead.Select_toTime,
                                Appt_Is_active = 1,
                                Appt_Type = "REFERRALS",
                                Assi_Id = 1,
                                Ref_Id_FK = lead.Ref_Id,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                                status = 1

                            };
                            var result1 = await db.PatientAppointment.AddAsync(apptmod);
                            await db.SaveChangesAsync();

                        }
                    }
                    return null;
                //}
                //return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
