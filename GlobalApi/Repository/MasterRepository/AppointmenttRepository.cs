using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Globalization;

namespace GlobalApi.Repository.MasterRepository
{
    public class AppointmenttRepository : IAppointment
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private ComplaintRepository complaintRepository;
        private SymptomsRepository symptomsRepository;
        private DiseasesDtlRepository diseasesDtlRepository;
        private AllergySigns_DTLRepository allergySigns_DTLRepository;
        //private PatientHealthRecordsRepository patientHealthRecordsRepository;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly NotificationRepository notificationRepository;
        public readonly FindUserId findUserId;

        public AppointmenttRepository()
        {
            this.db = new GlobalContext();
            ado_Configurations = new ADO_Configrations();
            this.complaintRepository = new ComplaintRepository();
            this.symptomsRepository = new SymptomsRepository();
            this.diseasesDtlRepository = new DiseasesDtlRepository();
            this.allergySigns_DTLRepository = new AllergySigns_DTLRepository();
            //this.patientHealthRecordsRepository = new PatientHealthRecordsRepository();
            primarykeyvalue = new Primarykeyvalue();
            notificationRepository = new NotificationRepository();
            this.findUserId = new FindUserId();
        }
        public async Task<AppointmentModel> InsertAppointment(InsertDetails lead, int Appt_PatientId,string UserId)
        {

            try
            {
                var b = (from a in db.PatientAppointment
                         where a.Appt_PatientId_FK == lead.Appt_PatientId_FK
                         select a.Appt_PatientId_FK).FirstOrDefault();
                var PatientName = db.Patient.SingleOrDefault(x => x.PR_Id == Appt_PatientId);
                var DoctorName = db.Doctor.SingleOrDefault(x => x.DO_Id == lead.Appt_DO_Id_FK);
                var DoctorDetails =await db.Doctor.FirstOrDefaultAsync(x => x.DO_Id == lead.Appt_DO_Id_FK);
                var datet = DateTime.Parse(lead.Select_day);
                var datetim = datet.ToString("yyyy-MM-dd");
                if (b == null)
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");

                   AppointmentModel obj = new AppointmentModel()  
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = Appt_PatientId,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = DateTime.Now,
                        Select_day = datetim,
                        Select_FrmTime = DateTime.ParseExact(lead.Select_FrmTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Select_toTime = DateTime.ParseExact(lead.Select_toTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Appt_Is_active = 1,
                        Appt_Type = "FRESH",
                        Assi_Id = lead.Assi_Id!=null?lead.Assi_Id :0,
                        UnderBPMedication = lead.UnderBPMedication,
                        UnderSugarMedication = lead.UnderSugarMedication,
                        //Ref_Id_FK = lead.Ref_Id_FK != null ? lead.Ref_Id_FK : 0,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var COMPT = await complaintRepository.InsertComplaint(lead.Complaint, id);
                    var SYMPT = await symptomsRepository.InsertSymptoms(lead.Symptoms, id);
                    var DDTL = await diseasesDtlRepository.InsertDiseasesDtl(lead.DiseasesDtl, id);
                    var AL = await allergySigns_DTLRepository.InsertAllergySigns_DTL(lead.AllergySigns_DTL,id);
                    //if(lead.PHR_Doc.Any( x => x.Choose_Document != null))
                    //{
                    //    var PHRs = await patientHealthRecordsRepository.InsertPatientHealthRecords(lead.PHR_Doc, id);
                    //}
                    int _pkid2 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj3 = new Parameters();
                    obj3.PA_Id = _pkid2;
                    obj3.Appt_Id = id;
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
                    obj3.PA_Hemoglobin = lead.Hemoglobin;
                    obj3.PA_UserId_FK = lead.UserId_FK;
                    obj3.created_by = 1;
                    obj3.created_date = DateTime.Now;
                    obj3.delete_flag = false;
                    obj3.status = 1;

                    var result1 = await db.Parameters.AddAsync(obj3);
                    await db.SaveChangesAsync();

                    //await InsertPatientDocument(lead,obj.Appt_Id);
                    await InsertUsers(obj);
                    //await InsertConsultation(obj);

                    var NotificationSendToPatient = await notificationRepository.InsertNotification("New Appointment fixed with DR" + DoctorName.DO_FirstName, "Your Appointment fix at "+ Convert.ToString(DateTime.Now),true, UserId);
                    var NotificationSendToDoctor = await notificationRepository.InsertNotification("New Appointment fixed with Patient" + PatientName.PR_FirstName, "Your Appointment fix at " + Convert.ToString(DateTime.Now), true, DoctorDetails.UserId);
                    return result.Entity;

                }
                else
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = lead.Appt_PatientId_FK,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = DateTime.Now,
                        Select_day = datetim,
                        //Select_Time = lead.Select_Time,
                        Select_FrmTime = DateTime.ParseExact(lead.Select_FrmTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        Select_toTime = DateTime.ParseExact(lead.Select_toTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "REVISIT",
                        Assi_Id = lead.Assi_Id != null ? lead.Assi_Id : 0,
                        //Ref_Id_FK = lead.Ref_Id_FK != null ? lead.Ref_Id_FK : 0,
                        //Dis_id = lead.Dis_id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var COMPT = await complaintRepository.InsertComplaint(lead.Complaint, id);
                    var SYMPT = await symptomsRepository.InsertSymptoms(lead.Symptoms, id);
                    var DDTL = await diseasesDtlRepository.InsertDiseasesDtl(lead.DiseasesDtl, id);
                    var AL = await allergySigns_DTLRepository.InsertAllergySigns_DTL(lead.AllergySigns_DTL, id);
                    //var PARA = await parametersRepository.InsertParameters(lead.Parameters, id);
                    //var list2 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();
                    int _pkid3 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj4 = new Parameters();
                    obj4.PA_Id = _pkid3;
                    obj4.Appt_Id = id;
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
                    obj4.PA_Hemoglobin = lead.Hemoglobin;
                    obj4.PA_UserId_FK = lead.UserId_FK;
                    obj4.created_by = 1;
                    obj4.created_date = DateTime.Now;
                    obj4.delete_flag = false;
                    obj4.status = 1;
                    var result1 = await db.Parameters.AddAsync(obj4);
                    var notification=
                    await db.SaveChangesAsync();

                    //await InsertPatientDocument(lead, obj.Appt_Id);
                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
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
        //public async Task<string> InsertPatientDocument(InsertDetails lead, int Appt_Id)
        //{
        //    try
        //    {
        //        if(lead.Choose_Document.Length <= 3 )
        //        {
        //            foreach (var PDoc in lead.Choose_Document)
        //            {
        //                //var duplicate = await db.PatientDocument.FirstOrDefaultAsync(x => x.PR_Id_FK == lead.Appt_PatientId_FK
        //                //    && x.Doc_Type_Id_FK == lead.doc_type);
        //                //if (duplicate == null)
        //                //{
        //                int id = await primarykeyvalue.primary_key("PatientDocument");
        //                string uniqueFilename = ProcessUploadedFile(PDoc);
        //                PatientDocument obj = new PatientDocument()
        //                {
        //                    Doc_Id = id,
        //                    PR_Id_FK = lead.Appt_PatientId_FK,
        //                    Appt_Id_Fk = Appt_Id,
        //                    Doc_Type_Id_FK = 1,//modify
        //                    Choose_Document = uniqueFilename,
        //                    Doc_UserId_FK = 1,//modify
        //                    created_by = 1,
        //                    created_date = DateTime.Now,
        //                    delete_flag = false,
        //                    status = 1
        //                };
        //                var result = await db.PatientDocument.AddAsync(obj);
        //                await db.SaveChangesAsync();
        //                //}
        //                //else
        //                //    return "Data already inserted";

        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //        return "Record insert successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //Inserting PatientDocuments
        //private string ProcessUploadedFile(IFormFile Choose_Document)
        //{
        //    string uniqueFileName = null;


        //    if (Choose_Document != null)
        //    {
        //        string uploadsFolder = Path.Combine("wwwroot/PatientDocuments");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + Choose_Document.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            Choose_Document.CopyTo(fileStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}

        public async Task<UsersLists> InsertUsers(AppointmentModel lead)
        {
            try
            {
                int _id = await primarykeyvalue.primary_key("UsersLists");
                UsersLists insert = new UsersLists()
                {
                    Id = _id,
                    User_cat = "PatientAppointment",
                    User_ref_id = lead.Appt_Id,
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
        public async Task<AppointmentModel> ApproveAppointment(int Appt_Id , string CON_ConsultedDate, string CON_ConsultedTime , string Remarks)
        {
            try
            {
                var result = await db.PatientAppointment.Where(x => x.Appt_Id == Appt_Id).FirstOrDefaultAsync();
                var datet = DateTime.Parse(CON_ConsultedDate);
                var datetim = datet.ToString("yyyy-MM-dd");
                if (result != null)
                {
                    result.Appt_Id = Appt_Id;
                    //result.Doctor_approval_status = 2;
                    result.status = 3;
                    result.Remarks = Remarks;
                    await db.SaveChangesAsync();
                    if (result.status == 3)
                    {
                        int pkId = await primarykeyvalue.primary_key("Consultation");
                        var doct = (from a in db.Doctor
                                    where a.DO_Id == result.Appt_DO_Id_FK
                                    //orderby a.DO_Id ascending
                                    select a.DO_HO_Id_FK).FirstOrDefault();
                        var spec = (from a in db.Doctor
                                    where a.DO_Id == result.Appt_DO_Id_FK
                                    //orderby a.DO_Id ascending
                                    select a.DO_SP_Id_FK).FirstOrDefault();
                        Consultation savechanges = new Consultation()
                        {
                            CON_Id = pkId,
                            CON_Code = pkId <= 09 ? "CON" + '0' + Convert.ToString(pkId) : "CON" + Convert.ToString(pkId),
                            CON_Type = result.Appt_Type,
                            CON_APPT_Id_FK = result.Appt_Id,
                            CON_PR_Id_FK = result.Appt_PatientId_FK,
                            CON_DO_Id_FK = result.Appt_DO_Id_FK,
                            CON_CD_Id_FK = result.CD_Id,
                            CON_SP_Id_FK = spec,
                            CON_HO_Id_FK = doct,
                            CON_Ref_AS_Id = result.Assi_Id,
                            CON_ConsultedDate = datetim,
                            CON_ConsultedTime = DateTime.ParseExact(CON_ConsultedTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                            Inactive = "N",
                            delete_flag = false,
                            status = 1,
                            Remarks = Remarks,
                            
                        };
                        var _new1 = await db.Consultation.AddAsync(savechanges);
                        await db.SaveChangesAsync();
                        //return _new1.Entity;
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
        public async Task<AppointmentModel> RejectAppointment(int Appt_Id)
        {
            try
            {
                var result = await db.PatientAppointment.FirstOrDefaultAsync(x => x.Appt_Id == Appt_Id);
                if (result != null)
                {
                    result.Appt_Id = Appt_Id;
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
        public async Task<string> UpdateAppointment(InsertDetails lead)
        {
            try
            {
                if (lead.status != 3)
                {
                    var result = await db.PatientAppointment.FirstOrDefaultAsync(x => x.Appt_Id == lead.Appt_Id);
                    if (result != null)
                    {
                        result.Appt_Id = lead.Appt_Id;
                        result.Appt_PatientId_FK = lead.Appt_PatientId_FK;
                        result.CD_Id = lead.CD_Id;
                        result.Appt_DO_Id_FK = lead.Appt_DO_Id_FK;
                        result.Appt_DateTime = lead.Appt_DateTime;
                        result.Select_day = lead.Select_day;
                        //result.Select_Time = lead.Select_Time;
                        result.Select_FrmTime = lead.Select_FrmTime;
                        result.Select_toTime = lead.Select_toTime;
                        //result.Doctor_approval_status = 0;
                        result.Appt_Is_active = 1;
                        result.Appt_Type = "FRESH";
                        result.Assi_Id = lead.Assi_Id;
                        result.UnderBPMedication = lead.UnderBPMedication;
                        result.UnderSugarMedication = lead.UnderSugarMedication;
                        //result.Dis_id = lead.Dis_id;
                        result.modified_by = 2;
                        result.modified_date = DateTime.Now;
                        result.delete_flag = false;
                        result.status = 2;
                        await db.SaveChangesAsync();
                        var COMPT = await complaintRepository.UpdateComplainttest(lead.Complaint, lead.Appt_Id);
                        var SYMPT = await symptomsRepository.UpdateSymptomstest(lead.Symptoms, lead.Appt_Id);
                        var DDTL = await diseasesDtlRepository.UpdateDiseasesDtltest(lead.DiseasesDtl, lead.Appt_Id);
                        var AL = await allergySigns_DTLRepository.UpdateAllergySigns_DTLtest(lead.AllergySigns_DTL, lead.Appt_Id);

                        await UpdateParameters(lead);
                        //return result;
                        //var list1 = (from a in db.Parameters where a.PA_APPT_Id_FK == lead.Appt_Id select a.PA_Id).FirstOrDefaultAsync();
                        //Parameters obj3 = new Parameters();
                        //obj3.PA_Id = await list1;
                        //obj3.PA_APPT_Id_FK = lead.Appt_Id;
                        //obj3.PA_Height = lead.Height;
                        //obj3.PA_Weight = lead.Weight;
                        //obj3.PA_TempInFahrenheit = lead.TempInFahrenheit;
                        //obj3.PA_TempInCelsius = lead.TempInCelsius;
                        //obj3.PA_BloodPressure = lead.BloodPressure;
                        //obj3.PA_Sugar = lead.Sugar;
                        //obj3.PA_ECG = lead.ECG;
                        //obj3.PA_OxygenSaturation = lead.OxygenSaturation;
                        //obj3.PA_PulseRate = lead.PulseRate;
                        //obj3.PA_RespiratoryRate = lead.RespiratoryRate;
                        //obj3.PA_UserId_FK = lead.UserId_FK;
                        //obj3.modified_by = 2;
                        //obj3.modified_date = DateTime.Now;
                        //obj3.delete_flag = false;
                        //obj3.status = 2;

                        ////var result1 = await db.Parameters.UpdateAsync(obj3);
                        //await db.SaveChangesAsync();
                        
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
        public async Task<Parameters> UpdateParameters(InsertDetails lead)
        {
            try
            {
                var result = await db.Parameters.FirstOrDefaultAsync(x => x.Appt_Id == lead.Appt_Id);
                var list = (from a in db.Parameters where a.Appt_Id == lead.Appt_Id select a.PA_Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.PA_Id = await list;
                    //result.PA_Code = lead.PA_Code;
                    result.Appt_Id = lead.Appt_Id;
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
                    result.PA_Hemoglobin = lead.Hemoglobin;
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
        //public async Task<Consultation> UpdateConsultation(AppointmentModel lead)
        //{
        //    var result = await db.Consultation.FirstOrDefaultAsync(x => x.CON_Id == lead.Appt_Id);
        //    var doct = (from a in db.Doctor
        //                where a.DO_Id == lead.Appt_DO_Id_FK
        //                //orderby a.DO_Id ascending
        //                select a.DO_HO_Id_FK).FirstOrDefault();
        //    var spec = (from a in db.Doctor
        //                where a.DO_Id == lead.Appt_DO_Id_FK
        //                //orderby a.DO_Id ascending
        //                select a.DO_SP_Id_FK).FirstOrDefault();
        //    //var cd = (from a in db.Doctor
        //    //          where a.DO_Id == lead.Appt_DO_Id_FK
        //    //          select a.DO_CD_Id_FK).FirstOrDefault();
        //    if (result != null)
        //    {
        //        result.CON_Id = lead.Appt_Id;
        //        result.CON_Type = lead.Appt_Type;
        //        result.CON_APPT_Id_FK = lead.Appt_Id;
        //        result.CON_PR_Id_FK = lead.Appt_PatientId_FK;
        //        result.CON_DO_Id_FK = lead.Appt_DO_Id_FK;
        //        result.CON_CD_Id_FK = lead.CD_Id;
        //        //result.CON_CD_Id_FK = cd;
        //        result.CON_Ref_AS_Id = lead.Assi_Id;
        //        result.CON_SP_Id_FK = spec;
        //        result.CON_HO_Id_FK = doct;
        //        //result.Dis_Id_FK = lead.Dis_id;
        //        result.Inactive = "N";
        //        result.modified_by = 2;
        //        result.modified_date = DateTime.Now;
        //        result.delete_flag = false;
        //        result.status = 2;
        //        await db.SaveChangesAsync();
        //        return result;

        //    }
        //    return null;

        //}
        public async Task<List<GetAllAppointmentModel>> GetAllAppointment(int? HospitalId, string roleaction)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.PatientAppointment
                                 join b in db.Patient on a.Appt_PatientId_FK equals b.PR_Id 
                                 join c in db.Discipline on a.CD_Id equals c.CD_Id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Doctor on a.Appt_DO_Id_FK equals d.DO_Id
                                 join z in db.Hospital on d.DO_HO_Id_FK equals z.Hos_Id 
                                 join e in db.Parameters on a.Appt_Id equals e.Appt_Id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Assistant on a.Assi_Id equals f.Assi_Id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join n in db.Status on a.status equals n.sts_id into nlist
                                 from n in nlist.DefaultIfEmpty()
                                 join o in db.States on b.PR_S_Id_FK equals o.stat_id into olist
                                 from o in olist.DefaultIfEmpty()
                                 join m in db.Districts on b.PR_D_Id_FK equals m.district_id into mlist
                                 from m in mlist.DefaultIfEmpty()
                                 join s in db.Language_MST on b.PR_MotherTongue equals s.Id
                                 where roleaction == "Hospital" ? z.Hos_Id == HospitalId : a.Appt_Id > 0
                                 orderby a.Appt_Id descending
                                 select new GetAllAppointmentModel()
                                 {
                                     Appt_Id = a.Appt_Id,
                                     Appt_PatientId_FK = a.Appt_PatientId_FK,
                                     Appt_P_Code = b.PR_PatientCode,
                                     Appt_P_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                     Appt_P_Age = b.PR_Age,
                                     Appt_P_Gender = b.PR_Gender,
                                     Appt_P_BloodGroup = b.PR_BloodGroup,
                                     Appt_P_MotherTounge = b.PR_MotherTongue,
                                     Language = s.Language,
                                     PR_Photobyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                               System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                               System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                                     PatientLocation = m.district_name,
                                     complaintslist = (from g in db.Complaint
                                                       join h in db.ComplaintMst on g.Cmst_Id equals h.Cmst_Id
                                                       where g.Appt_Id == a.Appt_Id
                                                       select new GetAllComplaint()
                                                       {
                                                           //CPT_Id = g.CPT_Id,
                                                           Cmst_Id = g.Cmst_Id,
                                                           Cmst_Name = h.Cmst_Name,
                                                           //CPT_APPT_Id_FK = g.CPT_APPT_Id_FK,
                                                           //Remarks = g.Remarks,
                                                           //delete_flag = g.delete_flag
                                                       }).ToList(),
                                     symptomslist = (from i in db.Symptoms
                                                     join j in db.SymptomsMst on i.Smst_Id equals j.Smst_Id
                                                     where i.Appt_Id == a.Appt_Id
                                                     select new GetAllSymptoms()
                                                     {
                                                         //SYM_Id = i.SYM_Id,
                                                         Smst_Id = i.Smst_Id,
                                                         Smst_Name = j.Smst_Name,
                                                         //SYM_APPT_Id_FK = i.SYM_APPT_Id_FK,
                                                         //Remarks = i.Remarks,
                                                         //delete_flag=i.delete_flag,
                                                     }).ToList(),
                                     diseaseslist = (from k in db.DiseasesDtl
                                                     join l in db.Diseases on k.Id equals l.Id
                                                     where k.Appt_Id == a.Appt_Id
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         //Ddtl_Id = k.Ddtl_Id,
                                                         Id = k.Id,
                                                         Diseases_Name = l.Diseases_Name,
                                                         //Ddtl_APPT_Id_FK = k.Ddtl_APPT_Id_FK,
                                                         //Remarks = k.Remarks,
                                                         //delete_flag = k.delete_flag,
                                                     }).ToList(),
                                     Allergylist = (from p in db.AllergySigns_DTL
                                                     join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                     where p.Appt_Id == a.Appt_Id
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
                                     Appt_PA_Hemoglobin = e.PA_Hemoglobin,
                                     CD_Id = a.CD_Id,
                                     CD_Name = c.CD_ClinicalDiscipline,
                                     Appt_DO_Id_FK = a.Appt_DO_Id_FK,
                                     Appt_DO_Name = string.Concat(d.DO_FirstName, d.DO_LastName),
                                     Appt_DateTime = a.Appt_DateTime,
                                     Select_day = Convert.ToString(Convert.ToDateTime(a.Select_day).DayOfWeek),
                                     Select_FrmTime = a.Select_FrmTime,
                                     Select_toTime =a.Select_toTime,
                                     //Doctor_approval_status = a.Doctor_approval_status,
                                     Appt_Is_active = a.Appt_Is_active,
                                     Appt_Type = a.Appt_Type,
                                     Assi_Id = a.Assi_Id,
                                     Appt_Assi_Name = string.Concat(f.Assi_FirstName, f.Assi_LastName),
                                     Ref_Id_FK = a.Ref_Id_FK,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     status_name = n.sts_name,
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
        public async Task<AppointmentModel> DeleteAppointment(int Appt_Id)
        {
            try
            {
                var result = await db.PatientAppointment.FirstOrDefaultAsync(x => x.Appt_Id == Appt_Id);
                if (result != null)
                {
                    result.Appt_Id = Appt_Id;
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
        public async Task<List<AppointmentModelById>> GetAppointmentById(int Appt_PatientId_FK)
        {
            try 
            { 

            if (db != null)
            {
                var query = (from a in db.PatientAppointment
                             join b in db.Patient on a.Appt_PatientId_FK equals b.PR_Id
                             join c in db.Discipline on a.CD_Id equals c.CD_Id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Doctor on a.Appt_DO_Id_FK equals d.DO_Id
                             join e in db.Parameters on a.Appt_Id equals e.Appt_Id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Assistant on a.Assi_Id equals f.Assi_Id into flist
                             from f in flist.DefaultIfEmpty()
                             join n in db.Status on a.status equals n.sts_id into nlist
                             from n in nlist.DefaultIfEmpty()
                             join o in db.States on b.PR_S_Id_FK equals o.stat_id into olist
                             from o in olist.DefaultIfEmpty()
                             join m in db.Districts on b.PR_D_Id_FK equals m.district_id into mlist
                             from m in mlist.DefaultIfEmpty()
                             join s in db.Language_MST on b.PR_MotherTongue equals s.Id
                             where a.Appt_PatientId_FK == Appt_PatientId_FK
                             orderby a.Appt_Id descending
                             select new AppointmentModelById()
                             {
                                 Appt_Id = a.Appt_Id,
                                 Appt_PatientId_FK = a.Appt_PatientId_FK,
                                 Appt_P_Code = b.PR_PatientCode,
                                 Appt_P_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                 Appt_P_Age = b.PR_Age,
                                 Appt_P_Gender = b.PR_Gender,
                                 Appt_P_BloodGroup = b.PR_BloodGroup,
                                 Appt_P_MotherTounge = b.PR_MotherTongue,
                                 Language = s.Language,
                                 PR_Photobyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                               System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                               System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                                 PatientLocation = m.district_name,
                                 complaintslist = (from g in db.Complaint
                                                   join h in db.ComplaintMst on g.Cmst_Id equals h.Cmst_Id
                                                   where g.Appt_Id == a.Appt_Id
                                                   select new GetAllComplaint()
                                                   {
                                                       Cmst_Id = g.Cmst_Id,
                                                       Cmst_Name = h.Cmst_Name,
                                                   }).ToList(),
                                 symptomslist = (from i in db.Symptoms
                                                 join j in db.SymptomsMst on i.Smst_Id equals j.Smst_Id
                                                 where i.Appt_Id == a.Appt_Id
                                                 select new GetAllSymptoms()
                                                 {
                                                     Smst_Id = i.Smst_Id,
                                                     Smst_Name = j.Smst_Name,
                                                 }).ToList(),
                                 diseaseslist = (from k in db.DiseasesDtl
                                                 join l in db.Diseases on k.Id equals l.Id
                                                 where k.Appt_Id == a.Appt_Id
                                                 select new GetAllDiseasesDtl()
                                                 {
                                                     Id = k.Id,
                                                     Diseases_Name = l.Diseases_Name,
                                                 }).ToList(),
                                 Allergylist = (from p in db.AllergySigns_DTL
                                                join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                where p.Appt_Id == a.Appt_Id
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
                                 Appt_PA_Hemoglobin = e.PA_Hemoglobin,
                                 CD_Id = a.CD_Id,
                                 CD_Name = c.CD_ClinicalDiscipline,
                                 Appt_DO_Id_FK = a.Appt_DO_Id_FK,
                                 Appt_DO_Name = string.Concat(d.DO_FirstName, d.DO_LastName),
                                 Appt_DateTime = a.Appt_DateTime,
                                 Select_day = Convert.ToString(Convert.ToDateTime(a.Select_day).DayOfWeek),
                                 Select_FrmTime = DateTime.ParseExact(a.Select_FrmTime, "hh:mm tt", CultureInfo.CurrentCulture).ToString("HH:mm"),
                                 Select_toTime = DateTime.ParseExact(a.Select_toTime, "hh:mm tt", CultureInfo.CurrentCulture).ToString("HH:mm"),
                                 //Doctor_approval_status = a.Doctor_approval_status,
                                 Appt_Is_active = a.Appt_Is_active,
                                 Appt_Type = a.Appt_Type,
                                 Assi_Id = a.Assi_Id,
                                 Appt_Assi_Name = string.Concat(f.Assi_FirstName, f.Assi_LastName),
                                 Ref_Id_FK = a.Ref_Id_FK,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 status_name = n.sts_name,
                                 Remarks = a.Remarks,
                             }).ToListAsync();
                return await query;
            }
            return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<AppointmentModelById>> GetAdminAppointmentById(int Appt_Id)
        {
            if (db != null)
            {
                var query = (from a in db.PatientAppointment
                             join b in db.Patient on a.Appt_PatientId_FK equals b.PR_Id
                             join c in db.Discipline on a.CD_Id equals c.CD_Id into D
                             from c in D.DefaultIfEmpty()
                             join d in db.Doctor on a.Appt_DO_Id_FK equals d.DO_Id
                             join e in db.Parameters on a.Appt_Id equals e.Appt_Id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Assistant on a.Assi_Id equals f.Assi_Id into flist
                             from f in flist.DefaultIfEmpty()
                             join n in db.Status on a.status equals n.sts_id into nlist
                             from n in nlist.DefaultIfEmpty()
                             join o in db.States on b.PR_S_Id_FK equals o.stat_id into olist
                             from o in olist.DefaultIfEmpty()
                             join m in db.Districts on b.PR_D_Id_FK equals m.district_id into mlist
                             from m in mlist.DefaultIfEmpty()
                             join s in db.Language_MST on b.PR_MotherTongue equals s.Id
                             where a.Appt_Id == Appt_Id
                             orderby a.Appt_Id descending
                             select new AppointmentModelById()
                             {
                                 Appt_Id = a.Appt_Id,
                                 Appt_PatientId_FK = a.Appt_PatientId_FK,
                                 Appt_P_Code = b.PR_PatientCode,
                                 Appt_P_Name = string.Concat(b.PR_FirstName, b.PR_LastName),
                                 Appt_P_Age = b.PR_Age,
                                 Appt_P_Gender = b.PR_Gender,
                                 Appt_P_BloodGroup = b.PR_BloodGroup,
                                 Appt_P_MotherTounge = b.PR_MotherTongue,
                                 Language = s.Language,
                                 PatientLocation = m.district_name,
                                 PR_Photobyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                               System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                               System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                                 complaintslist = (from g in db.Complaint
                                                   join h in db.ComplaintMst on g.Cmst_Id equals h.Cmst_Id
                                                   where g.Appt_Id == a.Appt_Id
                                                   select new GetAllComplaint()
                                                   {
                                                       Cmst_Id = g.Cmst_Id,
                                                       Cmst_Name = h.Cmst_Name,
                                                   }).ToList(),
                                 symptomslist = (from i in db.Symptoms
                                                 join j in db.SymptomsMst on i.Smst_Id equals j.Smst_Id
                                                 where i.Appt_Id == a.Appt_Id
                                                 select new GetAllSymptoms()
                                                 {
                                                     Smst_Id = i.Smst_Id,
                                                     Smst_Name = j.Smst_Name,
                                                 }).ToList(),
                                 diseaseslist = (from k in db.DiseasesDtl
                                                 join l in db.Diseases on k.Id equals l.Id
                                                 where k.Appt_Id == a.Appt_Id
                                                 select new GetAllDiseasesDtl()
                                                 {
                                                     Id = k.Id,
                                                     Diseases_Name = l.Diseases_Name,
                                                 }).ToList(),
                                 Allergylist = (from p in db.AllergySigns_DTL
                                                join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                where p.Appt_Id == a.Appt_Id
                                                select new GetAllAllergySigns_DTL()
                                                {
                                                    Al_Id = p.Al_Id,
                                                    Al_Name = q.Al_Name,
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
                                 Appt_PA_Hemoglobin = e.PA_Hemoglobin,
                                 CD_Id = a.CD_Id,
                                 CD_Name = c.CD_ClinicalDiscipline,
                                 Appt_DO_Id_FK = a.Appt_DO_Id_FK,
                                 Appt_DO_Name = string.Concat(d.DO_FirstName, d.DO_LastName),
                                 Appt_DateTime = a.Appt_DateTime,
                                 Select_day = Convert.ToString(Convert.ToDateTime(a.Select_day).DayOfWeek),
                                 Select_FrmTime = a.Select_FrmTime,
                                 Select_toTime = a.Select_toTime,
                                 //Doctor_approval_status = a.Doctor_approval_status,
                                 Appt_Is_active = a.Appt_Is_active,
                                 Appt_Type = a.Appt_Type,
                                 Assi_Id = a.Assi_Id,
                                 Appt_Assi_Name = string.Concat(f.Assi_FirstName, f.Assi_LastName),
                                 Ref_Id_FK = a.Ref_Id_FK,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 status_name = n.sts_name,
                                 Remarks = a.Remarks,

                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<GetDocDD>> GetDoctorDD(string Select_day, string Select_FrmTime, string Select_toTime)
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
                {
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetDoctorDD_Testing", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Select_day", Convert.ToDateTime(Select_day));
                        cmd.Parameters.AddWithValue("@Select_FrmTime", Convert.ToDateTime(Select_FrmTime));
                        cmd.Parameters.AddWithValue("@Select_toTime", Convert.ToDateTime(Select_toTime));
                        var response = new List<GetDocDD>();
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(GetAllDocDD(reader));
                            }
                        }
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GetDocDD GetAllDocDD(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            return new GetDocDD()
            {
                Appt_DO_Id_FK = Convert.ToInt32(reader["DO_Id_FK"]),
                Doc_Name = Convert.ToString(reader["DO_Name"])
            };
        }
        public async Task<AppointmentModel> InsertApptBasedOnSymptoms(ApptonDiffCategory lead, int Appt_PatientId, int Smst_Id)
        {

            try
            {
                var b = (from a in db.PatientAppointment
                         where a.Appt_PatientId_FK == lead.Appt_PatientId_FK
                         select a.Appt_PatientId_FK).FirstOrDefault();
                if (b == null)
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = Appt_PatientId,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = DateTime.Now,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "FRESH",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    //var list1 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();

                    int _pkid1 = await primarykeyvalue.primary_key("Symptoms");
                    Symptoms obj2 = new Symptoms();
                    obj2.SYM_Id = _pkid1;
                    obj2.Smst_Id = Smst_Id;
                    obj2.Appt_Id = lead.Appt_Id;
                    //obj2.Remarks = "NULL";
                    obj2.created_by = 1;
                    obj2.created_date = DateTime.Now;
                    obj2.delete_flag = false;
                    var result2 = await db.Symptoms.AddAsync(obj2);
                    await db.SaveChangesAsync();

                    int _pkid2 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj3 = new Parameters();
                    obj3.PA_Id = _pkid2;
                    obj3.Appt_Id = lead.Appt_Id;
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
                    obj3.PA_Hemoglobin = lead.Hemoglobin;
                    obj3.PA_UserId_FK = lead.UserId_FK;
                    obj3.created_by = 1;
                    obj3.created_date = DateTime.Now;
                    obj3.delete_flag = false;
                    obj3.status = 1;

                    var result3 = await db.Parameters.AddAsync(obj3);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                else
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = lead.Appt_PatientId_FK,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = lead.Appt_DateTime,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "REVISIT",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    //var list2 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();

                    int _pkid1 = await primarykeyvalue.primary_key("Symptoms");
                    Symptoms obj2 = new Symptoms();
                    obj2.SYM_Id = _pkid1;
                    obj2.Smst_Id = Smst_Id;
                    obj2.Appt_Id = lead.Appt_Id;
                    //obj2.Remarks = "NULL";
                    obj2.created_by = 1;
                    obj2.created_date = DateTime.Now;
                    obj2.delete_flag = false;
                    var result2 = await db.Symptoms.AddAsync(obj2);
                    await db.SaveChangesAsync();


                    int _pkid3 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj4 = new Parameters();
                    obj4.PA_Id = _pkid3;
                    obj4.Appt_Id = lead.Appt_Id;
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
                    obj4.PA_Hemoglobin = lead.Hemoglobin;
                    obj4.PA_UserId_FK = Appt_PatientId;
                    obj4.created_by = 1;
                    obj4.created_date = DateTime.Now;
                    obj4.delete_flag = false;
                    obj4.status = 1;
                    var result1 = await db.Parameters.AddAsync(obj4);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<AppointmentModel> InsertApptBasedOnDisease(ApptonDiffCategory lead, int Appt_PatientId, int Id)
        {

            try
            {
                var b = (from a in db.PatientAppointment
                         where a.Appt_PatientId_FK == lead.Appt_PatientId_FK
                         select a.Appt_PatientId_FK).FirstOrDefault();
                if (b == null)
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = Appt_PatientId,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = DateTime.Now,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "FRESH",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    //var list1 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();

                    int _pkid1 = await primarykeyvalue.primary_key("DiseasesDtl");
                    DiseasesDtl obj2 = new DiseasesDtl();
                    obj2.Ddtl_Id = _pkid1;
                    obj2.Id = Id;
                    obj2.Appt_Id = lead.Appt_Id;
                    obj2.created_by = 1;
                    obj2.created_date = DateTime.Now;
                    obj2.delete_flag = false;
                    var result2 = await db.DiseasesDtl.AddAsync(obj2);
                    await db.SaveChangesAsync();

                    int _pkid2 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj3 = new Parameters();
                    obj3.PA_Id = _pkid2;
                    obj3.Appt_Id = lead.Appt_Id;
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
                    obj3.PA_Hemoglobin = lead.Hemoglobin;
                    obj3.PA_UserId_FK = Appt_PatientId;
                    obj3.created_by = 1;
                    obj3.created_date = DateTime.Now;
                    obj3.delete_flag = false;
                    obj3.status = 1;

                    var result3 = await db.Parameters.AddAsync(obj3);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                else
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = lead.Appt_PatientId_FK,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = lead.Appt_DateTime,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "REVISIT",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    //var list2 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();

                    int _pkid1 = await primarykeyvalue.primary_key("DiseasesDtl");
                    DiseasesDtl obj2 = new DiseasesDtl();
                    obj2.Ddtl_Id = _pkid1;
                    obj2.Id = Id;
                    obj2.Appt_Id = lead.Appt_Id;
                    obj2.created_by = 1;
                    obj2.created_date = DateTime.Now;
                    obj2.delete_flag = false;
                    var result2 = await db.DiseasesDtl.AddAsync(obj2);
                    await db.SaveChangesAsync();


                    int _pkid3 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj4 = new Parameters();
                    obj4.PA_Id = _pkid3;
                    obj4.Appt_Id = lead.Appt_Id;
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
                    obj4.PA_Hemoglobin = lead.Hemoglobin;
                    obj4.PA_UserId_FK = Appt_PatientId;
                    obj4.created_by = 1;
                    obj4.created_date = DateTime.Now;
                    obj4.delete_flag = false;
                    obj4.status = 1;
                    var result1 = await db.Parameters.AddAsync(obj4);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<AppointmentModel> InsertApptBasedOnDoctor(ApptonDoctor lead, int Appt_PatientId, int DO_Id)
        {

            try
            {
                var b = (from a in db.PatientAppointment
                         where a.Appt_PatientId_FK == lead.Appt_PatientId_FK
                         select a.Appt_PatientId_FK).FirstOrDefault();
                if (b == null)
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = Appt_PatientId,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = DO_Id,
                        Appt_DateTime = DateTime.Now,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "FRESH",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var COMPT = await complaintRepository.InsertComplaint(lead.Complaint, id);
                    var SYMPT = await symptomsRepository.InsertSymptoms(lead.Symptoms, id);
                    var DDTL = await diseasesDtlRepository.InsertDiseasesDtl(lead.DiseasesDtl, id);
                    //var list1 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();
                    int _pkid2 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj3 = new Parameters();
                    obj3.PA_Id = _pkid2;
                    obj3.Appt_Id = lead.Appt_Id;
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
                    obj3.PA_Hemoglobin = lead.Hemoglobin;
                    obj3.PA_UserId_FK = Appt_PatientId;
                    obj3.created_by = 1;
                    obj3.created_date = DateTime.Now;
                    obj3.delete_flag = false;
                    obj3.status = 1;

                    var result3 = await db.Parameters.AddAsync(obj3);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                else
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = lead.Appt_PatientId_FK,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = lead.Appt_DateTime,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "REVISIT",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var COMPT = await complaintRepository.InsertComplaint(lead.Complaint, id);
                    var SYMPT = await symptomsRepository.InsertSymptoms(lead.Symptoms, id);
                    var DDTL = await diseasesDtlRepository.InsertDiseasesDtl(lead.DiseasesDtl, id);
                    //var list2 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();
                    int _pkid3 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj4 = new Parameters();
                    obj4.PA_Id = _pkid3;
                    obj4.Appt_Id = lead.Appt_Id;
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
                    obj4.PA_Hemoglobin = lead.Hemoglobin;
                    obj4.PA_UserId_FK = Appt_PatientId;
                    obj4.created_by = 1;
                    obj4.created_date = DateTime.Now;
                    obj4.delete_flag = false;
                    obj4.status = 1;
                    var result1 = await db.Parameters.AddAsync(obj4);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<AppointmentModel> InsertApptBasedOnSpecalization(ApptonSpecalization lead, int Appt_PatientId, int SP_Id)
        {

            try
            {
                var b = (from a in db.PatientAppointment
                         where a.Appt_PatientId_FK == lead.Appt_PatientId_FK
                         select a.Appt_PatientId_FK).FirstOrDefault();
                if (b == null)
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = Appt_PatientId,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = DateTime.Now,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "FRESH",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var COMPT = await complaintRepository.InsertComplaint(lead.Complaint, id);
                    var SYMPT = await symptomsRepository.InsertSymptoms(lead.Symptoms, id);
                    var DDTL = await diseasesDtlRepository.InsertDiseasesDtl(lead.DiseasesDtl, id);

                    //var list1 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();
                    int _pkid2 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj3 = new Parameters();
                    obj3.PA_Id = _pkid2;
                    obj3.Appt_Id = lead.Appt_Id;
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
                    obj3.PA_Hemoglobin = lead.Hemoglobin;
                    obj3.PA_UserId_FK = Appt_PatientId;
                    obj3.created_by = 1;
                    obj3.created_date = DateTime.Now;
                    obj3.delete_flag = false;
                    obj3.status = 1;

                    var result3 = await db.Parameters.AddAsync(obj3);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                else
                {
                    int id = await primarykeyvalue.primary_key("PatientAppointment");
                    AppointmentModel obj = new AppointmentModel()
                    {
                        Appt_Id = id,
                        Appt_PatientId_FK = lead.Appt_PatientId_FK,
                        CD_Id = lead.CD_Id,
                        Appt_DO_Id_FK = lead.Appt_DO_Id_FK,
                        Appt_DateTime = lead.Appt_DateTime,
                        Select_day = lead.Select_day,
                        Select_FrmTime = lead.Select_FrmTime,
                        Select_toTime = lead.Select_toTime,
                        //Doctor_approval_status = 0,
                        Appt_Is_active = 1,
                        Appt_Type = "REVISIT",
                        Assi_Id = lead.Assi_Id,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientAppointment.AddAsync(obj);
                    await db.SaveChangesAsync();
                    //var list2 = (from a in db.PatientAppointment orderby a.Appt_Id descending select a.Appt_Id).FirstOrDefaultAsync();

                    int _pkid3 = await primarykeyvalue.primary_key("Parameters");
                    Parameters obj4 = new Parameters();
                    obj4.PA_Id = _pkid3;
                    obj4.Appt_Id = lead.Appt_Id;
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
                    obj4.PA_Hemoglobin = lead.Hemoglobin;
                    obj4.PA_UserId_FK = Appt_PatientId;
                    obj4.created_by = 1;
                    obj4.created_date = DateTime.Now;
                    obj4.delete_flag = false;
                    obj4.status = 1;
                    var result1 = await db.Parameters.AddAsync(obj4);
                    await db.SaveChangesAsync();

                    await InsertUsers(obj);
                    //await InsertConsultation(obj);
                    return result.Entity;

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetDocDD>> GetDoctorDDOnSpec(int Sp_Id,string Select_day, string Select_FrmTime, string Select_toTime)
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
                {
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetDoctorDD_Spec", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Sp_Id", Sp_Id);
                        cmd.Parameters.AddWithValue("@Select_day", Select_day);
                        cmd.Parameters.AddWithValue("@Select_FrmTime", Select_FrmTime);
                        cmd.Parameters.AddWithValue("@Select_toTime", Select_toTime);
                        var response = new List<GetDocDD>();
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(GetAllDocDD(reader));
                            }
                        }
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
