//using Microsoft.EntityFrameworkCore;
//using GlobalApi.Data;
//using GlobalApi.GlobalClasses;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Repository.MasterRepository
//{
//    public class SHReferralsRepository : ISHReferrals
//    {
//        GlobalContext db;
//        //public readonly string _connectionString;
//        private IPrimarykeyvalue primarykeyvalue;
//        public SHReferralsRepository(GlobalContext _db)
//        {
//            db = _db;
//            primarykeyvalue = new Primarykeyvalue(_db);
//        }
//        public async Task<SHReferrals> InsertSHReferrals(SHReferrals lead, string? Time, string? date)
//        {
//            try
//            {
//                //var duplicate = await db.SHReferrals.FirstOrDefaultAsync(x => x.SHR_Id == lead.SHR_Id);
//                //if (duplicate == null)
//                //{
//                int id = await primarykeyvalue.primary_key("SHReferrals");
//                SHReferrals obj = new SHReferrals()
//                {
//                    SHR_Id = id,
//                    SHR_Appt_Id_FK = lead.SHR_Appt_Id_FK,
//                    SHR_CON_Id_FK = lead.SHR_CON_Id_FK,
//                    SHR_PR_Id_FK = lead.SHR_PR_Id_FK,
//                    SHR_Ref_D_Id_FK = lead.SHR_Ref_D_Id_FK,
//                    SHR_RH_DoctorRefferdTime = DateTime.Now,
//                    SHR_UserId_FK = lead.SHR_UserId_FK,
//                    Remarks = lead.Remarks,
//                    created_by = 1,
//                    created_date = DateTime.Now,
//                    delete_flag = false,
//                    status = 1
//                };

//                var result = await db.SHReferrals.AddAsync(obj);
//                await db.SaveChangesAsync();
//                await InsertAppointment(obj, Time, date);
//                await InsertComplaint(obj);
//                await InsertSymptoms(obj);
//                await InsertParameters(obj);
//                //await InsertDisease(obj);
//                return result.Entity;
//                //}
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        public async Task<AppointmentModel> InsertAppointment(SHReferrals lead, string? Time, string? date)
//        {
//            try
//            {
//                int _id = await primarykeyvalue.primary_key("PatientAppointment");
//                var list1 = (from a in db.SHReferrals orderby a.SHR_Id descending select a.SHR_Id).FirstOrDefault();
//                var list2 = (from a in db.SHReferrals
//                             join b in db.PatientAppointment on a.SHR_Appt_Id_FK equals b.Appt_Id
//                             where b.Appt_Id == lead.SHR_Appt_Id_FK
//                             //orderby a.SHR_Appt_Id_FK descending
//                             select b.Assi_Id_FK).FirstOrDefault();
//                var list3 = (from a in db.SHReferrals
//                             join b in db.PatientAppointment on a.SHR_Appt_Id_FK equals b.Appt_Id
//                             where b.Appt_Id == lead.SHR_Appt_Id_FK
//                             //orderby a.SHR_Appt_Id_FK descending
//                             select b.Appt_CD_Id_FK).FirstOrDefault();
//                var list4 = (from a in db.SHReferrals
//                             join b in db.PatientAppointment on a.SHR_Appt_Id_FK equals b.Appt_Id
//                             where b.Appt_Id == lead.SHR_Appt_Id_FK
//                             //orderby a.SHR_Appt_Id_FK descending
//                             select b.Dis_id).FirstOrDefault();

//                AppointmentModel obj = new AppointmentModel()
//                {
//                    Appt_Id = _id,
//                    Appt_PatientId_FK = lead.SHR_PR_Id_FK,
//                    Appt_CD_Id_FK = list3,
//                    Appt_DO_Id_FK = lead.SHR_Ref_D_Id_FK,
//                    Select_day = date,
//                    Select_Time = Time,
//                    Doctor_approval_status = 0,
//                    Appt_Is_active = 1,
//                    Appt_Type = "REFERRAL",
//                    Assi_Id_FK = list2,
//                    Ref_Id_FK = list1,
//                    Dis_id = list4,
//                    created_by = 1,
//                    created_date = DateTime.Now,
//                    delete_flag = false,
//                    status = 1
//                };
//                var result = await db.PatientAppointment.AddAsync(obj);
//                await db.SaveChangesAsync();
//                await InsertUsers(lead);
//                await InsertConsultation(lead);
//                return result.Entity;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }
//        public async Task<UsersLists> InsertUsers(SHReferrals lead)
//        {
//            int _id = await primarykeyvalue.primary_key("Users");
//            UsersLists insert = new UsersLists()
//            {
//                Id = _id,
//                User_cat = "PatientAppointment",
//                User_ref_id = lead.SHR_PR_Id_FK,
//            };
//            var _new = await db.UsersLists.AddAsync(insert);
//            await db.SaveChangesAsync();
//            return _new.Entity;

