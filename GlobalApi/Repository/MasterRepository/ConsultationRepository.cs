using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class ConsultationRepository : IConsultation
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public ConsultationRepository()
        {
            db = new GlobalContext();
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
                                 join h in db.Parameters on a.CON_APPT_Id_FK equals h.Appt_Id into hlist
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
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     CON_PR_Photo = b.PR_Photo,
                                     complaintslist = (from i in db.Complaint
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.Appt_Id == a.CON_APPT_Id_FK
                                                       select new GetAllComplaint()
                                                       {
                                                           //CPT_Id = i.CPT_Id,
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Name = j.Cmst_Name,
                                                           //CPT_APPT_Id_FK = i.CPT_APPT_Id_FK,
                                                           //Remarks = i.Remarks,
                                                           //delete_flag = i.delete_flag
                                                       }).ToList(),
                                     symptomslist = (from k in db.Symptoms
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.Appt_Id == a.CON_APPT_Id_FK
                                                     select new GetAllSymptoms()
                                                     {
                                                         //SYM_Id = k.SYM_Id,
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Name = l.Smst_Name,
                                                         //SYM_APPT_Id_FK = k.SYM_APPT_Id_FK,
                                                         //Remarks = k.Remarks,
                                                         //delete_flag=k.delete_flag,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.DiseasesDtl
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.Appt_Id == a.CON_APPT_Id_FK
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         //Ddtl_Id = m.Ddtl_Id,
                                                         Id = m.Id,
                                                         Diseases_Name = n.Diseases_Name,
                                                         //Ddtl_APPT_Id_FK = m.Ddtl_APPT_Id_FK,
                                                         //Remarks = m.Remarks,
                                                         //delete_flag = m.delete_flag,
                                                     }).ToList(),

                                     Allergylist = (from p in db.AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.Appt_Id == a.CON_APPT_Id_FK
                                                    select new GetAllAllergySigns_DTL()
                                                    {
                                                        //Ddtl_Id = k.Ddtl_Id,
                                                        Al_Id = p.Al_Id,
                                                        Al_Name = q.Al_Name,
                                                        //Ddtl_APPT_Id_FK = k.Ddtl_APPT_Id_FK,
                                                        //Remarks = k.Remarks,
                                                        //delete_flag = k.delete_flag,
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
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
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
                                 join h in db.Parameters on a.Phc_ApptId equals h.MAppt_Id into hlist
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
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     CON_PR_Photo = b.PR_Photo,
                                     complaintslist = (from i in db.Complaint
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.MAppt_Id == a.Phc_ApptId
                                                       select new GetAllComplaint()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Name = j.Cmst_Name,

                                                       }).ToList(),
                                     symptomslist = (from k in db.Symptoms
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.MAppt_Id == a.Phc_ApptId
                                                     select new GetAllSymptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.DiseasesDtl
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.MAppt_Id == a.Phc_ApptId
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Name = n.Diseases_Name,

                                                     }).ToList(),
                                     Allergylist = (from p in db.AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.MAppt_Id == a.Phc_ApptId
                                                    select new GetAllAllergySigns_DTL()
                                                    {
                                                        //Ddtl_Id = k.Ddtl_Id,
                                                        Al_Id = p.Al_Id,
                                                        Al_Name = q.Al_Name,
                                                        //Ddtl_APPT_Id_FK = k.Ddtl_APPT_Id_FK,
                                                        //Remarks = k.Remarks,
                                                        //delete_flag = k.delete_flag,
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
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
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
                                 join h in db.Parameters on a.CON_APPT_Id_FK equals h.Appt_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 where a.CON_PR_Id_FK == CON_PR_Id_FK
                                 select new ConsultationBy_Id
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     CON_APPT_Id_FK = a.CON_APPT_Id_FK,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     CON_PR_Photo = b.PR_Photo,
                                     complaintslist = (from i in db.Complaint
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.Appt_Id == a.CON_APPT_Id_FK
                                                       select new GetAllComplaint()
                                                       {
                                                           //CPT_Id = i.CPT_Id,
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Name = j.Cmst_Name,
                                                           //CPT_APPT_Id_FK = i.CPT_APPT_Id_FK,
                                                           //Remarks = i.Remarks,
                                                           //delete_flag = i.delete_flag
                                                       }).ToList(),
                                     symptomslist = (from k in db.Symptoms
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.Appt_Id == a.CON_APPT_Id_FK
                                                     select new GetAllSymptoms()
                                                     {
                                                         //SYM_Id = k.SYM_Id,
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Name = l.Smst_Name,
                                                         //SYM_APPT_Id_FK = k.SYM_APPT_Id_FK,
                                                         //Remarks = k.Remarks,
                                                         //delete_flag=k.delete_flag,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.DiseasesDtl
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.Appt_Id == a.CON_APPT_Id_FK
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         //Ddtl_Id = m.Ddtl_Id,
                                                         Id = m.Id,
                                                         Diseases_Name = n.Diseases_Name,
                                                         //Ddtl_APPT_Id_FK = m.Ddtl_APPT_Id_FK,
                                                         //Remarks = m.Remarks,
                                                         //delete_flag = m.delete_flag,
                                                     }).ToList(),
                                     Allergylist = (from p in db.AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.Appt_Id == a.CON_APPT_Id_FK
                                                    select new GetAllAllergySigns_DTL()
                                                    {
                                                        //Ddtl_Id = k.Ddtl_Id,
                                                        Al_Id = p.Al_Id,
                                                        Al_Name = q.Al_Name,
                                                        //Ddtl_APPT_Id_FK = k.Ddtl_APPT_Id_FK,
                                                        //Remarks = k.Remarks,
                                                        //delete_flag = k.delete_flag,
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
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,

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
        public async Task<List<ConsultationBy_Id>> GetAdminConsultationById(int CON_Id)
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
                                 join h in db.Parameters on a.CON_APPT_Id_FK equals h.Appt_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 where a.CON_Id == CON_Id
                                 select new ConsultationBy_Id
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     CON_APPT_Id_FK = a.CON_APPT_Id_FK,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     CON_PR_Photo = b.PR_Photo,
                                     complaintslist = (from i in db.Complaint
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.Appt_Id == a.CON_APPT_Id_FK
                                                       select new GetAllComplaint()
                                                       {
                                                           //CPT_Id = i.CPT_Id,
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Name = j.Cmst_Name,
                                                           //CPT_APPT_Id_FK = i.CPT_APPT_Id_FK,
                                                           //Remarks = i.Remarks,
                                                           //delete_flag = i.delete_flag
                                                       }).ToList(),
                                     symptomslist = (from k in db.Symptoms
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.Appt_Id == a.CON_APPT_Id_FK
                                                     select new GetAllSymptoms()
                                                     {
                                                         //SYM_Id = k.SYM_Id,
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Name = l.Smst_Name,
                                                         //SYM_APPT_Id_FK = k.SYM_APPT_Id_FK,
                                                         //Remarks = k.Remarks,
                                                         //delete_flag=k.delete_flag,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.DiseasesDtl
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.Appt_Id == a.CON_APPT_Id_FK
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         //Ddtl_Id = m.Ddtl_Id,
                                                         Id = m.Id,
                                                         Diseases_Name = n.Diseases_Name,
                                                         //Ddtl_APPT_Id_FK = m.Ddtl_APPT_Id_FK,
                                                         //Remarks = m.Remarks,
                                                         //delete_flag = m.delete_flag,
                                                     }).ToList(),
                                     Allergylist = (from p in db.AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.Appt_Id == a.CON_APPT_Id_FK
                                                    select new GetAllAllergySigns_DTL()
                                                    {
                                                        //Ddtl_Id = k.Ddtl_Id,
                                                        Al_Id = p.Al_Id,
                                                        Al_Name = q.Al_Name,
                                                        //Ddtl_APPT_Id_FK = k.Ddtl_APPT_Id_FK,
                                                        //Remarks = k.Remarks,
                                                        //delete_flag = k.delete_flag,
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
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
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
                                 join h in db.Parameters on a.Phc_ApptId equals h.MAppt_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join o in db.Status on a.status equals o.sts_id
                                 where a.CON_Id == CON_Id
                                 select new PhcConsultationBy_Id
                                 {
                                     CON_Id = a.CON_Id,
                                     CON_Code = a.CON_Code,
                                     CON_Type = a.CON_Type,
                                     Phc_ApptId = a.Phc_ApptId,
                                     CON_PR_Id_FK = a.CON_PR_Id_FK,
                                     CON_PR_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     CON_PR_Gender = b.PR_Gender,
                                     CON_PR_DOB = b.PR_DOB,
                                     CON_PR_Age = b.PR_Age,
                                     CON_PR_BloodGroup = b.PR_BloodGroup,
                                     CON_PR_Photo = b.PR_Photo,
                                     complaintslist = (from i in db.Complaint
                                                       join j in db.ComplaintMst on i.Cmst_Id equals j.Cmst_Id
                                                       where i.MAppt_Id == a.Phc_ApptId
                                                       select new GetAllComplaint()
                                                       {
                                                           Cmst_Id = i.Cmst_Id,
                                                           Cmst_Name = j.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from k in db.Symptoms
                                                     join l in db.SymptomsMst on k.Smst_Id equals l.Smst_Id
                                                     where k.MAppt_Id == a.Phc_ApptId
                                                     select new GetAllSymptoms()
                                                     {
                                                         Smst_Id = k.Smst_Id,
                                                         Smst_Name = l.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.DiseasesDtl
                                                     join n in db.Diseases on m.Id equals n.Id
                                                     where m.MAppt_Id == a.Phc_ApptId
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         Id = m.Id,
                                                         Diseases_Name = n.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.MAppt_Id == a.Phc_ApptId
                                                    select new GetAllAllergySigns_DTL()
                                                    {
                                                        Al_Id = p.Al_Id,
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
                                     Inactive = a.Inactive,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = o.sts_name,
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

    }
}
