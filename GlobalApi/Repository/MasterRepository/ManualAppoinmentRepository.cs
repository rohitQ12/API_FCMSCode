using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GlobalApi.Repository.MasterRepository
{
    public class ManualAppoinmentRepository : IManualAppointment
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private ComplaintRepository complaintRepository;
        private SymptomsRepository symptomsRepository;
        private DiseasesDtlRepository diseasesDtlRepository;
        private AllergySigns_DTLRepository allergySigns_DTLRepository;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly NotificationRepository notificationRepository;
        //public readonly FindUserId findUserId;

        public ManualAppoinmentRepository()
        {
            this.db = new GlobalContext();
            ado_Configurations = new ADO_Configrations();
            this.complaintRepository = new ComplaintRepository();
            this.symptomsRepository = new SymptomsRepository();
            this.diseasesDtlRepository = new DiseasesDtlRepository();
            this.allergySigns_DTLRepository = new AllergySigns_DTLRepository();
            //this.patientDocumentRepository = new PatientDocumentRepository();
            primarykeyvalue = new Primarykeyvalue();
            notificationRepository = new NotificationRepository();
            //this.findUserId = new FindUserId();
        }
        public async Task<ManualAppointment> InsertAppointment(InsertManualApptDetails lead, int Appt_PatientId, string UserId)
        {

            try
            {
                var b = (from a in db.ManualAppointment
                         where a.Appt_PatientId_FK == lead.Appt_PatientId_FK
                         select a.Appt_PatientId_FK).FirstOrDefault();
                var PatientName = db.Patient.SingleOrDefault(x => x.PR_Id == Appt_PatientId);
                var DoctorName = db.Doctor.SingleOrDefault(x => x.DO_Id == lead.Appt_DO_Id_FK);

                var DoctorDetails = await db.Doctor.FirstOrDefaultAsync(x => x.DO_Id == lead.Appt_DO_Id_FK);
                if (b == null)
                {
                    int id = await primarykeyvalue.primary_key("ManualAppointment");
                    ManualAppointment obj = new ManualAppointment()
                    {
                        MAppt_Id = id,
                        Appt_PatientId_FK = Appt_PatientId,
                        CD_Id = lead.CD_Id != null ? lead.CD_Id : 0,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK != null ? lead.Appt_DO_Id_FK : 0,
                        Hos_Id = lead.Hos_Id,
                        Appt_DateTime = DateTime.Now,
                        Select_day = lead.Select_day,
                        Select_FrmTime = DateTime.ParseExact(lead.Select_FrmTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Select_toTime = DateTime.ParseExact(lead.Select_toTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Appt_Is_active = 1,
                        Appt_Type = "FRESH",
                        Assi_Id = lead.Assi_Id != null ? lead.Assi_Id : 0,
                        UnderBPMedication = lead.UnderBPMedication,
                        UnderSugarMedication = lead.UnderSugarMedication,
                        //Ref_Id_FK = lead.Ref_Id_FK != null ? lead.Ref_Id_FK : 0,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.ManualAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var COMPT = await complaintRepository.InsertManualComplaint(lead.Complaint, id);
                    var SYMPT = await symptomsRepository.InsertManualSymptoms(lead.Symptoms, id);
                    var DDTL = await diseasesDtlRepository.InsertManualDiseasesDtl(lead.DiseasesDtl, id);
                    var AL = await allergySigns_DTLRepository.InsertManualAllergySigns_DTL(lead.AllergySigns_DTL, id);
                    int _pkid2 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj3 = new Parameters();
                    obj3.PA_Id = _pkid2;
                    obj3.MAppt_Id = id;
                    obj3.PA_Code = _pkid2 <= 09 ? "PA" + '0' + Convert.ToString(_pkid2) : "PA" + Convert.ToString(_pkid2);
                    obj3.PA_Height = lead.Height;
                    obj3.PA_Weight = lead.Weight;
                    obj3.PA_TempInFahrenheit = lead.TempInFahrenheit;
                    obj3.PA_TempInCelsius = lead.TempInCelsius;
                    obj3.PA_BloodPressure = lead.BloodPressure;
                    obj3.PA_Sugar = lead.Sugar;
                    obj3.PA_ECG = lead.ECG;
                    obj3.PA_OxygenSaturation = lead.OxygenSaturation;
                    obj3.PA_PulseRate = lead.PulseRate;
                    obj3.PA_RespiratoryRate = lead.RespiratoryRate;
                    obj3.PA_UserId_FK = lead.UserId_FK;
                    obj3.created_by = 1;
                    obj3.created_date = DateTime.Now;
                    obj3.delete_flag = false;
                    obj3.status = 1;

                    var result1 = await db.Parameters.AddAsync(obj3);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);

                    var NotificationSendToPatient = await notificationRepository.InsertNotification("New Appointment fixed with DR" + DoctorName.DO_FirstName, "Your Appointment fix at " + Convert.ToString(DateTime.Now), true, UserId);
                    var NotificationSendToDoctor = await notificationRepository.InsertNotification("New Appointment fixed with Patient" + PatientName.PR_FirstName, "Your Appointment fix at " + Convert.ToString(DateTime.Now), true, DoctorDetails.UserId);
                    return result.Entity;

                }
                else
                {
                    int id = await primarykeyvalue.primary_key("ManualAppointment");
                    ManualAppointment obj = new ManualAppointment()
                    {
                        MAppt_Id = id,
                        Appt_PatientId_FK = lead.Appt_PatientId_FK,
                        CD_Id = lead.CD_Id != null ? lead.CD_Id : 0,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK != null ? lead.Appt_DO_Id_FK : 0,
                        Hos_Id = lead.Hos_Id,
                        Appt_DateTime = DateTime.Now,
                        Select_day = lead.Select_day,
                        Select_FrmTime = DateTime.ParseExact(lead.Select_FrmTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Select_toTime = DateTime.ParseExact(lead.Select_toTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Appt_Is_active = 1,
                        Appt_Type = "REVISIT",
                        Assi_Id = lead.Assi_Id != null ? lead.Assi_Id : 0,
                        UnderBPMedication = lead.UnderBPMedication,
                        UnderSugarMedication = lead.UnderSugarMedication,
                        //Ref_Id_FK = lead.Ref_Id_FK != null ? lead.Ref_Id_FK : 0,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.ManualAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var COMPT = await complaintRepository.InsertManualComplaint(lead.Complaint, id);
                    var SYMPT = await symptomsRepository.InsertManualSymptoms(lead.Symptoms, id);
                    var DDTL = await diseasesDtlRepository.InsertManualDiseasesDtl(lead.DiseasesDtl, id);
                    var AL = await allergySigns_DTLRepository.InsertManualAllergySigns_DTL(lead.AllergySigns_DTL, id);
                    int _pkid3 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj4 = new Parameters();
                    obj4.PA_Id = _pkid3;
                    obj4.MAppt_Id = id;
                    obj4.PA_Code = _pkid3 <= 09 ? "PA" + '0' + Convert.ToString(_pkid3) : "PA" + Convert.ToString(_pkid3);
                    obj4.PA_Height = lead.Height;
                    obj4.PA_Weight = lead.Weight;
                    obj4.PA_TempInFahrenheit = lead.TempInFahrenheit;
                    obj4.PA_TempInCelsius = lead.TempInCelsius;
                    obj4.PA_BloodPressure = lead.BloodPressure;
                    obj4.PA_Sugar = lead.Sugar;
                    obj4.PA_ECG = lead.ECG;
                    obj4.PA_OxygenSaturation = lead.OxygenSaturation;
                    obj4.PA_PulseRate = lead.PulseRate;
                    obj4.PA_RespiratoryRate = lead.RespiratoryRate;
                    obj4.PA_UserId_FK = lead.UserId_FK;
                    obj4.created_by = 1;
                    obj4.created_date = DateTime.Now;
                    obj4.delete_flag = false;
                    obj4.status = 1;
                    var result1 = await db.Parameters.AddAsync(obj4);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    var NotificationSendToPatient = await notificationRepository.InsertNotification("Revisit Appointment fixed with DR" + DoctorName, "Your Appointment fix at" + Convert.ToString(DateTime.Now), true, UserId);
                    var NotificationSendToDoctor = await notificationRepository.InsertNotification("Revisit Appointment fixed with Patient" + PatientName, "Your Appointment fix at" + Convert.ToString(DateTime.Now), true, DoctorDetails.UserId);
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<UsersLists> InsertUsers(ManualAppointment lead)
        {
            try
            {
                int _id = await primarykeyvalue.primary_key("UsersLists");
                UsersLists insert = new UsersLists()
                {
                    Id = _id,
                    User_cat = "ManualAppointment",
                    User_ref_id = lead.MAppt_Id,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1,
                };
                var _new = await db.UsersLists.AddAsync(insert);
                await db.SaveChangesAsync();
                return _new.Entity;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<ManualAppointment> ApproveAppointment(int MAppt_Id)
        {
            try
            {
                var result = await db.ManualAppointment.Where(x => x.MAppt_Id == MAppt_Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.MAppt_Id = MAppt_Id;
                    result.status = 3;
                    await db.SaveChangesAsync();
                    if (result.status == 3)
                    {
                        int pkId = await primarykeyvalue.primary_key("Consultation");
                        var spec = (from a in db.Doctor
                                    where a.DO_Id == result.Appt_DO_Id_FK
                                    select a.DO_SP_Id_FK).FirstOrDefault();
                        Consultation savechanges = new Consultation()
                        {
                            CON_Id = pkId,
                            CON_Code = pkId <= 09 ? "CON" + '0' + Convert.ToString(pkId) : "CON" + Convert.ToString(pkId),
                            CON_Type = result.Appt_Type,
                            Phc_ApptId = result.MAppt_Id,
                            CON_PR_Id_FK = result.Appt_PatientId_FK,
                            CON_DO_Id_FK = result.Appt_DO_Id_FK,
                            CON_CD_Id_FK = result.CD_Id,
                            CON_SP_Id_FK = spec,
                            CON_HO_Id_FK = result.Hos_Id,
                            CON_Ref_AS_Id = result.Assi_Id != null ? result.Assi_Id : 0,
                            Inactive = "N",
                            delete_flag = false,
                            status = 1,
                        };
                        var _new1 = await db.Consultation.AddAsync(savechanges);
                        await db.SaveChangesAsync();
                    }
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<ManualAppointment> RejectAppointment(int MAppt_Id)
        {
            try
            {
                var result = await db.ManualAppointment.FirstOrDefaultAsync(x => x.MAppt_Id == MAppt_Id);
                if (result != null)
                {
                    result.MAppt_Id = MAppt_Id;
                    result.delete_flag = true;
                    result.deleted_by = 3;
                    result.deleted_date = DateTime.Now;
                    result.status = 7;
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
        public async Task<string> UpdateAppointment(InsertManualApptDetails lead)
        {
            try
            {
                if (lead.status != 3)
                {
                    var result = await db.ManualAppointment.FirstOrDefaultAsync(x => x.MAppt_Id == lead.MAppt_Id);
                    if (result != null)
                    {
                        result.MAppt_Id = lead.MAppt_Id;
                        result.Appt_PatientId_FK = lead.Appt_PatientId_FK;
                        result.CD_Id = lead.CD_Id;
                        result.Appt_DO_Id_FK = lead.Appt_DO_Id_FK;
                        result.Hos_Id = lead.Hos_Id;
                        result.Appt_DateTime = lead.Appt_DateTime;
                        result.Select_day = lead.Select_day;
                        result.Select_FrmTime = lead.Select_FrmTime;
                        result.Select_toTime = lead.Select_toTime;
                        result.Appt_Is_active = 1;
                        result.Appt_Type = "FRESH";
                        result.Assi_Id = lead.Assi_Id;
                        result.UnderBPMedication = lead.UnderBPMedication;
                        result.UnderSugarMedication = lead.UnderSugarMedication;
                        result.modified_by = 2;
                        result.modified_date = DateTime.Now;
                        result.delete_flag = false;
                        result.status = 2;
                        await db.SaveChangesAsync();
                        var COMPT = await complaintRepository.UpdateComplainttest(lead.Complaint, lead.MAppt_Id);
                        var SYMPT = await symptomsRepository.UpdateManualSymptoms(lead.Symptoms, lead.MAppt_Id);
                        var DDTL = await diseasesDtlRepository.UpdateManualDiseasesDtl(lead.DiseasesDtl, lead.MAppt_Id);
                        var AL = await allergySigns_DTLRepository.UpdateManualAllergySigns_DTL(lead.AllergySigns_DTL, lead.MAppt_Id);
                        await UpdateParameters(lead);
                        return "Record Updated successfully";

                    }
                    return "Appointment Not Found";
                }
                else
                    return "Cannot Update Approved Appointment";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Parameters> UpdateParameters(InsertManualApptDetails lead)
        {
            try
            {
                var result = await db.Parameters.FirstOrDefaultAsync(x => x.Appt_Id == lead.MAppt_Id);
                var list = (from a in db.Parameters where a.Appt_Id == lead.MAppt_Id select a.PA_Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.PA_Id = await list;
                    //result.PA_Code = lead.PA_Code;
                    result.MAppt_Id = lead.MAppt_Id;
                    result.PA_Height = lead.Height;
                    result.PA_Weight = lead.Weight;
                    result.PA_TempInFahrenheit = lead.TempInFahrenheit;
                    result.PA_TempInCelsius = lead.TempInCelsius;
                    result.PA_BloodPressure = lead.BloodPressure;
                    result.PA_Sugar = lead.Sugar;
                    result.PA_PulseRate = lead.PulseRate;
                    result.PA_RespiratoryRate = lead.RespiratoryRate;
                    result.PA_ECG = lead.ECG;
                    result.PA_OxygenSaturation = lead.OxygenSaturation;
                    result.PA_UserId_FK = lead.UserId_FK;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
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
        public async Task<List<GetAllManualAppointment>> GetAllAppointment()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.ManualAppointment
                                 join b in db.Patient on a.Appt_PatientId_FK equals b.PR_Id
                                 join c in db.Discipline on a.CD_Id equals c.CD_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Doctor on a.Appt_DO_Id_FK equals d.DO_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join r in db.Hospital on a.Hos_Id equals r.Hos_Id into rlist 
                                 from r in rlist.DefaultIfEmpty()
                                 join e in db.Parameters on a.MAppt_Id equals e.Appt_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Assistant on a.Assi_Id equals f.Assi_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join n in db.Status on a.status equals n.sts_id into nlist
                                 from n in nlist.DefaultIfEmpty()
                                 join o in db.States on b.PR_S_Id_FK equals o.stat_id into olist
                                 from o in olist.DefaultIfEmpty()
                                 join m in db.Districts on b.PR_D_Id_FK equals m.district_id into mlist
                                 from m in mlist.DefaultIfEmpty()
                                 orderby a.MAppt_Id descending
                                 select new GetAllManualAppointment()
                                 {
                                     MAppt_Id = a.MAppt_Id,
                                     Appt_PatientId_FK = a.Appt_PatientId_FK,
                                     Appt_P_Code = b.PR_PatientCode,
                                     Appt_P_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     PR_Photobyte = System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo),
                                     PatientLocation = m.district_name,
                                     complaintslist = (from g in db.Complaint
                                                       join h in db.ComplaintMst on g.Cmst_Id equals h.Cmst_Id
                                                       where g.MAppt_Id == a.MAppt_Id
                                                       select new GetAllComplaint()
                                                       {
                                                           Cmst_Id = g.Cmst_Id,
                                                           Cmst_Name = h.Cmst_Name,

                                                       }).ToList(),
                                     symptomslist = (from i in db.Symptoms
                                                     join j in db.SymptomsMst on i.Smst_Id equals j.Smst_Id
                                                     where i.MAppt_Id == a.MAppt_Id
                                                     select new GetAllSymptoms()
                                                     {
                                                         Smst_Id = i.Smst_Id,
                                                         Smst_Name = j.Smst_Name,

                                                     }).ToList(),
                                     diseaseslist = (from k in db.DiseasesDtl
                                                     join l in db.Diseases on k.Id equals l.Id
                                                     where k.MAppt_Id == a.MAppt_Id
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         Id = k.Id,
                                                         Diseases_Name = l.Diseases_Name,

                                                     }).ToList(),
                                     Allergylist = (from p in db.AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.MAppt_Id == a.MAppt_Id
                                                    select new GetAllAllergySigns_DTL()
                                                    {
                                                        //Ddtl_Id = k.Ddtl_Id,
                                                        Al_Id = p.Al_Id,
                                                        Al_Name = q.Al_Name,
                                                        //Ddtl_APPT_Id_FK = k.Ddtl_APPT_Id_FK,
                                                        //Remarks = k.Remarks,
                                                        //delete_flag = k.delete_flag,
                                                    }).ToList(),
                                     UnderBPMedication = a.UnderBPMedication,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     Appt_PA_Height = e.PA_Height,
                                     Appt_PA_Weight = e.PA_Weight,
                                     Appt_PA_TempInFahrenheit = e.PA_TempInFahrenheit,
                                     Appt_PA_TempInCelsius = e.PA_TempInCelsius,
                                     Appt_PA_BloodPressure = e.PA_BloodPressure,
                                     Appt_PA_Sugar = e.PA_Sugar,
                                     Appt_PA_RespiratoryRate = e.PA_RespiratoryRate,
                                     Appt_PA_PulseRate = e.PA_PulseRate,
                                     Appt_PA_ECG = e.PA_ECG,
                                     Appt_PA_OxygenSaturation = e.PA_OxygenSaturation,
                                     CD_Id = a.CD_Id,
                                     CD_Name = c.CD_ClinicalDiscipline,
                                     Appt_DO_Id_FK = a.Appt_DO_Id_FK,
                                     Appt_DO_Name = string.Concat(d.DO_FirstName, d.DO_LastName),
                                     Hos_Id = a.Hos_Id,
                                     Hos_HospitalName = r.Hos_HospitalName,
                                     Appt_DateTime = a.Appt_DateTime,
                                     Select_day = Convert.ToString(Convert.ToDateTime(a.Select_day).DayOfWeek),
                                     Select_FrmTime = a.Select_FrmTime,
                                     Select_toTime = a.Select_toTime,
                                     Appt_Is_active = a.Appt_Is_active,
                                     Appt_Type = a.Appt_Type,
                                     Assi_Id = a.Assi_Id,
                                     Appt_Assi_Name = string.Concat(f.Assi_FirstName, f.Assi_LastName),
                                     Ref_Id_FK = a.Ref_Id_FK,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     status_name = n.sts_name,

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
        public async Task<ManualAppointment> DeleteAppointment(int MAppt_Id)
        {
            try
            {
                var result = await db.ManualAppointment.FirstOrDefaultAsync(x => x.MAppt_Id == MAppt_Id);
                if (result != null)
                {
                    result.MAppt_Id = MAppt_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
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
        public async Task<List<ManualAppointmentById>> GetAdminAppointmentById(int MAppt_Id)
        {
            if (db != null)
            {
                var query = (from a in db.ManualAppointment
                             join b in db.Patient on a.Appt_PatientId_FK equals b.PR_Id
                             join c in db.Discipline on a.CD_Id equals c.CD_Id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Doctor on a.Appt_DO_Id_FK equals d.DO_Id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join r in db.Hospital on a.Hos_Id equals r.Hos_Id into rlist
                             from r in rlist.DefaultIfEmpty()
                             join e in db.Parameters on a.MAppt_Id equals e.Appt_Id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Assistant on a.Assi_Id equals f.Assi_Id into flist
                             from f in flist.DefaultIfEmpty()
                             join n in db.Status on a.status equals n.sts_id into nlist
                             from n in nlist.DefaultIfEmpty()
                             join o in db.States on b.PR_S_Id_FK equals o.stat_id into olist
                             from o in olist.DefaultIfEmpty()
                             join m in db.Districts on b.PR_D_Id_FK equals m.district_id into mlist
                             from m in mlist.DefaultIfEmpty()
                             where a.MAppt_Id == MAppt_Id
                             orderby a.MAppt_Id descending
                             select new ManualAppointmentById()
                             {
                                 MAppt_Id = a.MAppt_Id,
                                 Appt_PatientId_FK = a.Appt_PatientId_FK,
                                 Appt_P_Code = b.PR_PatientCode,
                                 Appt_P_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                 PR_Photobyte = System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo),
                                 PatientLocation = m.district_name,
                                 complaintslist = (from g in db.Complaint
                                                   join h in db.ComplaintMst on g.Cmst_Id equals h.Cmst_Id
                                                   where g.MAppt_Id == a.MAppt_Id
                                                   select new GetAllComplaint()
                                                   {
                                                       Cmst_Id = g.Cmst_Id,
                                                       Cmst_Name = h.Cmst_Name,
                                                   }).ToList(),
                                 symptomslist = (from i in db.Symptoms
                                                 join j in db.SymptomsMst on i.Smst_Id equals j.Smst_Id
                                                 where i.MAppt_Id == a.MAppt_Id
                                                 select new GetAllSymptoms()
                                                 {
                                                     Smst_Id = i.Smst_Id,
                                                     Smst_Name = j.Smst_Name,
                                                 }).ToList(),
                                 diseaseslist = (from k in db.DiseasesDtl
                                                 join l in db.Diseases on k.Id equals l.Id
                                                 where k.MAppt_Id == a.MAppt_Id
                                                 select new GetAllDiseasesDtl()
                                                 {
                                                     Id = k.Id,
                                                     Diseases_Name = l.Diseases_Name,
                                                 }).ToList(),
                                 Allergylist = (from p in db.AllergySigns_DTL
                                                join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                where p.MAppt_Id == a.MAppt_Id
                                                select new GetAllAllergySigns_DTL()
                                                {
                                                    //Ddtl_Id = k.Ddtl_Id,
                                                    Al_Id = p.Al_Id,
                                                    Al_Name = q.Al_Name,
                                                    //Ddtl_APPT_Id_FK = k.Ddtl_APPT_Id_FK,
                                                    //Remarks = k.Remarks,
                                                    //delete_flag = k.delete_flag,
                                                }).ToList(),
                                 UnderBPMedication = a.UnderBPMedication,
                                 UnderSugarMedication = a.UnderSugarMedication,
                                 Appt_PA_Height = e.PA_Height,
                                 Appt_PA_Weight = e.PA_Weight,
                                 Appt_PA_TempInFahrenheit = e.PA_TempInFahrenheit,
                                 Appt_PA_TempInCelsius = e.PA_TempInCelsius,
                                 Appt_PA_BloodPressure = e.PA_BloodPressure,
                                 Appt_PA_Sugar = e.PA_Sugar,
                                 Appt_PA_RespiratoryRate = e.PA_RespiratoryRate,
                                 Appt_PA_PulseRate = e.PA_PulseRate,
                                 Appt_PA_ECG = e.PA_ECG,
                                 Appt_PA_OxygenSaturation = e.PA_OxygenSaturation,
                                 CD_Id = a.CD_Id,
                                 CD_Name = c.CD_ClinicalDiscipline,
                                 Appt_DO_Id_FK = a.Appt_DO_Id_FK,
                                 Appt_DO_Name = string.Concat(d.DO_FirstName, d.DO_LastName),
                                 Hos_Id = a.Hos_Id,
                                 Hos_HospitalName = r.Hos_HospitalName,
                                 Appt_DateTime = a.Appt_DateTime,
                                 Select_day = Convert.ToString(Convert.ToDateTime(a.Select_day).DayOfWeek),
                                 Select_FrmTime = a.Select_FrmTime,
                                 Select_toTime = a.Select_toTime,
                                 Appt_Is_active = a.Appt_Is_active,
                                 Appt_Type = a.Appt_Type,
                                 Assi_Id = a.Assi_Id,
                                 Appt_Assi_Name = string.Concat(f.Assi_FirstName, f.Assi_LastName),
                                 Ref_Id_FK = a.Ref_Id_FK,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 status_name = n.sts_name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        //public async Task<List<GetHosDD>> GetHospital_DD(int PR_Id)
        //{
        //    if (db != null)
        //    {
        //        //var PsCode = (from b in db.Patient where b.PR_Id == PR_Id select b.PR_Postalcode).FirstOrDefault();
        //        var query = (from a in db.Hospital
        //                     where a.delete_flag == false && a.status != 6 && a.Hos_Id != 0
        //                     //&& a.Hos_PostalCode == PsCode
        //                     select new GetHosDD
        //                     {
        //                         Hos_Id = a.Hos_Id,
        //                         Hos_HospitalName = a.Hos_HospitalName,

        //                     }).ToListAsync();
        //        return await query;
        //    }
        //    return null;
        //}
        public async Task<List<GetHosDD>> GetHospital_DD()
        {
            if (db != null)
            {
                //var PsCode = (from b in db.Patient where b.PR_Id == PR_Id select b.PR_Postalcode).FirstOrDefault();
                var query = (from a in db.Hospital
                             where a.delete_flag == false && a.status != 6 && a.Hos_Id != 0
                             //&& a.Hos_PostalCode == PsCode
                             select new GetHosDD
                             {
                                 Hos_Id = a.Hos_Id,
                                 Hos_HospitalName = a.Hos_HospitalName,

                             }).ToListAsync();
                return await query;
            }
            return null;
        }



    }
}
