//using Microsoft.EntityFrameworkCore;
//using GlobalApi.Data;
//using GlobalApi.GlobalClasses;
//using GlobalApi.IRepository.MasterIRepository;
//using GlobalApi.Models.Master;

//namespace GlobalApi.Repository.MasterRepository
//{
//    public class AppointmentRepository : IAppointment
//    {
//        GlobalContext db;
//        //public readonly string _connectionString;
//        private IPrimarykeyvalue primarykeyvalue;
//        public AppointmentRepository(GlobalContext _db)
//        {
//            db = _db;
//            primarykeyvalue = new Primarykeyvalue(_db);
//        }
//        public async Task<AppointmentModel> InsertAppointment(InsertDetails lead)
//        {
//            try
//            {
//                var duplicate = await db.PatientAppointment.FirstOrDefaultAsync(x => x.Select_Time == lead.Select_Time && x.Select_day == lead.Select_day);
//                if (duplicate == null)
//                {
//                    int id = await primarykeyvalue.primary_key("PatientAppointment");
//                    AppointmentModel obj = new AppointmentModel()
//                    {
//                        Appt_Id = id,
//                        Appt_PatientId_FK = lead.Appt_PatientId_FK,
//                        Appt_CD_Id_FK = lead.Appt_CD_Id_FK,
//                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
//                        //imp
//                        Appt_DateTime = lead.Appt_DateTime,
//                        Select_day = lead.Select_day,
//                        Select_Time = lead.Select_Time,
//                        //Select_FromTime = lead.Select_FromTime,
//                        //Select_ToTime = lead.Select_ToTime,
//                        //status
//                        Doctor_approval_status = 0,
//                        Appt_Is_active = 1,
//                        Appt_Type = "FRESH",
//                        Assi_Id_FK = lead.Assi_Id_FK,
//                        //spe_id = lead.spe_id,
//                        //Hos_id = lead.Hos_id,
//                        Dis_id = lead.Dis_id,
//                        created_by = 1,
//                        created_date = DateTime.Now,
//                        delete_flag = false,
//                        status = 1
//                    };
//                    var result = await db.PatientAppointment.AddAsync(obj);
//                    await db.SaveChangesAsync();
//                    var list1 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();
//                    int _pkid = await primarykeyvalue.primary_key("Complaint");
//                    Complaint obj1 = new Complaint();
//                    obj1.CPT_Id = _pkid;
//                    obj1.CPT_APPT_Id_FK = await list1;
//                    obj1.CPT_Code = lead.cpt_Code;
//                    obj1.CPT_Complaint = lead.cpt;
//                    obj1.CPT_UserId_FK = lead.cpt_userid_fk;
//                    obj1.created_by = 1;
//                    obj1.created_date = DateTime.Now;
//                    obj1.delete_flag = false;
//                    obj1.status = 1;

//                    //var result1 = await db.Complaint.AddAsync(obj1);
//                    //await db.SaveChangesAsync();
//                    int _pkid1 = await primarykeyvalue.primary_key("Symptoms");
//                    Symptoms obj2 = new Symptoms();
//                    obj2.SYM_Id = _pkid1;
//                    obj2.SYM_APPT_Id_FK = await list1;
//                    obj2.SYM_Code = lead.symcode;
//                    obj2.SYM_Symptoms = lead.symptoms;
//                    obj2.SYM_UserId_FK = lead.sym_userid_fk;
//                    obj2.created_by = 1;
//                    obj2.created_date = DateTime.Now;
//                    obj2.delete_flag = false;
//                    obj2.status = 1;
//                    //var result2 = await db.Symptoms.AddAsync(obj2);
//                    //await db.SaveChangesAsync();
//                    int _pkid2 = await primarykeyvalue.primary_key("Parameters");
//                    Parameters obj3 = new Parameters();
//                    obj3.PA_Id = _pkid2;
//                    obj3.PA_APPT_Id_FK = await list1;
//                    obj3.PA_Code = lead.para_code;
//                    obj3.PA_Height = lead.height;
//                    obj3.PA_Weight = lead.weight;
//                    obj3.PA_TempInFahrenheit = lead.TempInFahrenheit;
//                    obj3.PA_TempInCelsius = lead.TempInCelsius;
//                    obj3.PA_BloodPressure = lead.bloodpressure;
//                    obj3.PA_Sugar = lead.sugar;
//                    obj3.PA_ECG = lead.ECG;
//                    obj3.PA_OxygenSaturation = lead.OxygenSaturation;
//                    obj3.PA_PulseRate = lead.pulserate;
//                    obj3.PA_RespiratoryRate = lead.respiratoryrate;
//                    obj3.PA_UserId_FK = lead.para_userid_fk;
//                    obj3.created_by = 1;
//                    obj3.created_date = DateTime.Now;
//                    obj3.delete_flag = false;
//                    obj3.status = 1;
//                    //var result3 = await db.Parameters.AddAsync(obj3);
//                    //await db.SaveChangesAsync();

