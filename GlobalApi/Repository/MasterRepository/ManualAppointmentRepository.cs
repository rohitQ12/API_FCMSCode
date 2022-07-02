using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GlobalApi.Repository.MasterRepository
{
    public class ManualAppointmentRepository : IManualAppointment
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private ComplaintRepository complaintRepository;
        private SymptomsRepository symptomsRepository;
        private DiseasesDtlRepository diseasesDtlRepository;
        private AllergySigns_DTLRepository allergySigns_DTLRepository;
        private ParametersRepository parametersRepository;
        //private PatientHealthRecordsRepository patientHealthRecordsRepository;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly NotificationRepository notificationRepository;
        public readonly FindUserId findUserId;

        public ManualAppointmentRepository()
        {
            this.db = new GlobalContext();
            ado_Configurations = new ADO_Configrations();
            this.complaintRepository = new ComplaintRepository();
            this.symptomsRepository = new SymptomsRepository();
            this.diseasesDtlRepository = new DiseasesDtlRepository();
            this.allergySigns_DTLRepository = new AllergySigns_DTLRepository();
            this.parametersRepository = new ParametersRepository();
            //this.patientHealthRecordsRepository = new PatientHealthRecordsRepository();
            primarykeyvalue = new Primarykeyvalue();
            notificationRepository = new NotificationRepository();
            this.findUserId = new FindUserId();
        }
        public async Task<AppointmentModel> InsertAppointment(InsertDetails lead, int Appt_PatientId, string UserId)
        {

            try
            {
                var PatientName = db.Patient.SingleOrDefault(x => x.PR_Id == Appt_PatientId);
                var DoctorName = db.Doctor.SingleOrDefault(x => x.DO_Id == lead.Appt_DO_Id_FK);
                var DoctorDetails = await db.Doctor.FirstOrDefaultAsync(x => x.DO_Id == lead.Appt_DO_Id_FK);
                var datet = DateTime.Parse(lead.Select_day);
                var datetim = datet.ToString("yyyy-MM-dd");
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
                    Assi_Id = lead.Assi_Id != null ? lead.Assi_Id : 0,
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
                var AL = await allergySigns_DTLRepository.InsertAllergySigns_DTL(lead.AllergySigns_DTL, id);
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

                var NotificationSendToPatient = await notificationRepository.InsertNotification("New Appointment fixed with DR" + DoctorName.DO_FirstName, "Your Appointment fix at " + Convert.ToString(DateTime.Now), true, UserId);
                var NotificationSendToDoctor = await notificationRepository.InsertNotification("New Appointment fixed with Patient" + PatientName.PR_FirstName, "Your Appointment fix at " + Convert.ToString(DateTime.Now), true, DoctorDetails.DO_UserId);
                return result.Entity;

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
        public async Task<AppointmentModel> ApproveAppointment(ApproveAppointment lead)
        {
            try
            {
                var result = await db.PatientAppointment.Where(x => x.Appt_Id == lead.Appt_Id).FirstOrDefaultAsync();
                var datet = DateTime.Parse(lead.CON_ConsultedDate);
                var datetim = datet.ToString("yyyy-MM-dd");
                if (result != null)
                {
                    //result.Appt_Id = lead.Appt_Id;
                    result.status = 3;
                    if (lead.Remarks == null)
                    {
                        result.Remarks = "OK";
                    }
                    result.Remarks = lead.Remarks;
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
                        var medi = await (from a in db.PatientAppointment
                                          where a.Appt_Id == lead.Appt_Id
                                          select a).FirstOrDefaultAsync();
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
                            CON_ConsultedTime = DateTime.ParseExact(lead.CON_ConsultedTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt"),
                            UnderBPMedication = medi.UnderBPMedication,
                            UnderSugarMedication = medi.UnderSugarMedication,
                            Inactive = "N",
                            delete_flag = false,
                            status = 1,
                            //Remarks = lead.Remarks,

                        };
                        var _new1 = await db.Consultation.AddAsync(savechanges);
                        await db.SaveChangesAsync();
                        List<Complaint> AlreadyExistsComplaint = await complaintRepository.GetExistsComplaint(lead.Appt_Id);
                        foreach (var d in AlreadyExistsComplaint)
                        {
                            var result1 = await db.Consult_Complaint_DTL.FirstOrDefaultAsync(x => x.Cmst_Id == d.Cmst_Id && x.CON_Id == pkId);
                            if (result1 == null)
                            {
                                int id = await primarykeyvalue.primary_key("Consult_Complaint_DTL");
                                Consult_Complaint_DTL obj = new Consult_Complaint_DTL()
                                {
                                    CPT_Id = id,
                                    Cmst_Id = d.Cmst_Id,
                                    CON_Id = pkId,
                                    //Remarks = a.Remarks,
                                    created_by = 1,
                                    created_date = DateTime.Now,
                                    delete_flag = false,
                                };
                                var result_ = await db.Consult_Complaint_DTL.AddAsync(obj);
                                await db.SaveChangesAsync();
                            }
                            else
                                return null;
                        }

                        List<Symptoms> AlreadyExistsSymptoms = await symptomsRepository.GetExistsSymptoms(lead.Appt_Id);
                        foreach (var d in AlreadyExistsSymptoms)
                        {
                            var result1 = await db.Consult_Symptoms_DTL.FirstOrDefaultAsync(x => x.Smst_Id == d.Smst_Id && x.CON_Id == pkId);
                            if (result1 == null)
                            {
                                int id = await primarykeyvalue.primary_key("Consult_Symptoms_DTL");
                                Consult_Symptoms_DTL obj = new Consult_Symptoms_DTL()
                                {
                                    SYM_Id = id,
                                    Smst_Id = d.Smst_Id,
                                    CON_Id = pkId,
                                    //Remarks = a.Remarks,
                                    created_by = 1,
                                    created_date = DateTime.Now,
                                    delete_flag = false,
                                };
                                var result_ = await db.Consult_Symptoms_DTL.AddAsync(obj);
                                await db.SaveChangesAsync();
                            }
                            else
                                return null;
                        }

                        List<DiseasesDtl> AlreadyExistsDisease = await diseasesDtlRepository.GetExistsDiseases(lead.Appt_Id);
                        foreach (var d in AlreadyExistsDisease)
                        {
                            var result1 = await db.Consult_Diseases_DTL.FirstOrDefaultAsync(x => x.Id == d.Id && x.CON_Id == pkId);
                            if (result1 == null)
                            {
                                int id = await primarykeyvalue.primary_key("Consult_Diseases_DTL");
                                Consult_Diseases_DTL obj = new Consult_Diseases_DTL()
                                {
                                    Ddtl_Id = id,
                                    Id = d.Id,
                                    CON_Id = pkId,
                                    //Remarks = a.Remarks,
                                    created_by = 1,
                                    created_date = DateTime.Now,
                                    delete_flag = false,
                                };
                                var result_ = await db.Consult_Diseases_DTL.AddAsync(obj);
                                await db.SaveChangesAsync();
                            }
                            else
                                return null;
                        }

                        List<AllergySigns_DTL> AlreadyExistsAllergySigns = await allergySigns_DTLRepository.GetExistsAllergySigns(lead.Appt_Id);
                        foreach (var d in AlreadyExistsAllergySigns)
                        {
                            var result1 = await db.Consult_AllergySigns_DTL.FirstOrDefaultAsync(x => x.Al_Id == d.Al_Id && x.CON_Id == pkId);
                            if (result1 == null)
                            {
                                int id = await primarykeyvalue.primary_key("Consult_AllergySigns_DTL");
                                Consult_AllergySigns_DTL obj = new Consult_AllergySigns_DTL()
                                {
                                    Ddtl_Id = id,
                                    Al_Id = d.Al_Id,
                                    CON_Id = pkId,
                                    //Remarks = a.Remarks,
                                    created_by = 1,
                                    created_date = DateTime.Now,
                                    delete_flag = false,
                                };
                                var result_ = await db.Consult_AllergySigns_DTL.AddAsync(obj);
                                await db.SaveChangesAsync();
                            }
                            else
                                return null;
                        }

                        await InsertConsult_Parameters(lead);
                        //var para = await db.Parameters.FirstOrDefaultAsync(x => x.Appt_Id == lead.Appt_Id);
                        //int _pkid3 = await primarykeyvalue.primary_key("Consult_Parameters");
                        //Consult_Parameters obj1 = new Consult_Parameters();
                        //obj1.PA_Id = _pkid3;
                        //obj1.CON_Id = pkId;
                        //obj1.PA_Code = _pkid3 <= 09 ? "PA" + '0' + Convert.ToString(_pkid3) : "PA" + Convert.ToString(_pkid3);
                        //obj1.PA_Height = para.PA_Height;
                        //obj1.PA_Weight = para.PA_Weight;
                        //obj1.PA_TempInFahrenheit = para.PA_TempInFahrenheit;
                        //obj1.PA_TempInCelsius = para.PA_TempInCelsius;
                        //obj1.PA_BloodPressure = para.PA_BloodPressure;
                        //obj1.PA_Sugar = para.PA_Sugar;
                        //obj1.PA_ECG = para.PA_ECG;
                        //obj1.PA_OxygenSaturation = para.PA_OxygenSaturation;
                        //obj1.PA_PulseRate = para.PA_PulseRate;
                        //obj1.PA_RespiratoryRate = para.PA_RespiratoryRate;
                        //obj1.PA_Hemoglobin = para.PA_Hemoglobin;
                        //obj1.created_by = 1;
                        //obj1.created_date = DateTime.Now;
                        //obj1.delete_flag = false;
                        //obj1.status = 1;
                        //var res = await db.Consult_Parameters.AddAsync(obj1);

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
        public async Task<Consult_Parameters> InsertConsult_Parameters(ApproveAppointment lead)
        {
            try
            {
                var result = await db.Parameters.FirstOrDefaultAsync(x => x.Appt_Id == lead.Appt_Id);
                if (result != null)
                {
                    var consultn_Id = (from c in db.Consultation
                                       where c.CON_APPT_Id_FK == lead.Appt_Id
                                       select c.CON_Id).FirstOrDefault();
                    int id = await primarykeyvalue.primary_key("Consult_Parameters");
                    Consult_Parameters insert = new Consult_Parameters()
                    {
                        PA_Id = id,
                        CON_Id = consultn_Id,
                        PA_Code = id <= 09 ? "PA" + '0' + Convert.ToString(id) : "PA" + Convert.ToString(id),
                        PA_Height = result.PA_Height,
                        PA_Weight = result.PA_Weight,
                        PA_TempInFahrenheit = result.PA_TempInFahrenheit,
                        PA_TempInCelsius = result.PA_TempInCelsius,
                        PA_BloodPressure = result.PA_BloodPressure,
                        PA_Sugar = result.PA_Sugar,
                        PA_ECG = result.PA_ECG,
                        PA_OxygenSaturation = result.PA_OxygenSaturation,
                        PA_PulseRate = result.PA_PulseRate,
                        PA_RespiratoryRate = result.PA_RespiratoryRate,
                        PA_Hemoglobin = result.PA_Hemoglobin,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1,
                    };
                    var _new = await db.Consult_Parameters.AddAsync(insert);
                    await db.SaveChangesAsync();
                    return _new.Entity;

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
                    var datet = DateTime.Parse(lead.Select_day);
                    var datetim = datet.ToString("yyyy-MM-dd");
                    if (result != null)
                    {
                        result.Appt_Id = lead.Appt_Id;
                        result.Appt_PatientId_FK = lead.Appt_PatientId_FK;
                        result.CD_Id = lead.CD_Id;
                        result.Appt_DO_Id_FK = lead.Appt_DO_Id_FK;
                        result.Appt_DateTime = lead.Appt_DateTime;
                        result.Select_day = datetim;
                        result.Select_FrmTime = DateTime.ParseExact(lead.Select_FrmTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt");
                        result.Select_toTime = DateTime.ParseExact(lead.Select_toTime, "HH:mm", CultureInfo.CurrentCulture).ToString("hh:mm tt");
                        result.Appt_Is_active = lead.Appt_Is_active;
                        result.Appt_Type = lead.Appt_Type;
                        result.Assi_Id = lead.Assi_Id;
                        result.UnderBPMedication = lead.UnderBPMedication;
                        result.UnderSugarMedication = lead.UnderSugarMedication;
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
                                     Appt_P_DOB = b.PR_DOB,
                                     Appt_P_Gender = b.PR_Gender,
                                     Appt_P_BloodGroup = b.PR_BloodGroup,
                                     Appt_P_MotherTounge = b.PR_MotherTongue,
                                     Language = s.Language,
                                     PR_Photobyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                               System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                               System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                                     PatientLocation = m.district_name,
                                     PR_MobileNumber = b.PR_MobileNumber,
                                     complaintslist = (from g in db.Complaint
                                                       join h in db.ComplaintMst on g.Cmst_Id equals h.Cmst_Id
                                                       where g.Appt_Id == a.Appt_Id
                                                       select new GetAllComplaint()
                                                       {
                                                           Cmst_Id = g.Cmst_Id,
                                                           Cmst_Code = h.Cmst_Code,
                                                           Cmst_Name = h.Cmst_Name,
                                                       }).ToList(),
                                     symptomslist = (from i in db.Symptoms
                                                     join j in db.SymptomsMst on i.Smst_Id equals j.Smst_Id
                                                     where i.Appt_Id == a.Appt_Id
                                                     select new GetAllSymptoms()
                                                     {
                                                         Smst_Id = i.Smst_Id,
                                                         Smst_Code = j.Smst_Code,
                                                         Smst_Name = j.Smst_Name,
                                                     }).ToList(),
                                     diseaseslist = (from k in db.DiseasesDtl
                                                     join l in db.Diseases on k.Id equals l.Id
                                                     where k.Appt_Id == a.Appt_Id
                                                     select new GetAllDiseasesDtl()
                                                     {
                                                         Id = k.Id,
                                                         Diseases_Code = l.Diseases_Code,
                                                         Acronyms = l.Acronyms,
                                                         Diseases_Name = l.Diseases_Name,
                                                     }).ToList(),
                                     Allergylist = (from p in db.AllergySigns_DTL
                                                    join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                    where p.Appt_Id == a.Appt_Id
                                                    select new GetAllAllergySigns_DTL()
                                                    {
                                                        Al_Id = p.Al_Id,
                                                        Al_Code = q.Al_Code,
                                                        Acronyms = q.Acronyms,
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
                                     Select_date = (Convert.ToDateTime(a.Select_day)).ToString("yyyy-MM-dd"),
                                     Select_FrmTime = DateTime.ParseExact(a.Select_FrmTime, "hh:mm tt", CultureInfo.CurrentCulture).ToString("HH:mm"),
                                     Select_toTime = DateTime.ParseExact(a.Select_toTime, "hh:mm tt", CultureInfo.CurrentCulture).ToString("HH:mm"),
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
        public async Task<List<AppointmentModelById>> GetAppointmentById(int Appt_Id)
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
                                 Appt_P_DOB = b.PR_DOB,
                                 Appt_P_Gender = b.PR_Gender,
                                 Appt_P_BloodGroup = b.PR_BloodGroup,
                                 Appt_P_MotherTounge = b.PR_MotherTongue,
                                 Language = s.Language,
                                 PatientLocation = m.district_name,
                                 PR_Photobyte = File.Exists("wwwroot/Patient/" + b.PR_Photo) == true ?
                                               System.IO.File.ReadAllBytes("wwwroot/Patient/" + b.PR_Photo) :
                                               System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                                 PR_MobileNumber = b.PR_MobileNumber,
                                 complaintslist = (from g in db.Complaint
                                                   join h in db.ComplaintMst on g.Cmst_Id equals h.Cmst_Id
                                                   where g.Appt_Id == a.Appt_Id
                                                   select new GetAllComplaint()
                                                   {
                                                       Cmst_Id = g.Cmst_Id,
                                                       Cmst_Code = h.Cmst_Code,
                                                       Cmst_Name = h.Cmst_Name,
                                                   }).ToList(),
                                 symptomslist = (from i in db.Symptoms
                                                 join j in db.SymptomsMst on i.Smst_Id equals j.Smst_Id
                                                 where i.Appt_Id == a.Appt_Id
                                                 select new GetAllSymptoms()
                                                 {
                                                     Smst_Id = i.Smst_Id,
                                                     Smst_Code = j.Smst_Code,
                                                     Smst_Name = j.Smst_Name,
                                                 }).ToList(),
                                 diseaseslist = (from k in db.DiseasesDtl
                                                 join l in db.Diseases on k.Id equals l.Id
                                                 where k.Appt_Id == a.Appt_Id
                                                 select new GetAllDiseasesDtl()
                                                 {
                                                     Id = k.Id,
                                                     Diseases_Code = l.Diseases_Code,
                                                     Acronyms = l.Acronyms,
                                                     Diseases_Name = l.Diseases_Name,
                                                 }).ToList(),
                                 Allergylist = (from p in db.AllergySigns_DTL
                                                join q in db.AllergySigns on p.Al_Id equals q.Al_Id
                                                where p.Appt_Id == a.Appt_Id
                                                select new GetAllAllergySigns_DTL()
                                                {
                                                    Al_Id = p.Al_Id,
                                                    Al_Code = q.Al_Code,
                                                    Acronyms = q.Acronyms,
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
                                 Select_date = (Convert.ToDateTime(a.Select_day)).ToString("yyyy-MM-dd"),
                                 Select_FrmTime = DateTime.ParseExact(a.Select_FrmTime, "hh:mm tt", CultureInfo.CurrentCulture).ToString("HH:mm"),
                                 Select_toTime = DateTime.ParseExact(a.Select_toTime, "hh:mm tt", CultureInfo.CurrentCulture).ToString("HH:mm"),
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

    }
}
