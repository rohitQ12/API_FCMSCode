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
                                 join i in db.Diseases on a.Dis_Id_FK equals i.Id
                                 //join j in db.Complaint on a.CON_APPT_Id_FK equals j.CPT_APPT_Id_FK
                                 //join k in db.Symptoms on a.CON_APPT_Id_FK equals k.SYM_APPT_Id_FK
                                 join l in db.Parameters on a.CON_APPT_Id_FK equals l.PA_APPT_Id_FK
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
                                     Dis_Id_FK = a.Dis_Id_FK,
                                     Dis_Name = i.Diseases_Name,
                                     CON_ConsultedDate = a.CON_ConsultedDate,
                                     CON_UserId_FK = a.CON_UserId_FK,
                                     //CON_CPT_Name = j.CPT_Complaint,
                                     //CON_SYM_Name = k.SYM_Symptoms,
                                     CON_Height = l.PA_Height,
                                     CON_Weight = l.PA_Weight,
                                     CON_TempInFahrenheit = l.PA_TempInFahrenheit,
                                     CON_TempInCelsius = l.PA_TempInCelsius,
                                     CON_BloodPressure = l.PA_BloodPressure,
                                     CON_Sugar = l.PA_Sugar,
                                     CON_RespiratoryRate = l.PA_RespiratoryRate,
                                     CON_PulseRate = l.PA_PulseRate,
                                     CON_ECG = l.PA_ECG,
                                     CON_OxygenSaturation = l.PA_OxygenSaturation,
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
                    result.status = 0;
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
        public async Task<ConsultationBy_Id> GetConsultationById(int CON_Id)
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
                             join i in db.Diseases on a.Dis_Id_FK equals i.Id
                             //join j in db.Complaint on a.CON_APPT_Id_FK equals j.CPT_APPT_Id_FK
                             //join k in db.Symptoms on a.CON_APPT_Id_FK equals k.SYM_APPT_Id_FK
                             join l in db.Parameters on a.CON_APPT_Id_FK equals l.PA_APPT_Id_FK
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
                                 Dis_Id_FK = a.Dis_Id_FK,
                                 Dis_Name = i.Diseases_Name,
                                 CON_ConsultedDate = a.CON_ConsultedDate,
                                 CON_UserId_FK = a.CON_UserId_FK,
                                 //CON_CPT_Name = j.CPT_Complaint,
                                 //CON_SYM_Name = k.SYM_Symptoms,
                                 CON_Height = l.PA_Height,
                                 CON_Weight = l.PA_Weight,
                                 CON_TempInFahrenheit = l.PA_TempInFahrenheit,
                                 CON_TempInCelsius = l.PA_TempInCelsius,
                                 CON_BloodPressure = l.PA_BloodPressure,
                                 CON_Sugar = l.PA_Sugar,
                                 CON_RespiratoryRate = l.PA_RespiratoryRate,
                                 CON_PulseRate = l.PA_PulseRate,
                                 CON_ECG = l.PA_ECG,
                                 CON_OxygenSaturation = l.PA_OxygenSaturation,
                                 Inactive = a.Inactive,
                                 delete_flag = a.delete_flag,
                                 status = a.status

                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