//        }

//        public async Task<Consultation> InsertConsultation(SHReferrals lead)
//        {

//            try
//            {
//                int pkId = await primarykeyvalue.primary_key("Consultation");
//                var list2 = (from a in db.SHReferrals
//                             join b in db.PatientAppointment on a.SHR_Appt_Id_FK equals b.Appt_Id
//                             where b.Appt_Id == lead.SHR_Appt_Id_FK
//                             //orderby a.SHR_Appt_Id_FK descending
//                             select b.Assi_Id_FK).FirstOrDefault();
//                var list3 = (from a in db.PatientAppointment
//                             orderby a.Appt_Id descending
//                             select a.Appt_Id).FirstOrDefault();
//                var list4 = (from a in db.SHReferrals
//                             join b in db.Consultation on a.SHR_PR_Id_FK equals b.CON_PR_Id_FK
//                             where a.SHR_PR_Id_FK == b.CON_PR_Id_FK
//                             //orderby a.SHR_Appt_Id_FK descending
//                             select b.Dis_Id_FK).FirstOrDefault();
//                var hostp = (from a in db.Doctor
//                             where a.DO_Id == lead.SHR_Ref_D_Id_FK
//                             //orderby a.DO_Id ascending
//                             select a.DO_HO_Id_FK).FirstOrDefault();
//                var spec = (from a in db.Doctor
//                            where a.DO_Id == lead.SHR_Ref_D_Id_FK
//                            //orderby a.DO_Id ascending
//                            select a.DO_SP_Id_FK).FirstOrDefault();
//                var cd = (from a in db.Doctor
//                          where a.DO_Id == lead.SHR_Ref_D_Id_FK
//                          //orderby a.DO_Id ascending
//                          select a.DO_CD_Id_FK).FirstOrDefault();

//                Consultation savechanges = new Consultation()
//                {
//                    CON_Id = pkId,
//                    CON_PR_Id_FK = lead.SHR_PR_Id_FK,
//                    CON_DO_Id_FK = lead.SHR_Ref_D_Id_FK,
//                    CON_HO_Id_FK = hostp,
//                    CON_CD_Id_FK = cd,
//                    CON_SP_Id_FK = spec,
//                    CON_Code = pkId <= 09 ? "CON" + '0' + Convert.ToString(pkId) : "CON" + Convert.ToString(pkId),
//                    CON_Type = "REVISIT",
//                    //CON_APPT_Id_FK = lead.SHR_Appt_Id_FK,
//                    CON_APPT_Id_FK = list3,
//                    Dis_Id_FK = list4,
//                    CON_Ref_AS_Id = list2,
//                    Inactive = "N",
//                    delete_flag = false,
//                    status = 1
//                };
//                var _new1 = await db.Consultation.AddAsync(savechanges);
//                await db.SaveChangesAsync();
//                return _new1.Entity;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }

//        }
//        public async Task<Complaint> InsertComplaint(SHReferrals lead)
//        {
//            int _id = await primarykeyvalue.primary_key("Complaint");
//            var list1 = (from a in db.Complaint
//                         join b in db.SHReferrals on a.CPT_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.CPT_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.CPT_Complaint).FirstOrDefault();
//            var list2 = (from a in db.Complaint
//                         join b in db.SHReferrals on a.CPT_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.CPT_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.CPT_UserId_FK).FirstOrDefault();
//            var list3 = (from a in db.PatientAppointment
//                         orderby a.Appt_Id descending
//                         select a.Appt_Id).FirstOrDefault();
//            Complaint insert = new Complaint()
//            {
//                CPT_Id = _id,
//                CPT_Code = _id <= 09 ? "REF" + '0' + Convert.ToString(_id) : "REF" + Convert.ToString(_id),
//                CPT_APPT_Id_FK = list3,
//                CPT_Complaint = list1,
//                CPT_UserId_FK = list2,
//                created_by = 1,
//                created_date = DateTime.Now,
//                delete_flag = false,
//                status = 1,

