using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GlobalApi.Repository.MasterRepository
{
    public class ReferralsRepository : IReferrals
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        private Consult_Complaint_DTLRepository consult_Complaint_DTLRepository;
        private Consult_Symptoms_DTLRepository consult_Symptoms_DTLRepository;
        private Consult_Diseases_DTLRepository consult_Diseases_DTLRepository;
        private Consult_AllergySigns_DTLRepository consult_AllergySigns_DTLRepository;

        public readonly FindUserId findUserId;

        public ReferralsRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            this.consult_Complaint_DTLRepository = new Consult_Complaint_DTLRepository();
            this.consult_Symptoms_DTLRepository = new Consult_Symptoms_DTLRepository();
            this.consult_Diseases_DTLRepository = new Consult_Diseases_DTLRepository();
            this.consult_AllergySigns_DTLRepository = new Consult_AllergySigns_DTLRepository();
            this.findUserId = new FindUserId();

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
                        Hos_Id = lead.Hos_Id,
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
                                 join c in db.Hospital on a.Hos_Id equals c.Hos_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Status on a.Status equals d.sts_id
                                 orderby a.Ref_Id descending
                                 select new GetReferrals
                                 {
                                     Ref_Id = a.Ref_Id,
                                     CON_Id = a.CON_Id,
                                     DO_Id = a.DO_Id,
                                     DO_Name = string.Concat(b.DO_FirstName, b.DO_LastName),
                                     Hos_Id = a.Hos_Id,
                                     Hos_Name = c.Hos_HospitalName,
                                     Ref_Date = a.Ref_Date,
                                     SplObs = a.SplObs,
                                     Remarks = a.Remarks,
                                     Delete_flag = a.Delete_flag,
                                     Status = a.Status,
                                     sts_name = d.sts_name,
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
        public async Task<Referrals> ApproveReferrals(ApprvReferrals lead)
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
                        var Refrls = await (from a in db.Referrals
                                            where a.Ref_Id == lead.Ref_Id
                                            select a).FirstOrDefaultAsync();
                        var Consltn = await (from b in db.Consultation
                                             where b.CON_Id == Refrls.CON_Id
                                             select b).FirstOrDefaultAsync();
                        var Doc = await (from c in db.Doctor
                                         where c.DO_Id == Refrls.DO_Id
                                         select c).FirstOrDefaultAsync();
                        if(Consltn.CON_APPT_Id_FK != null)
                        {
                            int pkId = await primarykeyvalue.primary_key("PatientAppointment");
                            AppointmentModel apptmod = new AppointmentModel()
                            {
                                Appt_Id = pkId,
                                Appt_PatientId_FK = Consltn.CON_PR_Id_FK,
                                CD_Id = Doc.DO_CD_Id_FK,
                                Appt_DO_Id_FK = Refrls.DO_Id,
                                Appt_DateTime = DateTime.Now,
                                Select_day = Refrls.Ref_Date,
                                Select_FrmTime = DateTime.ParseExact(lead.Select_FrmTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                                Select_toTime = DateTime.ParseExact(lead.Select_toTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                                Appt_Is_active = 1,
                                Appt_Type = "REFERRALS",
                                Assi_Id = 72,
                                UnderBPMedication = Consltn.UnderBPMedication,
                                UnderSugarMedication = Consltn.UnderSugarMedication,
                                Ref_Id_FK = lead.Ref_Id,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                                status = 1

                            };
                            var result1 = await db.PatientAppointment.AddAsync(apptmod);
                            await db.SaveChangesAsync();
                            List<Consult_Complaint_DTL> AlreadyExistsComplaint = await consult_Complaint_DTLRepository.GetExistsConsult_Complaint_DTL(result.CON_Id);
                            foreach (var d in AlreadyExistsComplaint)
                            {
                                var res = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == d.Cmst_Id && x.Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Complaint");
                                    Complaint obj = new Complaint()
                                    {
                                        CPT_Id = id,
                                        Cmst_Id = d.Cmst_Id,
                                        Appt_Id = pkId,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Complaint.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            List<Consult_Symptoms_DTL> AlreadyExistsSymptoms = await consult_Symptoms_DTLRepository.GetExistsConsult_Symptoms_DTL(result.CON_Id);
                            foreach (var d in AlreadyExistsSymptoms)
                            {
                                var res = await db.Symptoms.FirstOrDefaultAsync(x => x.Smst_Id == d.Smst_Id && x.Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Symptoms");
                                    Symptoms obj = new Symptoms()
                                    {
                                        SYM_Id = id,
                                        Smst_Id = d.Smst_Id,
                                        Appt_Id = pkId,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Symptoms.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            List<Consult_Diseases_DTL> AlreadyExistsDisease = await consult_Diseases_DTLRepository.GetExistsConsult_Diseases_DTL(result.CON_Id);
                            foreach (var d in AlreadyExistsDisease)
                            {
                                var res = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == d.Id && x.Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("DiseasesDtl");
                                    DiseasesDtl obj = new DiseasesDtl()
                                    {
                                        Ddtl_Id = id,
                                        Id = d.Id,
                                        Appt_Id = pkId,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.DiseasesDtl.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            List<Consult_AllergySigns_DTL> AlreadyExistsAllergySigns = await consult_AllergySigns_DTLRepository.GetExistsAllergySigns(result.CON_Id);
                            foreach (var d in AlreadyExistsAllergySigns)
                            {
                                var res = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == d.Al_Id && x.Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                                    AllergySigns_DTL obj = new AllergySigns_DTL()
                                    {
                                        Ddtl_Id = id,
                                        Al_Id = d.Al_Id,
                                        Appt_Id = pkId,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.AllergySigns_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            var reslt = await db.Consult_Parameters.FirstOrDefaultAsync(x => x.CON_Id == result.CON_Id);
                            if (reslt != null)
                            {
                                int id = await primarykeyvalue.primary_key("Parameters");
                                Parameters insert = new Parameters()
                                {
                                    PA_Id = id,
                                    Appt_Id = pkId,
                                    PA_Code = id <= 09 ? "PA" + '0' + Convert.ToString(id) : "PA" + Convert.ToString(id),
                                    PA_Height = reslt.PA_Height,
                                    PA_Weight = reslt.PA_Weight,
                                    PA_TempInFahrenheit = reslt.PA_TempInFahrenheit,
                                    PA_TempInCelsius = reslt.PA_TempInCelsius,
                                    PA_BloodPressure = reslt.PA_BloodPressure,
                                    PA_Sugar = reslt.PA_Sugar,
                                    PA_ECG = reslt.PA_ECG,
                                    PA_OxygenSaturation = reslt.PA_OxygenSaturation,
                                    PA_PulseRate = reslt.PA_PulseRate,
                                    PA_RespiratoryRate = reslt.PA_RespiratoryRate,
                                    PA_Hemoglobin = reslt.PA_Hemoglobin,
                                    created_by = 1,
                                    created_date = DateTime.Now,
                                    delete_flag = false,
                                    status = 1,
                                };
                                var _new = await db.Parameters.AddAsync(insert);
                                await db.SaveChangesAsync();
                            }

                        }
                        else
                        {
                            int pkId = await primarykeyvalue.primary_key("PHC_Appointment");
                            PHC_Appointment apptmod = new PHC_Appointment()
                            {
                                Phc_Appt_Id = pkId,
                                Appt_PatientId_FK = Consltn.CON_PR_Id_FK,
                                CD_Id = Doc.DO_CD_Id_FK,
                                Appt_DO_Id_FK = Refrls.DO_Id,
                                Hos_Id = 140,
                                Appt_DateTime = DateTime.Now,
                                Select_day = Refrls.Ref_Date,
                                Select_FrmTime = DateTime.ParseExact(lead.Select_FrmTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                                Select_toTime = DateTime.ParseExact(lead.Select_toTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                                Appt_Is_active = 1,
                                Appt_Type = "REFERRALS",
                                Assi_Id = 72,
                                UnderBPMedication = Consltn.UnderBPMedication,
                                UnderSugarMedication = Consltn.UnderSugarMedication,
                                Ref_Id_FK = lead.Ref_Id,
                                created_by = 1,
                                created_date = DateTime.Now,
                                delete_flag = false,
                                status = 1

                            };
                            var result1 = await db.PHC_Appointment.AddAsync(apptmod);
                            await db.SaveChangesAsync();
                            List<Consult_Complaint_DTL> AlreadyExistsComplaint = await consult_Complaint_DTLRepository.GetExistsConsult_Complaint_DTL(result.CON_Id);
                            foreach (var d in AlreadyExistsComplaint)
                            {
                                var res = await db.Complaint.FirstOrDefaultAsync(x => x.Cmst_Id == d.Cmst_Id && x.Phc_Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Complaint");
                                    Complaint obj = new Complaint()
                                    {
                                        CPT_Id = id,
                                        Cmst_Id = d.Cmst_Id,
                                        Phc_Appt_Id = pkId,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Complaint.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            List<Consult_Symptoms_DTL> AlreadyExistsSymptoms = await consult_Symptoms_DTLRepository.GetExistsConsult_Symptoms_DTL(result.CON_Id);
                            foreach (var d in AlreadyExistsSymptoms)
                            {
                                var res = await db.Symptoms.FirstOrDefaultAsync(x => x.Smst_Id == d.Smst_Id && x.Phc_Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("Symptoms");
                                    Symptoms obj = new Symptoms()
                                    {
                                        SYM_Id = id,
                                        Smst_Id = d.Smst_Id,
                                        Phc_Appt_Id = pkId,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.Symptoms.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            List<Consult_Diseases_DTL> AlreadyExistsDisease = await consult_Diseases_DTLRepository.GetExistsConsult_Diseases_DTL(result.CON_Id);
                            foreach (var d in AlreadyExistsDisease)
                            {
                                var res = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Id == d.Id && x.Phc_Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("DiseasesDtl");
                                    DiseasesDtl obj = new DiseasesDtl()
                                    {
                                        Ddtl_Id = id,
                                        Id = d.Id,
                                        Phc_Appt_Id = pkId,
                                        //Remarks = a.Remarks,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.DiseasesDtl.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            List<Consult_AllergySigns_DTL> AlreadyExistsAllergySigns = await consult_AllergySigns_DTLRepository.GetExistsAllergySigns(result.CON_Id);
                            foreach (var d in AlreadyExistsAllergySigns)
                            {
                                var res = await db.AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == d.Al_Id && x.Phc_Appt_Id == pkId);
                                if (res == null)
                                {
                                    int id = await primarykeyvalue.primary_key("AllergySigns_DTL");
                                    AllergySigns_DTL obj = new AllergySigns_DTL()
                                    {
                                        Ddtl_Id = id,
                                        Al_Id = d.Al_Id,
                                        Phc_Appt_Id = pkId,
                                        created_by = 1,
                                        created_date = DateTime.Now,
                                        delete_flag = false,
                                    };
                                    var result_ = await db.AllergySigns_DTL.AddAsync(obj);
                                    await db.SaveChangesAsync();
                                }
                                else
                                    return null;
                            }

                            var reslt = await db.Consult_Parameters.FirstOrDefaultAsync(x => x.CON_Id == result.CON_Id);
                            if (reslt != null)
                            {
                                int id = await primarykeyvalue.primary_key("Parameters");
                                Parameters insert = new Parameters()
                                {
                                    PA_Id = id,
                                    Phc_Appt_Id = pkId,
                                    PA_Code = id <= 09 ? "PA" + '0' + Convert.ToString(id) : "PA" + Convert.ToString(id),
                                    PA_Height = reslt.PA_Height,
                                    PA_Weight = reslt.PA_Weight,
                                    PA_TempInFahrenheit = reslt.PA_TempInFahrenheit,
                                    PA_TempInCelsius = reslt.PA_TempInCelsius,
                                    PA_BloodPressure = reslt.PA_BloodPressure,
                                    PA_Sugar = reslt.PA_Sugar,
                                    PA_ECG = reslt.PA_ECG,
                                    PA_OxygenSaturation = reslt.PA_OxygenSaturation,
                                    PA_PulseRate = reslt.PA_PulseRate,
                                    PA_RespiratoryRate = reslt.PA_RespiratoryRate,
                                    PA_Hemoglobin = reslt.PA_Hemoglobin,
                                    created_by = 1,
                                    created_date = DateTime.Now,
                                    delete_flag = false,
                                    status = 1,
                                };
                                var _new = await db.Parameters.AddAsync(insert);
                                await db.SaveChangesAsync();
                            }

                        }
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
    }
}
