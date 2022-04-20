using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class ConsultationRepository : IConsultation
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public ConsultationRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
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
                    result.CON_PR_Id_FK = lead.CON_PR_Id_FK;
                    result.CON_DO_Id_FK = lead.CON_DO_Id_FK;
                    result.CON_HO_Id_FK = lead.CON_HO_Id_FK;
                    result.CON_CD_Id_FK = lead.CON_CD_Id_FK;
                    result.CON_SP_Id_FK = lead.CON_SP_Id_FK;
                    result.Dis_Id_FK = lead.Dis_Id_FK;
                    result.CON_Ref_AS_Id = lead.CON_Ref_AS_Id;
                    result.CON_Code = lead.CON_Code;
                    //result.CON_AssistantRefferedTime = lead.CON_AssistantRefferedTime;
                    result.CON_ConsultedDate = lead.CON_ConsultedDate;
                    result.CON_UserId_FK = lead.CON_UserId_FK;
                    //result.Ref_Appt_Id = lead.Ref_Appt_Id;
                    //result.Ref_Con_Id = lead.Ref_Con_Id;
                    result.Inactive = lead.Inactive;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 1;
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
                                 join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id
                                 join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id
                                 join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id
                                 join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id
                                 join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id
                                 join h in db.Parameters on a.CON_APPT_Id_FK equals h.PA_APPT_Id_FK
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
                                                       join j in db.ComplaintMst on i.CPT_Id equals j.Cmst_Id
                                                       where i.CPT_APPT_Id_FK == a.CON_APPT_Id_FK
                                                       select new GetAllComplaint()
                                                       {
                                                           //CPT_Id = i.CPT_Id,
                                                           CPT_MST_Id_FK = i.CPT_MST_Id_FK,
                                                           CPT_MST_Name = j.Cmst_Name,
                                                           //CPT_APPT_Id_FK = i.CPT_APPT_Id_FK,
                                                           //Remarks = i.Remarks,
                                                           //delete_flag = i.delete_flag
                                                       }).ToList(),
                                     symptomslist = (from k in db.Symptoms
                                                     join l in db.SymptomsMst on k.SYM_Id equals l.Smst_Id
                                                     where k.SYM_APPT_Id_FK == a.CON_APPT_Id_FK
                                                     select new GetAllSymptoms()
                                                     {
                                                         //SYM_Id = k.SYM_Id,
                                                         SYM_MST_Id_FK = k.SYM_MST_Id_FK,
                                                         SYM_MST_Name = l.Smst_Name,
                                                         //SYM_APPT_Id_FK = k.SYM_APPT_Id_FK,
                                                         //Remarks = k.Remarks,
                                                         //delete_flag=k.delete_flag,
                                                     }).ToList(),
                                     diseaseslist = (from m in db.DiseasesDtl
                                                     join n in db.Diseases on m.Dis_Id_FK equals n.Id
                                                     where m.Ddtl_APPT_Id_FK == a.CON_APPT_Id_FK
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         //Ddtl_Id = m.Ddtl_Id,
                                                         Dis_Id_FK = m.Dis_Id_FK,
                                                         Dis_Name = n.Diseases_Name,
                                                         //Ddtl_APPT_Id_FK = m.Ddtl_APPT_Id_FK,
                                                         //Remarks = m.Remarks,
                                                         //delete_flag = m.delete_flag,
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
                                     status = a.status

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
                    result.status = 5;
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
            if (db != null)
            {
                var query = (from a in db.Consultation
                             join b in db.Patient on a.CON_PR_Id_FK equals b.PR_Id
                             join c in db.Doctor on a.CON_DO_Id_FK equals c.DO_Id
                             join d in db.Hospital on a.CON_HO_Id_FK equals d.Hos_Id
                             join e in db.Discipline on a.CON_CD_Id_FK equals e.CD_Id
                             join f in db.Specialization on a.CON_SP_Id_FK equals f.SP_Id
                             join g in db.Assistant on a.CON_Ref_AS_Id equals g.Assi_Id
                             join h in db.Parameters on a.CON_APPT_Id_FK equals h.PA_APPT_Id_FK
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
                                                   join j in db.ComplaintMst on i.CPT_Id equals j.Cmst_Id
                                                   where i.CPT_APPT_Id_FK == a.CON_APPT_Id_FK
                                                   select new GetAllComplaint()
                                                   {
                                                       //CPT_Id = i.CPT_Id,
                                                       CPT_MST_Id_FK = i.CPT_MST_Id_FK,
                                                       CPT_MST_Name = j.Cmst_Name,
                                                       //CPT_APPT_Id_FK = i.CPT_APPT_Id_FK,
                                                       //Remarks = i.Remarks,
                                                       //delete_flag = i.delete_flag
                                                   }).ToList(),
                                 symptomslist = (from k in db.Symptoms
                                                 join l in db.SymptomsMst on k.SYM_Id equals l.Smst_Id
                                                 where k.SYM_APPT_Id_FK == a.CON_APPT_Id_FK
                                                 select new GetAllSymptoms()
                                                 {
                                                     //SYM_Id = k.SYM_Id,
                                                     SYM_MST_Id_FK = k.SYM_MST_Id_FK,
                                                     SYM_MST_Name = l.Smst_Name,
                                                     //SYM_APPT_Id_FK = k.SYM_APPT_Id_FK,
                                                     //Remarks = k.Remarks,
                                                     //delete_flag=k.delete_flag,
                                                 }).ToList(),
                                 diseaseslist = (from m in db.DiseasesDtl
                                                 join n in db.Diseases on m.Dis_Id_FK equals n.Id
                                                 where m.Ddtl_APPT_Id_FK == a.CON_APPT_Id_FK
                                                 select new GetAllDiseasesDtl()
                                                 {
                                                     //Ddtl_Id = m.Ddtl_Id,
                                                     Dis_Id_FK = m.Dis_Id_FK,
                                                     Dis_Name = n.Diseases_Name,
                                                     //Ddtl_APPT_Id_FK = m.Ddtl_APPT_Id_FK,
                                                     //Remarks = m.Remarks,
                                                     //delete_flag = m.delete_flag,
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
                                 status = a.status

                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