//            };
//            var _new = await db.Complaint.AddAsync(insert);
//            await db.SaveChangesAsync();
//            return _new.Entity;

//        }

//        public async Task<Symptoms> InsertSymptoms(SHReferrals lead)
//        {
//            int _id = await primarykeyvalue.primary_key("Symptoms");
//            var list1 = (from a in db.Symptoms
//                         join b in db.SHReferrals on a.SYM_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.SYM_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.SYM_Symptoms).FirstOrDefault();
//            var list2 = (from a in db.Symptoms
//                         join b in db.SHReferrals on a.SYM_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.SYM_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.SYM_UserId_FK).FirstOrDefault();
//            var list3 = (from a in db.PatientAppointment
//                         orderby a.Appt_Id descending
//                         select a.Appt_Id).FirstOrDefault();

//            Symptoms insert = new Symptoms()
//            {
//                SYM_Id = _id,
//                SYM_Code = _id <= 09 ? "REF" + '0' + Convert.ToString(_id) : "REF" + Convert.ToString(_id),
//                SYM_APPT_Id_FK = list3,
//                SYM_Symptoms = list1,
//                SYM_UserId_FK = list2,
//                created_by = 1,
//                created_date = DateTime.Now,
//                delete_flag = false,
//                status = 1,
//            };
//            var _new = await db.Symptoms.AddAsync(insert);
//            await db.SaveChangesAsync();
//            return _new.Entity;

//        }
//        public async Task<Parameters> InsertParameters(SHReferrals lead)
//        {
//            int _id = await primarykeyvalue.primary_key("Parameters");
//            var list1 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_Height).FirstOrDefault();
//            var list2 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_Weight).FirstOrDefault();
//            var list3 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_TempInFahrenheit).FirstOrDefault();
//            var list4 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_TempInCelsius).FirstOrDefault();
//            var list5 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_BloodPressure).FirstOrDefault();
//            var list6 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_Sugar).FirstOrDefault();
//            var list7 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_PulseRate).FirstOrDefault();
//            var list8 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_RespiratoryRate).FirstOrDefault();
//            var list9 = (from a in db.Parameters
//                         join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                         where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                         //orderby b.SHR_Id descending
//                         select a.PA_ECG).FirstOrDefault();
//            var list10 = (from a in db.Parameters
//                          join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                          where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                          //orderby b.SHR_Id descending
//                          select a.PA_OxygenSaturation).FirstOrDefault();
//            var list11 = (from a in db.Parameters
//                          join b in db.SHReferrals on a.PA_APPT_Id_FK equals b.SHR_Appt_Id_FK
//                          where a.PA_APPT_Id_FK == lead.SHR_Appt_Id_FK
//                          //orderby b.SHR_Id descending
//                          select a.PA_UserId_FK).FirstOrDefault();
//            var list12 = (from a in db.PatientAppointment
//                          orderby a.Appt_Id descending
//                          select a.Appt_Id).FirstOrDefault();

//            Parameters insert = new Parameters()
//            {
//                PA_Id = _id,
//                PA_Code = _id <= 09 ? "REF" + '0' + Convert.ToString(_id) : "REF" + Convert.ToString(_id),
//                PA_APPT_Id_FK = list12,
//                PA_Height = list1,
//                PA_Weight = list2,
//                PA_TempInFahrenheit = list3,
//                PA_TempInCelsius = list4,
//                PA_BloodPressure = list5,
//                PA_Sugar = list6,
//                PA_PulseRate = list7,
//                PA_RespiratoryRate = list8,
//                PA_ECG = list9,
//                PA_OxygenSaturation = list10,
//                PA_UserId_FK = list11,
//                created_by = 1,
//                created_date = DateTime.Now,
//                delete_flag = false,
//                status = 1,
//            };
//            var _new = await db.Parameters.AddAsync(insert);
//            await db.SaveChangesAsync();
//            return _new.Entity;