//                    if (obj1.CPT_Code != null || obj1.CPT_Complaint != null || obj1.CPT_UserId_FK != 0
//                        || obj2.SYM_Code != null || obj2.SYM_Symptoms != null || obj2.SYM_UserId_FK != 0
//                        || obj3.PA_Code != null || obj3.PA_Height != null || obj3.PA_Weight != null
//                        || obj3.PA_TempInFahrenheit != null || obj3.PA_TempInCelsius != null || obj3.PA_BloodPressure != null || obj3.PA_Sugar != null
//                    || obj3.PA_ECG != null || obj3.PA_OxygenSaturation != null || obj3.PA_PulseRate != null
//                    || obj3.PA_RespiratoryRate != null || obj3.PA_UserId_FK != 0)
//                    {
//                        var result1 = await db.Complaint.AddAsync(obj1);
//                        await db.SaveChangesAsync();

//                        var result2 = await db.Symptoms.AddAsync(obj2);
//                        await db.SaveChangesAsync();

//                        var result3 = await db.Parameters.AddAsync(obj3);
//                        await db.SaveChangesAsync();

//                        await InsertUsers(obj);

//                        await InsertConsultation(obj);
//                    }
//                    return result.Entity;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<UsersLists> InsertUsers(AppointmentModel lead)
//        {
//            try
//            {
//                int _id = await primarykeyvalue.primary_key("Users");
//                UsersLists insert = new UsersLists()
//                {
//                    Id = _id,
//                    User_cat = "PatientAppointment",
//                    User_ref_id = lead.Appt_Id,
//                    created_by = 1,
//                    created_date = DateTime.Now,
//                    delete_flag = false,
//                    status = 1,
//                };
//                var _new = await db.UsersLists.AddAsync(insert);
//                await db.SaveChangesAsync();
//                return _new.Entity;

//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }

//        }

//        public async Task<Consultation> InsertConsultation(AppointmentModel lead)
//        {
//            try
//            {
//                int pkId = await primarykeyvalue.primary_key("Consultation");
//                var doct = (from a in db.Doctor
//                            where a.DO_Id == lead.Appt_DO_Id_FK
//                            //orderby a.DO_Id ascending
//                            select a.DO_HO_Id_FK).FirstOrDefault();
//                var spec = (from a in db.Doctor
//                            where a.DO_Id == lead.Appt_DO_Id_FK
//                            //orderby a.DO_Id ascending
//                            select a.DO_SP_Id_FK).FirstOrDefault();
//                Consultation savechanges = new Consultation()
//                {
//                    CON_Id = pkId,
//                    CON_Code = pkId <= 09 ? "CON" + '0' + Convert.ToString(pkId) : "CON" + Convert.ToString(pkId),
//                    CON_Type = lead.Appt_Type,
//                    CON_APPT_Id_FK = lead.Appt_Id,
//                    CON_PR_Id_FK = lead.Appt_PatientId_FK,
//                    CON_DO_Id_FK = lead.Appt_DO_Id_FK,
//                    CON_CD_Id_FK = lead.Appt_CD_Id_FK,
//                    CON_SP_Id_FK = spec,
//                    CON_HO_Id_FK = doct,
//                    Dis_Id_FK = lead.Dis_id,
//                    CON_Ref_AS_Id = lead.Assi_Id_FK,
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


//        public async Task<AppointmentModel> UpdateAppointment(AppointmentModel lead)
//        {
//            try
//            {
//                var result = await db.PatientAppointment.FirstOrDefaultAsync(x => x.Appt_Id == lead.Appt_Id);
//                if (result != null)
//                {
//                    result.Appt_Id = lead.Appt_Id;
//                    result.Appt_PatientId_FK = lead.Appt_PatientId_FK;
//                    result.Appt_CD_Id_FK = lead.Appt_CD_Id_FK;
//                    result.Appt_DO_Id_FK = lead.Appt_DO_Id_FK;
//                    result.Appt_DateTime = lead.Appt_DateTime;
//                    result.Select_day = lead.Select_day;
//                    result.Select_Time = lead.Select_Time;
//                    //result.Select_FromTime = lead.Select_FromTime;
//                    //result.Select_ToTime = lead.Select_ToTime;
//                    //status
//                    result.Doctor_approval_status = 0;
//                    result.Appt_Is_active = 1;
//                    result.Appt_Type = "FRESH";
//                    //result.Ref_Id_FK = 0;
//                    //result.spe_id = lead.spe_id;
//                    //result.Hos_id = lead.Hos_id;
//                    result.Dis_id = lead.Dis_id;
//                    result.modified_by = 2;
//                    result.modified_date = DateTime.Now;
//                    result.delete_flag = false;

