using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class ConsultationRepository : IConsultation
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private Consult_Complaint_DTLRepository consult_Complaint_DTLRepository;
        private Consult_Symptoms_DTLRepository consult_Symptoms_DTLRepository;
        private Consult_Diseases_DTLRepository consult_Diseases_DTLRepository;
        private Consult_AllergySigns_DTLRepository consult_AllergySigns_DTLRepository;
        private IPrimarykeyvalue primarykeyvalue;
        public ConsultationRepository()
        {
            db = new GlobalContext();
            ado_Configurations = new ADO_Configrations();
            this.consult_Complaint_DTLRepository = new Consult_Complaint_DTLRepository();
            this.consult_Symptoms_DTLRepository = new Consult_Symptoms_DTLRepository();
            this.consult_Diseases_DTLRepository = new Consult_Diseases_DTLRepository();
            this.consult_AllergySigns_DTLRepository = new Consult_AllergySigns_DTLRepository();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Consultation> UpdateConsultation(Consultation lead)
        {
            try
            {
                var result = await db.Consultation.FirstOrDefaultAsync(x => x.CON_Id == lead.CON_Id);
                if (result != null)
                {
                    result.CON_Id = lead.CON_Id;
                    result.CON_Code = lead.CON_Code;
                    result.CON_Type = lead.CON_Type;
                    result.CON_APPT_Id_FK = lead.CON_APPT_Id_FK;
                    //result.Phc_ApptId = lead.Phc_ApptId;
                    result.CON_PR_Id_FK = lead.CON_PR_Id_FK;
                    result.CON_DO_Id_FK = lead.CON_DO_Id_FK;
                    result.CON_HO_Id_FK = lead.CON_HO_Id_FK;
                    result.CON_CD_Id_FK = lead.CON_CD_Id_FK;
                    result.CON_SP_Id_FK = lead.CON_SP_Id_FK;
                    result.CON_Ref_AS_Id = lead.CON_Ref_AS_Id;
                    result.CON_Code = lead.CON_Code;
                    result.CON_ConsultedDate = lead.CON_ConsultedDate;
                    result.CON_ConsultedTime = lead.CON_ConsultedTime;
                    result.CON_UserId_FK = lead.CON_UserId_FK;
                    result.Inactive = lead.Inactive;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    result.Remarks = lead.Remarks;
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
        public async Task<Consultation> UpdatePhcConsultation(Consultation lead)
        {
            try
            {
                var result = await db.Consultation.FirstOrDefaultAsync(x => x.CON_Id == lead.CON_Id);
                if (result != null)
                {
                    result.CON_Id = lead.CON_Id;
                    result.CON_Code = lead.CON_Code;
                    result.CON_Type = lead.CON_Type;
                    result.CON_APPT_Id_FK = lead.CON_APPT_Id_FK;
                    //result.Phc_ApptId = lead.Phc_ApptId;
                    result.CON_PR_Id_FK = lead.CON_PR_Id_FK;
                    result.CON_DO_Id_FK = lead.CON_DO_Id_FK;
                    result.CON_HO_Id_FK = lead.CON_HO_Id_FK;
                    result.CON_CD_Id_FK = lead.CON_CD_Id_FK;
                    result.CON_SP_Id_FK = lead.CON_SP_Id_FK;
                    result.CON_Ref_AS_Id = lead.CON_Ref_AS_Id;
                    result.CON_Code = lead.CON_Code;
                    result.CON_ConsultedDate = lead.CON_ConsultedDate;
                    result.CON_ConsultedTime = lead.CON_ConsultedTime;
                    result.CON_UserId_FK = lead.CON_UserId_FK;
                    result.Inactive = lead.Inactive;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    result.Remarks = lead.Remarks;
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
        public async Task<List<GetAllConsultation>> GetAllConsultation()
        {
            try
            {

                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Consult_Parameters on a.CON_Id equals h.CON_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 orderby a.CON_Id descending
                                 select new GetAllConsultation
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     CON_APPT_Id_FK = a.CON_APPT_Id_FK,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     PR_Code = b.PR_PatientCode,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     CON_PR_Photo = b.PR_Photo,
                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from i in db.Consult_Complaint_DTL
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.CON_Id == a.CON_Id
                                                       select new GetAllCons_Complaints()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Code = j.Cmst_Code,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Consult_Symptoms_DTL
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.CON_Id == a.CON_Id
                                                     select new GetAllCons_Symptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Code = l.Smst_Code,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.Consult_Diseases_DTL
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.CON_Id == a.CON_Id
                                                     select new GetAllCons_Diseases()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Code = n.Diseases_Code,
                                                         Acronyms = n.Acronyms,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.Consult_AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.CON_Id == a.CON_Id
                                                    select new GetAllCons_Allergys()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
                                                        Al_Name = q.Al_Name,
                                                    }).ToList(),
                                     CON_DO_Id_FK = a.CON_DO_Id_FK,
                                     CON_DO_Name = string.Concat(c.DO_FirstName, c.DO_LastName),
                                     CON_HO_Id_FK = a.CON_HO_Id_FK,
                                     CON_HospitalName = d.Hos_HospitalName,
                                     CON_CD_Id_FK = a.CON_CD_Id_FK,
                                     CON_ClinicalDiscipline = e.CD_ClinicalDiscipline,
                                     CON_SP_Id_FK = a.CON_SP_Id_FK,
                                     CON_Specialization = f.SP_Specialization,
                                     CON_Ref_AS_Id = a.CON_Ref_AS_Id,
                                     CON_Ref_AS_Name = string.Concat(g.Assi_FirstName, g.Assi_LastName),
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_ConsultedTime = a.CON_ConsultedTime,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     CON_PA_Id = h.PA_Id,
                                     CON_Height = h.PA_Height,
                                     CON_Weight = h.PA_Weight,
                                     CON_TempInFahrenheit = h.PA_TempInFahrenheit,
                                     CON_TempInCelsius = h.PA_TempInCelsius,
                                     CON_BloodPressure = h.PA_BloodPressure,
                                     CON_Sugar = h.PA_Sugar,
                                     CON_RespiratoryRate = h.PA_RespiratoryRate,
                                     CON_PulseRate = h.PA_PulseRate,
                                     CON_ECG = h.PA_ECG,
                                     CON_OxygenSaturation = h.PA_OxygenSaturation,
                                     CON_Hemoglobin = h.PA_Hemoglobin,
                                     UnderBPMedication = a.UnderBPMedication,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
                                     Remarks = a.Remarks,
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
        public async Task<List<GetAllPhcConsultation>> GetAllPhcConsultation()
        {
            try
            {

                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Consult_Parameters on a.CON_Id equals h.CON_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 orderby a.CON_Id descending
                                 select new GetAllPhcConsultation
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     Phc_ApptId = a.Phc_ApptId,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     PR_Code = b.PR_PatientCode,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     CON_PR_Photo = b.PR_Photo,
                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from i in db.Consult_Complaint_DTL
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.CON_Id == a.CON_Id
                                                       select new GetAllCons_Complaints()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Code = j.Cmst_Code,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Consult_Symptoms_DTL
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.CON_Id == a.CON_Id
                                                     select new GetAllCons_Symptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Code = l.Smst_Code,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.Consult_Diseases_DTL
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.CON_Id == a.CON_Id
                                                     select new GetAllCons_Diseases()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Code = n.Diseases_Code,
                                                         Acronyms = n.Acronyms,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.Consult_AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.CON_Id == a.CON_Id
                                                    select new GetAllCons_Allergys()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
                                                        Al_Name = q.Al_Name,
                                                    }).ToList(),

                                     CON_DO_Id_FK = a.CON_DO_Id_FK,
                                     CON_DO_Name = string.Concat(c.DO_FirstName, c.DO_LastName),
                                     CON_HO_Id_FK = a.CON_HO_Id_FK,
                                     CON_HospitalName = d.Hos_HospitalName,
                                     CON_CD_Id_FK = a.CON_CD_Id_FK,
                                     CON_ClinicalDiscipline = e.CD_ClinicalDiscipline,
                                     CON_SP_Id_FK = a.CON_SP_Id_FK,
                                     CON_Specialization = f.SP_Specialization,
                                     CON_Ref_AS_Id = a.CON_Ref_AS_Id,
                                     CON_Ref_AS_Name = string.Concat(g.Assi_FirstName, g.Assi_LastName),
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_ConsultedTime = a.CON_ConsultedTime,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     CON_PA_Id = h.PA_Id,
                                     CON_Height = h.PA_Height,
                                     CON_Weight = h.PA_Weight,
                                     CON_TempInFahrenheit = h.PA_TempInFahrenheit,
                                     CON_TempInCelsius = h.PA_TempInCelsius,
                                     CON_BloodPressure = h.PA_BloodPressure,
                                     CON_Sugar = h.PA_Sugar,
                                     CON_RespiratoryRate = h.PA_RespiratoryRate,
                                     CON_PulseRate = h.PA_PulseRate,
                                     CON_ECG = h.PA_ECG,
                                     CON_OxygenSaturation = h.PA_OxygenSaturation,
                                     CON_Hemoglobin = h.PA_Hemoglobin,
                                     UnderBPMedication = a.UnderBPMedication,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
                                     Remarks = a.Remarks,

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
        public async Task<Consultation> DeleteConsultation(int CON_Id)
        {
            try
            {
                var result = await db.Consultation.FirstOrDefaultAsync(x => x.CON_Id == CON_Id);
                if (result != null)
                {
                    result.CON_Id = CON_Id;
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
        public async Task<List<ConsultationBy_Id>> GetConsultationById(int CON_PR_Id_FK)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Consult_Parameters on a.CON_Id equals h.CON_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 join t in db.PatientAppointment on a.CON_APPT_Id_FK equals t.Appt_Id 
                                 where a.CON_PR_Id_FK == CON_PR_Id_FK
                                 select new ConsultationBy_Id
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     CON_APPT_Id_FK = a.CON_APPT_Id_FK,
                                     Appt_Date = t.Select_day,
                                     Appt_FrmTime = t.Select_FrmTime,
                                     Appt_ToTime = t.Select_toTime,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     PR_Code = b.PR_PatientCode,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     //CON_PR_Photo = b.PR_Photo,
                                     Imagebyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                             System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                             System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),

                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from i in db.Consult_Complaint_DTL
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.CON_Id == a.CON_Id
                                                       select new GetAllCons_Complaints()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Code = j.Cmst_Code,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Consult_Symptoms_DTL
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.CON_Id == a.CON_Id
                                                     select new GetAllCons_Symptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Code = l.Smst_Code,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.Consult_Diseases_DTL
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.CON_Id == a.CON_Id
                                                     select new GetAllCons_Diseases()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Code = n.Diseases_Code,
                                                         Acronyms = n.Acronyms,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.Consult_AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.CON_Id == a.CON_Id
                                                    select new GetAllCons_Allergys()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
                                                        Al_Name = q.Al_Name,
                                                    }).ToList(),


                                     CON_DO_Id_FK = a.CON_DO_Id_FK,
                                     CON_DO_Name = string.Concat(c.DO_FirstName, c.DO_LastName),
                                     CON_HO_Id_FK = a.CON_HO_Id_FK,
                                     CON_HospitalName = d.Hos_HospitalName,
                                     CON_CD_Id_FK = a.CON_CD_Id_FK,
                                     CON_ClinicalDiscipline = e.CD_ClinicalDiscipline,
                                     CON_SP_Id_FK = a.CON_SP_Id_FK,
                                     CON_Specialization = f.SP_Specialization,
                                     CON_Ref_AS_Id = a.CON_Ref_AS_Id,
                                     CON_Ref_AS_Name = string.Concat(g.Assi_FirstName, g.Assi_LastName),
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_ConsultedTime = a.CON_ConsultedTime,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     CON_PA_Id = h.PA_Id,
                                     CON_Height = h.PA_Height,
                                     CON_Weight = h.PA_Weight,
                                     CON_TempInFahrenheit = h.PA_TempInFahrenheit,
                                     CON_TempInCelsius = h.PA_TempInCelsius,
                                     CON_BloodPressure = h.PA_BloodPressure,
                                     CON_Sugar = h.PA_Sugar,
                                     CON_RespiratoryRate = h.PA_RespiratoryRate,
                                     CON_PulseRate = h.PA_PulseRate,
                                     CON_ECG = h.PA_ECG,
                                     CON_OxygenSaturation = h.PA_OxygenSaturation,
                                     CON_Hemoglobin = h.PA_Hemoglobin,
                                     UnderBPMedication = a.UnderBPMedication,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
                                     Remarks = a.Remarks,

                                 }).ToListAsync();
                    return await query;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<ConsultationBy_Id> GetAdminConsultationById(int CON_Id)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Consult_Parameters on a.CON_Id equals h.CON_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 join t in db.PatientAppointment on a.CON_APPT_Id_FK equals t.Appt_Id
                                 where a.CON_Id == CON_Id
                                 select new ConsultationBy_Id
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     CON_APPT_Id_FK = a.CON_APPT_Id_FK,
                                     Appt_Date = t.Select_day,
                                     Appt_FrmTime = t.Select_FrmTime,
                                     Appt_ToTime = t.Select_toTime,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     PR_Code = b.PR_PatientCode,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     //CON_PR_Photo = b.PR_Photo,
                                     Imagebyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                             System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                             System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),

                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from i in db.Consult_Complaint_DTL
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.CON_Id == a.CON_Id
                                                       select new GetAllCons_Complaints()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Code = j.Cmst_Code,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Consult_Symptoms_DTL
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.CON_Id == a.CON_Id
                                                     select new GetAllCons_Symptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Code = l.Smst_Code,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.Consult_Diseases_DTL
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.CON_Id == a.CON_Id
                                                     select new GetAllCons_Diseases()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Code = n.Diseases_Code,
                                                         Acronyms = n.Acronyms,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.Consult_AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.CON_Id == a.CON_Id
                                                    select new GetAllCons_Allergys()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
                                                        Al_Name = q.Al_Name,
                                                    }).ToList(),

                                     CON_DO_Id_FK = a.CON_DO_Id_FK,
                                     CON_DO_Name = string.Concat(c.DO_FirstName, c.DO_LastName),
                                     CON_HO_Id_FK = a.CON_HO_Id_FK,
                                     CON_HospitalName = d.Hos_HospitalName,
                                     CON_CD_Id_FK = a.CON_CD_Id_FK,
                                     CON_ClinicalDiscipline = e.CD_ClinicalDiscipline,
                                     CON_SP_Id_FK = a.CON_SP_Id_FK,
                                     CON_Specialization = f.SP_Specialization,
                                     CON_Ref_AS_Id = a.CON_Ref_AS_Id,
                                     CON_Ref_AS_Name = string.Concat(g.Assi_FirstName, g.Assi_LastName),
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_ConsultedTime = a.CON_ConsultedTime,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     CON_PA_Id = h.PA_Id,
                                     CON_Height = h.PA_Height,
                                     CON_Weight = h.PA_Weight,
                                     CON_TempInFahrenheit = h.PA_TempInFahrenheit,
                                     CON_TempInCelsius = h.PA_TempInCelsius,
                                     CON_BloodPressure = h.PA_BloodPressure,
                                     CON_Sugar = h.PA_Sugar,
                                     CON_RespiratoryRate = h.PA_RespiratoryRate,
                                     CON_PulseRate = h.PA_PulseRate,
                                     CON_ECG = h.PA_ECG,
                                     CON_OxygenSaturation = h.PA_OxygenSaturation,
                                     CON_Hemoglobin = h.PA_Hemoglobin,
                                     UnderBPMedication = a.UnderBPMedication,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
                                     Remarks = a.Remarks,

                                 }).FirstOrDefaultAsync();
                    return await query;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<PhcConsultationBy_Id>> GetPhcConsultationById(int CON_Id)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Consult_Parameters on a.CON_Id equals h.CON_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 join t in db.PHC_Appointment on a.CON_APPT_Id_FK equals t.Phc_Appt_Id
                                 where a.CON_Id == CON_Id
                                 select new PhcConsultationBy_Id
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     Phc_ApptId = a.Phc_ApptId,
                                     Appt_Date = t.Select_day,
                                     Appt_FrmTime = t.Select_FrmTime,
                                     Appt_ToTime = t.Select_toTime,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     PR_Code = b.PR_PatientCode,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     Imagebyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                             System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                             System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from i in db.Consult_Complaint_DTL
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.CON_Id == a.CON_Id
                                                       select new GetAllCons_Complaints()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Code = j.Cmst_Code,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Consult_Symptoms_DTL
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.CON_Id == a.CON_Id
                                                     select new GetAllCons_Symptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Code = l.Smst_Code,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.Consult_Diseases_DTL
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.CON_Id == a.CON_Id
                                                     select new GetAllCons_Diseases()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Code = n.Diseases_Code,
                                                         Acronyms = n.Acronyms,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.Consult_AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.CON_Id == a.CON_Id
                                                    select new GetAllCons_Allergys()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
                                                        Al_Name = q.Al_Name,
                                                    }).ToList(),


                                     CON_DO_Id_FK = a.CON_DO_Id_FK,
                                     CON_DO_Name = string.Concat(c.DO_FirstName, c.DO_LastName),
                                     CON_HO_Id_FK = a.CON_HO_Id_FK,
                                     CON_HospitalName = d.Hos_HospitalName,
                                     CON_CD_Id_FK = a.CON_CD_Id_FK,
                                     CON_ClinicalDiscipline = e.CD_ClinicalDiscipline,
                                     CON_SP_Id_FK = a.CON_SP_Id_FK,
                                     CON_Specialization = f.SP_Specialization,
                                     CON_Ref_AS_Id = a.CON_Ref_AS_Id,
                                     CON_Ref_AS_Name = string.Concat(g.Assi_FirstName, g.Assi_LastName),
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_ConsultedTime = a.CON_ConsultedTime,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     CON_PA_Id = h.PA_Id,
                                     CON_Height = h.PA_Height,
                                     CON_Weight = h.PA_Weight,
                                     CON_TempInFahrenheit = h.PA_TempInFahrenheit,
                                     CON_TempInCelsius = h.PA_TempInCelsius,
                                     CON_BloodPressure = h.PA_BloodPressure,
                                     CON_Sugar = h.PA_Sugar,
                                     CON_RespiratoryRate = h.PA_RespiratoryRate,
                                     CON_PulseRate = h.PA_PulseRate,
                                     CON_ECG = h.PA_ECG,
                                     CON_OxygenSaturation = h.PA_OxygenSaturation,
                                     CON_Hemoglobin = h.PA_Hemoglobin,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     UnderBPMedication = a.UnderBPMedication,
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
                                     Remarks = a.Remarks,

                                 }).ToListAsync();
                    return await query;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ConsultationBy_ApptId>> GetAdminConsultationBy_ApptId(int Appt_Id)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Consult_Parameters on a.CON_Id equals h.CON_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 where a.CON_APPT_Id_FK == Appt_Id
                                 select new ConsultationBy_ApptId
                                 {
                                     CON_APPT_Id_FK = a.CON_APPT_Id_FK,
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     PR_Code = b.PR_PatientCode,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     Imagebyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                             System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                             System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),

                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from i in db.Consult_Complaint_DTL
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.CON_Id == a.CON_Id
                                                       select new GetAllCons_Complaints()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Code = j.Cmst_Code,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Consult_Symptoms_DTL
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.CON_Id == a.CON_Id
                                                     select new GetAllCons_Symptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Code = l.Smst_Code,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.Consult_Diseases_DTL
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.CON_Id == a.CON_Id
                                                     select new GetAllCons_Diseases()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Code = n.Diseases_Code,
                                                         Acronyms = n.Acronyms,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.Consult_AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.CON_Id == a.CON_Id
                                                    select new GetAllCons_Allergys()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
                                                        Al_Name = q.Al_Name,
                                                    }).ToList(),

                                     CON_DO_Id_FK = a.CON_DO_Id_FK,
                                     CON_DO_Name = string.Concat(c.DO_FirstName, c.DO_LastName),
                                     CON_HO_Id_FK = a.CON_HO_Id_FK,
                                     CON_HospitalName = d.Hos_HospitalName,
                                     CON_CD_Id_FK = a.CON_CD_Id_FK,
                                     CON_ClinicalDiscipline = e.CD_ClinicalDiscipline,
                                     CON_SP_Id_FK = a.CON_SP_Id_FK,
                                     CON_Specialization = f.SP_Specialization,
                                     CON_Ref_AS_Id = a.CON_Ref_AS_Id,
                                     CON_Ref_AS_Name = string.Concat(g.Assi_FirstName, g.Assi_LastName),
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_ConsultedTime = a.CON_ConsultedTime,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     CON_PA_Id = h.PA_Id,
                                     CON_Height = h.PA_Height,
                                     CON_Weight = h.PA_Weight,
                                     CON_TempInFahrenheit = h.PA_TempInFahrenheit,
                                     CON_TempInCelsius = h.PA_TempInCelsius,
                                     CON_BloodPressure = h.PA_BloodPressure,
                                     CON_Sugar = h.PA_Sugar,
                                     CON_RespiratoryRate = h.PA_RespiratoryRate,
                                     CON_PulseRate = h.PA_PulseRate,
                                     CON_ECG = h.PA_ECG,
                                     CON_OxygenSaturation = h.PA_OxygenSaturation,
                                     CON_Hemoglobin = h.PA_Hemoglobin,
                                     UnderBPMedication = a.UnderBPMedication,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
                                     Remarks = a.Remarks,

                                 }).ToListAsync();
                    return await query;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<PhcConsultationBy_MAppt_Id>> GetPhcConsultationBy_ApptId(int Appt_Id)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Consult_Parameters on a.CON_Id equals h.CON_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 where a.Phc_ApptId == Appt_Id
                                 select new PhcConsultationBy_MAppt_Id
                                 {
                                     Phc_ApptId = a.Phc_ApptId,
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     PR_Code = b.PR_PatientCode,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     Imagebyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                             System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                             System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),

                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from i in db.Consult_Complaint_DTL
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.CON_Id == a.CON_Id
                                                       select new GetAllCons_Complaints()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Code = j.Cmst_Code,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Consult_Symptoms_DTL
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.CON_Id == a.CON_Id
                                                     select new GetAllCons_Symptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Code = l.Smst_Code,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.Consult_Diseases_DTL
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.CON_Id == a.CON_Id
                                                     select new GetAllCons_Diseases()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Code = n.Diseases_Code,
                                                         Acronyms = n.Acronyms,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.Consult_AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.CON_Id == a.CON_Id
                                                    select new GetAllCons_Allergys()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
                                                        Al_Name = q.Al_Name,
                                                    }).ToList(),

                                     CON_DO_Id_FK = a.CON_DO_Id_FK,
                                     CON_DO_Name = string.Concat(c.DO_FirstName, c.DO_LastName),
                                     CON_HO_Id_FK = a.CON_HO_Id_FK,
                                     CON_HospitalName = d.Hos_HospitalName,
                                     CON_CD_Id_FK = a.CON_CD_Id_FK,
                                     CON_ClinicalDiscipline = e.CD_ClinicalDiscipline,
                                     CON_SP_Id_FK = a.CON_SP_Id_FK,
                                     CON_Specialization = f.SP_Specialization,
                                     CON_Ref_AS_Id = a.CON_Ref_AS_Id,
                                     CON_Ref_AS_Name = string.Concat(g.Assi_FirstName, g.Assi_LastName),
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_ConsultedTime = a.CON_ConsultedTime,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     CON_PA_Id = h.PA_Id,
                                     CON_Height = h.PA_Height,
                                     CON_Weight = h.PA_Weight,
                                     CON_TempInFahrenheit = h.PA_TempInFahrenheit,
                                     CON_TempInCelsius = h.PA_TempInCelsius,
                                     CON_BloodPressure = h.PA_BloodPressure,
                                     CON_Sugar = h.PA_Sugar,
                                     CON_RespiratoryRate = h.PA_RespiratoryRate,
                                     CON_PulseRate = h.PA_PulseRate,
                                     CON_ECG = h.PA_ECG,
                                     CON_OxygenSaturation = h.PA_OxygenSaturation,
                                     CON_Hemoglobin = h.PA_Hemoglobin,
                                     UnderBPMedication = a.UnderBPMedication,
                                     UnderSugarMedication = a.UnderSugarMedication,
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
                                     Remarks = a.Remarks,

                                 }).ToListAsync();
                    return await query;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Consultation> CloseConsultation(int CON_Id)
        {
            try
            {
                var result = await db.Consultation.FirstOrDefaultAsync(x => x.CON_Id == CON_Id);
                if (result != null)
                {
                    result.CON_Id = CON_Id;
                    result.status = 5;
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

        public async Task<string> UpdateOtherInfo(Other_Info lead)
        {
            try
            {
                var result = await db.Consultation.FirstOrDefaultAsync(x => x.CON_Id == lead.CON_Id);
                if (result != null)
                {
                    var ccpt = await consult_Complaint_DTLRepository.UpdateConsult_Complaint_DTL(lead.Consult_Complaint_DTL, lead.CON_Id);
                    var csym = await consult_Symptoms_DTLRepository.UpdateConsult_Symptoms_DTL(lead.Consult_Symptoms_DTL, lead.CON_Id);
                    var cddtl = await consult_Diseases_DTLRepository.UpdateConsult_Diseases_DTL(lead.Consult_Diseases_DTL, lead.CON_Id);
                    var caldtl = await consult_AllergySigns_DTLRepository.UpdateConsult_AllergySigns_DTL(lead.Consult_AllergySigns_DTL, lead.CON_Id);
                    //result.CON_Id = lead.CON_Id;
                    result.UnderBPMedication = lead.UnderBPMedication;
                    result.UnderSugarMedication = lead.UnderSugarMedication;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "Record Updated successfully";

                }
                return "Appointment Not Found";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