//        }
//        public async Task<SHReferrals> UpdateSHReferrals(SHReferrals lead)
//        {
//            try
//            {
//                var result = await db.SHReferrals.FirstOrDefaultAsync(x => x.SHR_Id == lead.SHR_Id);
//                if (result != null)
//                {
//                    result.SHR_Id = lead.SHR_Id;
//                    result.SHR_Appt_Id_FK = lead.SHR_Appt_Id_FK;
//                    result.SHR_CON_Id_FK = lead.SHR_CON_Id_FK;
//                    result.SHR_PR_Id_FK = lead.SHR_PR_Id_FK;
//                    result.SHR_Ref_D_Id_FK = lead.SHR_Ref_D_Id_FK;
//                    result.SHR_RH_DoctorRefferdTime = DateTime.Now.AddDays(1);
//                    result.modified_by = 2;
//                    result.modified_date = DateTime.Now;
//                    result.delete_flag = false;
//                    result.status = 1;
//                    await db.SaveChangesAsync();
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<List<GetAllSHReferrals>> GetAllSHReferrals()
//        {
//            try
//            {
//                if (db != null)
//                {
//                    var query = (from a in db.SHReferrals
//                                 join b in db.Consultation on a.SHR_CON_Id_FK equals b.CON_Id
//                                 join c in db.Patient on a.SHR_PR_Id_FK equals c.PR_Id
//                                 join d in db.Complaint on b.CON_APPT_Id_FK equals d.CPT_APPT_Id_FK
//                                 join e in db.Symptoms on b.CON_APPT_Id_FK equals e.SYM_APPT_Id_FK
//                                 join f in db.Parameters on b.CON_APPT_Id_FK equals f.PA_APPT_Id_FK
//                                 join j in db.Doctor on a.SHR_Ref_D_Id_FK equals j.DO_Id
//                                 join g in db.Discipline on j.DO_CD_Id_FK equals g.CD_Id
//                                 join h in db.Specialization on j.DO_SP_Id_FK equals h.SP_Id
//                                 join i in db.Hospital on j.DO_HO_Id_FK equals i.Hos_Id
//                                 join k in db.Diseases on b.Dis_Id_FK equals k.Id
//                                 orderby a.SHR_Id descending
//                                 select new GetAllSHReferrals
//                                 {
//                                     SHR_Id = a.SHR_Id,
//                                     SHR_Appt_Id_FK = a.SHR_Appt_Id_FK,
//                                     SHR_CON_Id_FK = a.SHR_CON_Id_FK,
//                                     SHR_PR_Id_FK = a.SHR_PR_Id_FK,
//                                     SHR_PR_Name = string.Concat(c.PR_FirstName, c.PR_LastName),
//                                     SHR_CPT_Complaint = d.CPT_Complaint,
//                                     SHR_SYM_Symptoms = e.SYM_Symptoms,
//                                     SHR_Height = f.PA_Height,
//                                     SHR_Weight = f.PA_Weight,
//                                     SHR_TempInFahrenheit = f.PA_TempInFahrenheit,
//                                     SHR_TempInCelsius = f.PA_TempInCelsius,
//                                     SHR_BloodPressure = f.PA_BloodPressure,
//                                     SHR_Sugar = f.PA_Sugar,
//                                     SHR_RespiratoryRate = f.PA_RespiratoryRate,
//                                     SHR_PulseRate = f.PA_PulseRate,
//                                     SHR_ECG = f.PA_ECG,
//                                     SHR_OxygenSaturation = f.PA_OxygenSaturation,
//                                     SHR_Disease_Name = k.Diseases_Name,
//                                     SHR_CD_Id_FK = j.DO_CD_Id_FK,
//                                     SHR_CD_Name = g.CD_ClinicalDiscipline,
//                                     SHR_S_Id_FK = j.DO_SP_Id_FK,
//                                     SHR_S_Specialization = h.SP_Specialization,
//                                     SHR_H_Id_FK = j.DO_HO_Id_FK,
//                                     SHR_H_Name = i.Hos_HospitalName,
//                                     SHR_Ref_D_Id_FK = a.SHR_Ref_D_Id_FK,
//                                     SHR_Ref_D_Name = string.Concat(j.DO_FirstName, j.DO_LastName),
//                                     Remarks = a.Remarks,
//                                     SHR_RH_DoctorRefferdTime = a.SHR_RH_DoctorRefferdTime,
//                                     SHR_UserId_FK = a.SHR_UserId_FK,
//                                     delete_flag = a.delete_flag,
//                                     status = a.status
//                                 });
//                    return await query.ToListAsync();
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<SHReferrals> DeleteSHReferrals(int SHR_Id)
//        {
//            try
//            {
//                var result = await db.SHReferrals.FirstOrDefaultAsync(x => x.SHR_Id == SHR_Id);
//                if (result != null)
//                {
//                    result.SHR_Id = SHR_Id;
//                    result.delete_flag = true;
//                    result.status = 0;
//                    result.deleted_by = 1;
//                    result.deleted_date = DateTime.Now;
//                    await db.SaveChangesAsync();
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<SHReferralsBy_Id> GetSHReferralsById(int SHR_Id)
//        {
//            if (db != null)
//            {
//                var query = (from a in db.SHReferrals
//                             join b in db.Consultation on a.SHR_CON_Id_FK equals b.CON_Id
//                             join c in db.Patient on a.SHR_PR_Id_FK equals c.PR_Id
//                             join d in db.Complaint on b.CON_APPT_Id_FK equals d.CPT_APPT_Id_FK
//                             join e in db.Symptoms on b.CON_APPT_Id_FK equals e.SYM_APPT_Id_FK
//                             join f in db.Parameters on b.CON_APPT_Id_FK equals f.PA_APPT_Id_FK
//                             join j in db.Doctor on a.SHR_Ref_D_Id_FK equals j.DO_Id
//                             join g in db.Discipline on j.DO_CD_Id_FK equals g.CD_Id
//                             join h in db.Specialization on j.DO_SP_Id_FK equals h.SP_Id
//                             join i in db.Hospital on j.DO_HO_Id_FK equals i.Hos_Id
//                             join k in db.Diseases on b.Dis_Id_FK equals k.Id
//                             where a.SHR_Id == SHR_Id
//                             select new SHReferralsBy_Id
//                             {
//                                 SHR_Id = a.SHR_Id,
//                                 SHR_Appt_Id_FK = a.SHR_Appt_Id_FK,
//                                 SHR_CON_Id_FK = a.SHR_CON_Id_FK,
//                                 SHR_PR_Id_FK = a.SHR_PR_Id_FK,
//                                 SHR_PR_Name = string.Concat(c.PR_FirstName, c.PR_LastName),
//                                 SHR_CPT_Complaint = d.CPT_Complaint,
//                                 SHR_SYM_Symptoms = e.SYM_Symptoms,
//                                 SHR_Height = f.PA_Height,
//                                 SHR_Weight = f.PA_Weight,
//                                 SHR_TempInFahrenheit = f.PA_TempInFahrenheit,
//                                 SHR_TempInCelsius = f.PA_TempInCelsius,
//                                 SHR_BloodPressure = f.PA_BloodPressure,
//                                 SHR_Sugar = f.PA_Sugar,
//                                 SHR_RespiratoryRate = f.PA_RespiratoryRate,
//                                 SHR_PulseRate = f.PA_PulseRate,
//                                 SHR_ECG = f.PA_ECG,
//                                 SHR_OxygenSaturation = f.PA_OxygenSaturation,
//                                 SHR_Disease_Name = k.Diseases_Name,
//                                 SHR_CD_Id_FK = j.DO_CD_Id_FK,
//                                 SHR_CD_Name = g.CD_ClinicalDiscipline,
//                                 SHR_S_Id_FK = j.DO_SP_Id_FK,
//                                 SHR_S_Specialization = h.SP_Specialization,
//                                 SHR_H_Id_FK = j.DO_HO_Id_FK,
//                                 SHR_H_Name = i.Hos_HospitalName,
//                                 SHR_Ref_D_Id_FK = a.SHR_Ref_D_Id_FK,
//                                 SHR_Ref_D_Name = string.Concat(j.DO_FirstName, j.DO_LastName),
//                                 Remarks = a.Remarks,
//                                 SHR_RH_DoctorRefferdTime = a.SHR_RH_DoctorRefferdTime,
//                                 SHR_UserId_FK = a.SHR_UserId_FK,
//                                 delete_flag = a.delete_flag,
//                                 status = a.status
//                             }).FirstOrDefaultAsync();
//                return await query;
//            }
//            return null;
//        }

//    }
//}