//                    result.status = 1;
//                    await db.SaveChangesAsync();
//                    await UpdateConsultation(lead);
//                    return result;
//                }
//                return null;
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//        public async Task<Consultation> UpdateConsultation(AppointmentModel lead)
//        {
//            var result = await db.Consultation.FirstOrDefaultAsync(x => x.CON_Id == lead.Appt_Id);
//            var doct = (from a in db.Doctor
//                        where a.DO_Id == lead.Appt_DO_Id_FK
//                        //orderby a.DO_Id ascending
//                        select a.DO_HO_Id_FK).FirstOrDefault();
//            var spec = (from a in db.Doctor
//                        where a.DO_Id == lead.Appt_DO_Id_FK
//                        //orderby a.DO_Id ascending
//                        select a.DO_SP_Id_FK).FirstOrDefault();
//            if (result != null)
//            {
//                result.CON_Id = lead.Appt_Id;
//                result.CON_Type = lead.Appt_Type;
//                result.CON_APPT_Id_FK = lead.Appt_Id;
//                result.CON_PR_Id_FK = lead.Appt_PatientId_FK;
//                result.CON_DO_Id_FK = lead.Appt_DO_Id_FK;
//                result.CON_CD_Id_FK = lead.Appt_CD_Id_FK;
//                result.CON_Ref_AS_Id = lead.Assi_Id_FK;
//                result.CON_SP_Id_FK = spec;
//                result.CON_HO_Id_FK = doct;
//                result.Dis_Id_FK = lead.Dis_id;
//                result.Inactive = "N";
//                result.modified_by = 2;
//                result.modified_date = DateTime.Now;
//                result.delete_flag = false;
//                result.status = 1;
//                await db.SaveChangesAsync();
//                return result;

//            }
//            return null;

//        }

//        public async Task<List<GetAllAppointmentModel>> GetAllAppointment()
//        {
//            //string selectdate = "28-02-2022";
//            //DateTime seldate = Convert.ToDateTime(selectdate); 
//            //string s = seldate.DayOfWeek.ToString();
//            try
//            {
//                if (db != null)
//                {
//                    var query = (from a in db.PatientAppointment
//                                 join b in db.Patient on a.Appt_PatientId_FK equals b.PR_Id
//                                 join c in db.Discipline on a.Appt_CD_Id_FK equals c.CD_Id
//                                 join d in db.Doctor on a.Appt_DO_Id_FK equals d.DO_Id
//                                 join e in db.Complaint on a.Appt_Id equals e.CPT_APPT_Id_FK
//                                 join f in db.Symptoms on a.Appt_Id equals f.SYM_APPT_Id_FK
//                                 join g in db.Parameters on a.Appt_Id equals g.PA_APPT_Id_FK
//                                 join h in db.Assistant on a.Assi_Id_FK equals h.Assi_Id
//                                 orderby a.Appt_Id descending
//                                 select new GetAllAppointmentModel
//                                 {
//                                     Appt_Id = a.Appt_Id,
//                                     Appt_PatientId_FK = a.Appt_PatientId_FK,
//                                     Appt_P_Code = b.PR_PatientCode,
//                                     Appt_P_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
//                                     Appt_CPT_Name = e.CPT_Complaint,
//                                     Appt_SYM_Name = f.SYM_Symptoms,
//                                     Appt_PA_Height = g.PA_Height,
//                                     Appt_PA_Weight = g.PA_Weight,
//                                     Appt_PA_TempInFahrenheit = g.PA_TempInFahrenheit,
//                                     Appt_PA_TempInCelsius = g.PA_TempInCelsius,
//                                     Appt_PA_BloodPressure = g.PA_BloodPressure,
//                                     Appt_PA_Sugar = g.PA_Sugar,
//                                     Appt_PA_RespiratoryRate = g.PA_RespiratoryRate,
//                                     Appt_PA_PulseRate = g.PA_PulseRate,
//                                     Appt_PA_ECG = g.PA_ECG,
//                                     Appt_PA_OxygenSaturation = g.PA_OxygenSaturation,
//                                     Appt_CD_Id_FK = a.Appt_CD_Id_FK,
//                                     Appt_CD_Name = c.CD_ClinicalDiscipline,
//                                     Appt_DO_Id_FK = a.Appt_DO_Id_FK,
//                                     Appt_DO_Name = string.Concat(d.DO_FirstName, d.DO_LastName),
//                                     Appt_DateTime = a.Appt_DateTime,
//                                     Select_day = Convert.ToString(Convert.ToDateTime(a.Select_day).DayOfWeek),
//                                     Select_Time = a.Select_Time,
//                                     //Select_FromTime = a.Select_FromTime,
//                                     //Select_ToTime = a.Select_ToTime,
//                                     Doctor_approval_status = a.Doctor_approval_status,
//                                     Appt_Is_active = a.Appt_Is_active,
//                                     Appt_Type = a.Appt_Type,
//                                     Assi_Id_FK = a.Assi_Id_FK,
//                                     Appt_Assi_Name = string.Concat(h.Assi_FirstName, h.Assi_LastName),
//                                     Ref_Id_FK = a.Ref_Id_FK,
//                                     Dis_id = a.Dis_id,
//                                     delete_flag = a.delete_flag,
//                                     status = a.status,

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

