using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GlobalApi.Repository.MasterRepository
{
    public class PatientRepository : IPatient
    {
        //public readonly string _connectionString;
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        //private PatientDocumentRepository patientDocumentRepository;

        public PatientRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            //patientDocumentRepository = new PatientDocumentRepository();
        }

        public async Task<string> InsertPatient(Patient_Images lead, string UserId, string Create_by)
        {

            try
            {
                var MobileNumber = lead.PR_MobileNumber == null ? "" : lead.PR_MobileNumber;
                var Email = lead.PR_Email == null ? "" : lead.PR_Email;
                var Patient = await db.Patient.FirstOrDefaultAsync(x => x.PR_MobileNumber == lead.PR_MobileNumber || x.PR_Email == lead.PR_Email);
                var PR_MobileNumber = await db.Patient.FirstOrDefaultAsync(x => x.PR_MobileNumber == MobileNumber);
                var PR_Email = await db.Patient.FirstOrDefaultAsync(x => x.PR_Email == Email);

                if (PR_MobileNumber == null || PR_MobileNumber.PR_MobileNumber == "")
                {
                    if (PR_Email == null || PR_Email.PR_Email == "")
                    {
                        var getdocpkId = (from a in db.DocPkValue where a.PkName == "Patient" select a.PkId).FirstOrDefault();
                        var getpresentval = (from a in db.DocPkValue where a.PkName == "Patient" select a.PkPresentValue).FirstOrDefault();
                        //var strvoucherno = await PkIdAutomaicGeneration_test(1,"Branch",1);
                        var strvoucherno = await PkIdAutomaicGeneration_test(getdocpkId, "Patient", getpresentval);
                        var deptno = strvoucherno.automaticgen_patid;
                        //invoiceno with suffix and prefix//
                        var strinvoiceno = await GetSuffixPrefixDetails(getdocpkId);
                        var strprefix = strinvoiceno.Prefix;
                        var year = Convert.ToString(DateTime.Now.Year);

                        int id = await primarykeyvalue.primary_key("Patient");
                        string uniqueFilename = lead.PR_Photo != null ? ProcessUploadedFile(lead) : "user-1633249__340 (1).png";
                        Patient obj = new Patient()
                        {
                            PR_Id = id,
                            PR_RemoteHospitalName_Id_FK = lead.PR_RemoteHospitalName_Id_FK,
                            PR_UserId = UserId,
                            PR_RegNo = year + strprefix + deptno,
                            PR_PatientCode = "P-" + Convert.ToString(id),
                            //PR_PatientCode = lead.PR_PatientCode,
                            PR_FirstName = lead.PR_FirstName,
                            PR_LastName = lead.PR_LastName,
                            PR_Gender = lead.PR_Gender,
                            PR_DOB = lead.PR_DOB,
                            PR_Age = lead.PR_Age,
                            PR_LandlineNo = lead.PR_LandlineNo,
                            PR_Alternative_No = lead.PR_Alternative_No,
                            PR_MaritalStatus = lead.PR_MaritalStatus,
                            PR_FatherName = lead.PR_FatherName,
                            PR_BloodGroup = lead.PR_BloodGroup,
                            PR_MotherTongue = lead.PR_MotherTongue,
                            PR_REG_Id_FK = lead.PR_REG_Id_FK,
                            PR_NAL_Id_FK = lead.PR_NAL_Id_FK,
                            PR_CAT_Id_FK = lead.PR_CAT_Id_FK,
                            PR_IDN_Id_FK = lead.PR_IDN_Id_FK,
                            PR_Identity_No = lead.PR_Identity_No,
                            National_Health_Id = lead.National_Health_Id,
                            PR_OCU_Id_FK = lead.PR_OCU_Id_FK,
                            PR_Income = lead.PR_Income,
                            PR_Insurance = lead.PR_Insurance,
                            PR_INU_Id_FK = lead.PR_INU_Id_FK,
                            PR_Insured_Sum = lead.PR_Insured_Sum,
                            PR_Address = lead.PR_Address,
                            PR_Country_Id_FK = lead.PR_Country_Id_FK != null ? lead.PR_Country_Id_FK : 0,
                            PR_S_Id_FK = lead.PR_S_Id_FK != null ? lead.PR_S_Id_FK : 0,
                            PR_D_Id_FK = lead.PR_D_Id_FK != null ? lead.PR_D_Id_FK : 0,
                            PR_Taluk_Id = lead.PR_Taluk_Id,
                            PR_Gram_Id = lead.PR_Gram_Id,
                            PR_Village = lead.PR_Village,
                            PR_Postalcode = lead.PR_Postalcode,
                            PR_MobileNumber = lead.PR_MobileNumber != null ? lead.PR_MobileNumber : "0",
                            PR_Email = lead.PR_Email != null ? lead.PR_Email : "",
                            PR_PassportNo = lead.PR_PassportNo,
                            PR_RegistrationDateTime = DateTime.Now,
                            PR_Photo = uniqueFilename,
                            PR_UserId_FK = lead.PR_UserId_FK,
                            created_by = Create_by,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.Patient.AddAsync(obj);
                        await db.SaveChangesAsync();
                        //await InsertUsers(obj);
                        //var PDOC = lead.Patient_Documents!=null? await patientDocumentRepository.InsertPatientDocument(lead.Patient_Documents, id): null;
                        return "Patient Added Successfully";
                    }
                    return "Patient Email Already Exists";
                }
                return "Patient MobileNumber Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<get_Patidautomatic> PkIdAutomaicGeneration_test(int PkId, string tab_name, decimal txtBox)
        {
            try
            {
                Microsoft.Data.SqlClient.SqlConnection sql;
                Microsoft.Data.SqlClient.SqlCommand cmd;
                using (sql = ado_Configurations.connection())
                {
                    cmd = new Microsoft.Data.SqlClient.SqlCommand("PkIdAutomaicGeneration", sql);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@PkId", PkId));
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@tab_name", tab_name));
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@txtBox", txtBox));
                    await sql.OpenAsync();
                    var rdr = await cmd.ExecuteScalarAsync();
                    get_Patidautomatic automicpatid = new get_Patidautomatic();
                    automicpatid.automaticgen_patid = Convert.ToString(rdr);
                    return automicpatid;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<viewdetail_suffixprefix> GetSuffixPrefixDetails(int DocPkTblId)
        {
            DataSet ds = new DataSet();
            //SqlConnection sql = new SqlConnection(ado_Configurations.connection());
            var sql = ado_Configurations.connection();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetSuffixPrefixDetails", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@DocPkTblId", DocPkTblId));
            await sql.OpenAsync();
            da.SelectCommand = cmd;
            await cmd.ExecuteNonQueryAsync();
            da.Fill(ds);

            viewdetail_suffixprefix viewdetailsufpref = new viewdetail_suffixprefix();
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    viewdetailsufpref.SuffixprefixId = Convert.ToInt32(dr["SuffixprefixId"]);
                    viewdetailsufpref.DocPkTblId = Convert.ToInt32(dr["DocPkTblId"]);
                    viewdetailsufpref.StartIndex = Convert.ToDecimal(dr["StartIndex"]);
                    viewdetailsufpref.Prefix = Convert.ToString(dr["Prefix"]);
                    viewdetailsufpref.Suffix = Convert.ToString(dr["Suffix"]);
                    viewdetailsufpref.WidthOfNumericalPart = Convert.ToInt32(dr["WidthOfNumericalPart"]);
                    viewdetailsufpref.PrefillWithZero = Convert.ToBoolean(dr["PrefillWithZero"]);
                }
            }
            return viewdetailsufpref;
        }

        public async Task<UsersLists> InsertUsers(Patient lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Patient",
                User_ref_id = lead.PR_Id,
            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }

        //Inserting PatientRegistration Logo
        private string ProcessUploadedFile(Patient_Images model)
        {
            string uniqueFileName = null;


            if (model.PR_Photo != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Patient");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PR_Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PR_Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public async Task<string> UpdatePatient(Patient_Images lead)
        {
            try
            {
                var Patient = await db.Patient.FirstOrDefaultAsync(x => x.PR_Id == lead.PR_Id);
                var PR_MobileNumber = await db.Patient.FirstOrDefaultAsync(x => x.PR_MobileNumber == lead.PR_MobileNumber);
                var PR_Email = await db.Patient.FirstOrDefaultAsync(x => x.PR_Email == lead.PR_Email);
                if (PR_MobileNumber == null || Patient.PR_MobileNumber == lead.PR_MobileNumber)
                {
                    if (PR_Email == null || Patient.PR_Email == lead.PR_Email)
                    {
                        if (lead.PR_Photo != null)
                        {
                            if (Patient.PR_Photo != null && Patient.PR_Photo != "user-1633249__340 (1).png")
                            {
                                string filepath = Path.Combine("wwwroot/Patient", Patient.PR_Photo);
                                System.IO.File.Delete(filepath);
                            }

                        }
                        //Update PatientRegistration logo
                        string uniqueFilename = lead.PR_Photo != null ? ProcessUploadedFile(lead) : Patient.PR_Photo;

                        if (Patient != null)
                        {
                            //result.PR_Id = lead.PR_Id;
                            Patient.PR_RemoteHospitalName_Id_FK = lead.PR_RemoteHospitalName_Id_FK;
                            Patient.PR_PatientCode = lead.PR_PatientCode;
                            Patient.PR_FirstName = lead.PR_FirstName;
                            Patient.PR_LastName = lead.PR_LastName;
                            Patient.PR_Gender = lead.PR_Gender;
                            Patient.PR_DOB = lead.PR_DOB;
                            Patient.PR_Age = lead.PR_Age;
                            Patient.PR_LandlineNo = lead.PR_LandlineNo;
                            Patient.PR_Alternative_No = lead.PR_Alternative_No;
                            Patient.PR_MaritalStatus = lead.PR_MaritalStatus;
                            Patient.PR_FatherName = lead.PR_FatherName;
                            Patient.PR_REG_Id_FK = lead.PR_REG_Id_FK;
                            Patient.PR_NAL_Id_FK = lead.PR_NAL_Id_FK;
                            Patient.PR_CAT_Id_FK = lead.PR_CAT_Id_FK;
                            Patient.PR_BloodGroup = lead.PR_BloodGroup;
                            Patient.PR_MotherTongue = lead.PR_MotherTongue;
                            Patient.National_Health_Id = lead.National_Health_Id;
                            Patient.PR_OCU_Id_FK = lead.PR_OCU_Id_FK;
                            Patient.PR_Income = lead.PR_Income;
                            Patient.PR_Insurance = lead.PR_Insurance;
                            Patient.PR_Address = lead.PR_Address;
                            Patient.PR_Country_Id_FK = lead.PR_Country_Id_FK;
                            Patient.PR_S_Id_FK = lead.PR_S_Id_FK;
                            Patient.PR_D_Id_FK = lead.PR_D_Id_FK;
                            Patient.PR_Taluk_Id = lead.PR_Taluk_Id;
                            Patient.PR_Gram_Id = lead.PR_Gram_Id;
                            Patient.PR_Village = lead.PR_Village;
                            Patient.PR_Postalcode = lead.PR_Postalcode;
                            Patient.PR_MobileNumber = lead.PR_MobileNumber;
                            Patient.PR_Email = lead.PR_Email;
                            Patient.PR_PassportNo = lead.PR_PassportNo;
                            Patient.PR_RegistrationDateTime = lead.PR_RegistrationDateTime;
                            Patient.PR_Photo = uniqueFilename;
                            Patient.PR_UserId_FK = lead.PR_UserId_FK;
                            Patient.modified_by = 2;
                            Patient.modified_date = DateTime.Now;
                            Patient.delete_flag = false;
                            Patient.status = 2;
                            await db.SaveChangesAsync();
                            //var PDOC = await PatientDocumentRepository.UpdatePatientDocument(lead.PatientDocument, lead.PR_Id);
                            return "Patient Updated Successfully";
                        }
                    }
                    return "Patient Email Already Exists";
                }
                return "Patient MobileNumber Already Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllPatient>> GetAllPatient(int OfficeRoleId, string Roleaction)
        {
            using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
            {
                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetAllPatient", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@OfficeRoleId", OfficeRoleId)); 
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Roleaction", Roleaction));
                    var response = new List<GetAllPatient>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }
                    return response;
                }
            }
        }
        public GetAllPatient MapToValue(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            return new GetAllPatient()
            {
                PR_Id = Convert.ToInt32(reader["PR_Id"]),
                UserId = Convert.ToString(reader["UserId"]),
                PR_RegNo = Convert.ToString(reader["PR_RegNo"]),
                PR_RemoteHospitalName_Id_FK = Convert.ToInt32(reader["PR_RemoteHospitalName_Id_FK"]),
                PR_RemoteHospitalName = Convert.ToString(reader["Hos_HospitalName"]),
                PR_PatientCode = Convert.ToString(reader["PR_PatientCode"]),
                PR_FirstName = Convert.ToString(reader["PR_FirstName"]),
                PR_LastName = Convert.ToString(reader["PR_LastName"]),
                PR_Gender = Convert.ToString(reader["PR_Gender"]),
                PR_DOB = Convert.ToDateTime(reader["PR_DOB"]),
                PR_Age = Convert.ToString(reader["PR_Age"]),
                PR_LandlineNo = Convert.ToString(reader["PR_LandlineNo"]),
                PR_Alternative_No = Convert.ToString(reader["PR_Alternative_No"]),
                PR_MaritalStatus = Convert.ToString(reader["PR_MaritalStatus"]),
                PR_FatherName = Convert.ToString(reader["PR_FatherName"]),
                PR_BloodGroup = Convert.ToString(reader["PR_BloodGroup"]),
                PR_MotherTongue = Convert.ToInt32(reader["PR_MotherTongue"]),
                Language = Convert.ToString(reader["Language"]),
                PR_REG_Id_FK = Convert.ToInt32(reader["PR_REG_Id_FK"]),
                Religion = Convert.ToString(reader["Religion"]),
                PR_NAL_Id_FK = Convert.ToInt32(reader["PR_NAL_Id_FK"]),
                Nationality = Convert.ToString(reader["Nationality"]),
                PR_CAT_Id_FK = Convert.ToInt32(reader["PR_CAT_Id_FK"]),
                Caste = Convert.ToString(reader["Caste"]),
                PR_IDN_Id_FK = Convert.ToInt32(reader["PR_IDN_Id_FK"]),
                DOC_Name = Convert.ToString(reader["DOC_Name"]),
                PR_Identity_No = Convert.ToString(reader["PR_Identity_No"]),
                National_Health_Id = Convert.ToInt32(reader["National_Health_Id"]),
                PR_OCU_Id_FK = Convert.ToInt32(reader["PR_OCU_Id_FK"]),
                Occupation = Convert.ToString(reader["Occupation"]),
                PR_Income = Convert.ToString(reader["PR_Income"]),
                PR_Insurance = Convert.ToString(reader["PR_Insurance"]),
                PR_INU_Id_FK = Convert.ToInt32(reader["PR_INU_Id_FK"]),
                Insurer = Convert.ToString(reader["Insurer"]),
                Insurer_Category= Convert.ToString(reader["Insurer_Category"]),
                PR_Insured_Sum = Convert.ToInt32(reader["PR_Insured_Sum"]),
                PR_Address = Convert.ToString(reader["PR_Address"]),
                PR_Country_Id_FK = Convert.ToInt32(reader["PR_Country_Id_FK"]),
                PR_Country_Name = Convert.ToString(reader["Country_Name"]),
                PR_S_Id_FK = Convert.ToInt32(reader["PR_S_Id_FK"]),
                PR_StateName = Convert.ToString(reader["state_name"]),
                PR_D_Id_FK = Convert.ToInt32(reader["PR_D_Id_FK"]),
                PR_District = Convert.ToString(reader["district_name"]),
                PR_Taluk_Id = Convert.ToInt32(reader["PR_Taluk_Id"]),
                Taluk_name = Convert.ToString(reader["Taluk_name"]),
                PR_Gram_Id = Convert.ToInt32(reader["PR_Gram_Id"]),
                Gram_name = Convert.ToString(reader["Gram_name"]),
                PR_Village = Convert.ToString(reader["PR_Village"]),
                PR_Postalcode = Convert.ToInt32(reader["PR_Postalcode"]),
                PR_MobileNumber = Convert.ToString(reader["PR_MobileNumber"]),
                PR_Email = Convert.ToString(reader["PR_Email"]),
                PR_PassportNo = Convert.ToString(reader["PR_PassportNo"]),
                PR_RegistrationDateTime = Convert.ToDateTime(reader["PR_RegistrationDateTime"]),
                PR_Photo = Convert.ToString(reader["PR_Photo"]),
                PR_Photobyte = File.Exists("wwwroot/Patient/" + Convert.ToString(reader["PR_Photo"])) == true ? 
                System.IO.File.ReadAllBytes("wwwroot/Patient/" + Convert.ToString(reader["PR_Photo"])) :
                System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                PR_UserId_FK = Convert.ToInt32(reader["PR_UserId_FK"]),
                delete_flag = Convert.ToBoolean(reader["delete_flag"]),
                status = Convert.ToInt32(reader["status"]),
                sts_name = Convert.ToString(reader["sts_name"]),
            };
        }
        public async Task<string> DeletePatient(int PR_Id)
        {
            try
            {
                var result = await db.Patient.FirstOrDefaultAsync(x => x.PR_Id == PR_Id);
                if (result != null)
                {
                    result.PR_Id = PR_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Patient Deleted Successfully";
                }
                return "Patient Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<PatientById>> GetPatientById(int PR_Id)
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
                {
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetPatientById", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@patient_id", PR_Id));
                        var response = new List<PatientById>();
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(MapToPatientById(reader));
                            }
                        }
                        return response;
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PatientById MapToPatientById(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            return new PatientById()
            {
                PR_Id = Convert.ToInt32(reader["PR_Id"]),
                UserId = Convert.ToString(reader["UserId"]),
                PR_RegNo = Convert.ToString(reader["PR_RegNo"]),
                PR_RemoteHospitalName_Id_FK = Convert.ToInt32(reader["PR_RemoteHospitalName_Id_FK"]),
                PR_RemoteHospitalName = Convert.ToString(reader["Hos_HospitalName"]),
                PR_PatientCode = Convert.ToString(reader["PR_PatientCode"]),
                PR_FirstName = Convert.ToString(reader["PR_FirstName"]),
                PR_LastName = Convert.ToString(reader["PR_LastName"]),
                PR_Gender = Convert.ToString(reader["PR_Gender"]),
                PR_DOB = Convert.ToDateTime(reader["PR_DOB"]),
                PR_Age = Convert.ToString(reader["PR_Age"]),
                PR_LandlineNo = Convert.ToString(reader["PR_LandlineNo"]),
                PR_Alternative_No = Convert.ToString(reader["PR_Alternative_No"]),
                PR_MaritalStatus = Convert.ToString(reader["PR_MaritalStatus"]),
                PR_FatherName = Convert.ToString(reader["PR_FatherName"]),
                PR_BloodGroup = Convert.ToString(reader["PR_BloodGroup"]),
                PR_MotherTongue = Convert.ToInt32(reader["PR_MotherTongue"]),
                Language = Convert.ToString(reader["Language"]),
                PR_REG_Id_FK = Convert.ToInt32(reader["PR_REG_Id_FK"]),
                Religion = Convert.ToString(reader["Religion"]),
                PR_NAL_Id_FK = Convert.ToInt32(reader["PR_NAL_Id_FK"]),
                Nationality = Convert.ToString(reader["Nationality"]),
                PR_CAT_Id_FK = Convert.ToInt32(reader["PR_CAT_Id_FK"]),
                Caste = Convert.ToString(reader["Caste"]),
                PR_IDN_Id_FK = Convert.ToInt32(reader["PR_IDN_Id_FK"]),
                DOC_Name = Convert.ToString(reader["DOC_Name"]),
                PR_Identity_No = Convert.ToString(reader["PR_Identity_No"]),
                National_Health_Id = Convert.ToInt32(reader["National_Health_Id"]),
                PR_OCU_Id_FK = Convert.ToInt32(reader["PR_OCU_Id_FK"]),
                Occupation = Convert.ToString(reader["Occupation"]),
                PR_Income = Convert.ToString(reader["PR_Income"]),
                PR_Insurance = Convert.ToString(reader["PR_Insurance"]),
                PR_INU_Id_FK = Convert.ToInt32(reader["PR_INU_Id_FK"]),
                Insurer = Convert.ToString(reader["Insurer"]),
                PR_Insured_Sum = Convert.ToInt32(reader["PR_Insured_Sum"]),
                PR_Address = Convert.ToString(reader["PR_Address"]),
                PR_Country_Id_FK = Convert.ToInt32(reader["PR_Country_Id_FK"]),
                PR_Country_Name = Convert.ToString(reader["Country_Name"]),
                PR_S_Id_FK = Convert.ToInt32(reader["PR_S_Id_FK"]),
                PR_StateName = Convert.ToString(reader["state_name"]),
                PR_D_Id_FK = Convert.ToInt32(reader["PR_D_Id_FK"]),
                PR_District = Convert.ToString(reader["district_name"]),
                PR_Taluk_Id = Convert.ToInt32(reader["PR_Taluk_Id"]),
                Taluk_name = Convert.ToString(reader["Taluk_name"]),
                PR_Gram_Id = Convert.ToInt32(reader["PR_Gram_Id"]),
                Gram_name = Convert.ToString(reader["Gram_name"]),
                PR_Village = Convert.ToString(reader["PR_Village"]),
                PR_Postalcode = Convert.ToInt32(reader["PR_Postalcode"]),
                PR_MobileNumber = Convert.ToString(reader["PR_MobileNumber"]),
                PR_Email = Convert.ToString(reader["PR_Email"]),
                PR_PassportNo = Convert.ToString(reader["PR_PassportNo"]),
                PR_RegistrationDateTime = Convert.ToDateTime(reader["PR_RegistrationDateTime"]),
                PR_Photo = Convert.ToString(reader["PR_Photo"]),
                PR_Photobyte = File.Exists("wwwroot/Patient/" + Convert.ToString(reader["PR_Photo"])) == true ?
                System.IO.File.ReadAllBytes("wwwroot/Patient/" + Convert.ToString(reader["PR_Photo"])) :
                System.IO.File.ReadAllBytes(("wwwroot/Patient/" + "user-1633249__340 (1).png")),
                PR_UserId_FK = Convert.ToInt32(reader["PR_UserId_FK"]),
                delete_flag = Convert.ToBoolean(reader["delete_flag"]),
                status = Convert.ToInt32(reader["status"]),
                sts_name = Convert.ToString(reader["sts_name"]),

            };
        }
        public async Task<List<Patient_DD>> GetPatient_DD()
        {
            if (db != null)
            {
                var query = (from a in db.Patient
                             where a.delete_flag == false && a.status == 1
                             select new Patient_DD
                             {
                                 PR_Id = a.PR_Id,
                                 PR_PatientCode = a.PR_PatientCode,
                                 PR_Name = string.Concat(a.PR_FirstName,a.PR_LastName)
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<PatientById>> GetPatientByCode(string PR_PatientCode)
        {
            using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
            {
                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetPatientByCode", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@patient_code", PR_PatientCode));
                    var response = new List<PatientById>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToPatientById(reader));
                        }
                    }
                    return response;
                }
            }
        }

    }
}