//        public async Task<AppointmentModel> DeleteAppointment(int Appt_Id)
//        {
//            try
//            {
//                var result = await db.PatientAppointment.FirstOrDefaultAsync(x => x.Appt_Id == Appt_Id);
//                if (result != null)
//                {
//                    result.Appt_Id = Appt_Id;
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
//        public async Task<AppointmentModelById> GetAppointmentById(int Appt_Id)
//        {
//            if (db != null)
//            {
//                var query = (from a in db.PatientAppointment
//                             join b in db.Patient on a.Appt_PatientId_FK equals b.PR_Id
//                             join c in db.Discipline on a.Appt_CD_Id_FK equals c.CD_Id
//                             join d in db.Doctor on a.Appt_DO_Id_FK equals d.DO_Id
//                             join e in db.Complaint on a.Appt_Id equals e.CPT_APPT_Id_FK
//                             join f in db.Symptoms on a.Appt_Id equals f.SYM_APPT_Id_FK
//                             join g in db.Parameters on a.Appt_Id equals g.PA_APPT_Id_FK
//                             join h in db.Assistant on a.Assi_Id_FK equals h.Assi_Id
//                             where a.Appt_Id == Appt_Id
//                             select new AppointmentModelById
//                             {
//                                 Appt_Id = a.Appt_Id,
//                                 Appt_PatientId_FK = a.Appt_PatientId_FK,
//                                 Appt_P_Code = b.PR_PatientCode,
//                                 Appt_P_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
//                                 Appt_CPT_Name = e.CPT_Complaint,
//                                 Appt_SYM_Name = f.SYM_Symptoms,
//                                 Appt_PA_Height = g.PA_Height,
//                                 Appt_PA_Weight = g.PA_Weight,
//                                 Appt_PA_TempInFahrenheit = g.PA_TempInFahrenheit,
//                                 Appt_PA_TempInCelsius = g.PA_TempInCelsius,
//                                 Appt_PA_BloodPressure = g.PA_BloodPressure,
//                                 Appt_PA_Sugar = g.PA_Sugar,
//                                 Appt_PA_RespiratoryRate = g.PA_RespiratoryRate,
//                                 Appt_PA_PulseRate = g.PA_PulseRate,
//                                 Appt_PA_ECG = g.PA_ECG,
//                                 Appt_PA_OxygenSaturation = g.PA_OxygenSaturation,
//                                 Appt_CD_Id_FK = a.Appt_CD_Id_FK,
//                                 Appt_CD_Name = c.CD_ClinicalDiscipline,
//                                 Appt_DO_Id_FK = a.Appt_DO_Id_FK,
//                                 Appt_DO_Name = string.Concat(d.DO_FirstName, d.DO_LastName),
//                                 Appt_DateTime = a.Appt_DateTime,
//                                 Select_day = Convert.ToString(Convert.ToDateTime(a.Select_day).DayOfWeek),
//                                 Select_Time = a.Select_Time,
//                                 //Select_FromTime = a.Select_FromTime,
//                                 //Select_ToTime = a.Select_ToTime,
//                                 Doctor_approval_status = a.Doctor_approval_status,
//                                 Appt_Is_active = a.Appt_Is_active,
//                                 Appt_Type = a.Appt_Type,
//                                 Assi_Id_FK = a.Assi_Id_FK,
//                                 Appt_Assi_Name = string.Concat(h.Assi_FirstName, h.Assi_LastName),
//                                 Ref_Id_FK = a.Ref_Id_FK,
//                                 Dis_id = a.Dis_id,
//                                 delete_flag = a.delete_flag,
//                                 status = a.status,

//                             }).FirstOrDefaultAsync();
//                return await query;
//            }
//            return null;
//        }
//        public async Task<List<GetDocDD>> GetDoctorDD()
//        {
//            if (db != null)
//            {
//                var query = (from a in db.Doctor
//                             //join b in db.Doctor_Schedules on a.DO_Id equals b.DO_Id_FK
//                             where a.delete_flag == false && a.status == 1
//                             select new GetDocDD
//                             {
//                                 Doc_Id = a.DO_Id,
//                                 Doc_code = a.DO_Code,
//                                 Doc_Name = string.Concat(a.DO_FirstName, a.DO_LastName),
//                             }).ToListAsync();
//                return await query;
//            }
//            return null;
//        }
//    }
//}
