using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class PatientRepository : IPatient
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private PatientDocumentRepository patientDocumentRepository;
        private readonly IConfiguration connectionstrings;

        public PatientRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            patientDocumentRepository = new PatientDocumentRepository();
        }
        public PatientRepository(IConfiguration configuration)
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            patientDocumentRepository = new PatientDocumentRepository();
            this.connectionstrings = configuration.GetSection("ConnectionString");


        }
        public async Task<Patient> InsertPatient(Patient_Images lead,string UserId)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("Patient");
                string uniqueFilename = lead.PR_Photo!=null?ProcessUploadedFile(lead): "user-1633249__340 (1).png";
                Patient obj = new Patient()
                {
                    PR_Id = id,
                    PR_RemoteHospitalName_Id_FK = lead.PR_RemoteHospitalName_Id_FK,
                    UserId= UserId,
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
                    PR_Religion = lead.PR_Religion,
                    PR_Nationality = lead.PR_Nationality,
                    PR_Caste = lead.PR_Caste,
                    PR_BloodGroup = lead.PR_BloodGroup,
                    PR_MotherTongue = lead.PR_MotherTongue,
                    PR_Occupation = lead.PR_Occupation,
                    PR_Income = lead.PR_Income,
                    PR_Insurance = lead.PR_Insurance,
                    PR_Address = lead.PR_Address,
                    PR_Country_Id_FK = lead.PR_Country_Id_FK != null ? lead.PR_Country_Id_FK : 0,
                    PR_S_Id_FK = lead.PR_S_Id_FK!=null ? lead.PR_S_Id_FK: 0 ,
                    PR_D_Id_FK = lead.PR_D_Id_FK != null ? lead.PR_D_Id_FK : 0,
                    PR_Taluk_Id = lead.PR_Taluk_Id,
                    PR_Gram_Id = lead.PR_Gram_Id,
                    PR_Village = lead.PR_Village,
                    PR_Postalcode = lead.PR_Postalcode,
                    PR_MobileNumber = lead.PR_MobileNumber!=null? lead.PR_MobileNumber :"0",
                    PR_Email = lead.PR_Email != null ? lead.PR_Email : "",
                    PR_PassportNo = lead.PR_PassportNo,
                    PR_RegistrationDateTime = DateTime.Now,
                    PR_Photo = uniqueFilename,
                    PR_UserId_FK = lead.PR_UserId_FK,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Patient.AddAsync(obj);
                await db.SaveChangesAsync();
                //await InsertUsers(obj);
                //var PDOC = lead.Patient_Documents!=null? await patientDocumentRepository.InsertPatientDocument(lead.Patient_Documents, id): null;
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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

        public async Task<Patient> UpdatePatient(Patient_Images lead)
        {
            try
            {
                var result = await db.Patient.FirstOrDefaultAsync(x => x.PR_Id == lead.PR_Id);
                if (lead.PR_Photo != null)
                {
                    if (result != null)
                    {
                        string filepath = Path.Combine("wwwroot/Patient", result.PR_Photo);
                        System.IO.File.Delete(filepath);
                    }
                }
                //Update PatientRegistration logo
                string uniqueFilename = ProcessUploadedFile(lead);

                if (result != null)
                {
                    //result.PR_Id = lead.PR_Id;
                    result.PR_RemoteHospitalName_Id_FK = lead.PR_RemoteHospitalName_Id_FK;
                    result.PR_PatientCode = lead.PR_PatientCode;
                    result.PR_FirstName = lead.PR_FirstName;
                    result.PR_LastName = lead.PR_LastName;
                    result.PR_Gender = lead.PR_Gender;
                    result.PR_DOB = lead.PR_DOB;
                    result.PR_Age = lead.PR_Age;
                    result.PR_LandlineNo = lead.PR_LandlineNo;
                    result.PR_Alternative_No = lead.PR_Alternative_No;
                    result.PR_MaritalStatus = lead.PR_MaritalStatus;
                    result.PR_FatherName = lead.PR_FatherName;
                    result.PR_Religion = lead.PR_Religion;
                    result.PR_Nationality = lead.PR_Nationality;
                    result.PR_Caste = lead.PR_Caste;
                    result.PR_BloodGroup = lead.PR_BloodGroup;
                    result.PR_MotherTongue = lead.PR_MotherTongue;
                    result.PR_Occupation = lead.PR_Occupation;
                    result.PR_Income = lead.PR_Income;
                    result.PR_Insurance = lead.PR_Insurance;
                    result.PR_Address = lead.PR_Address;
                    result.PR_Country_Id_FK = lead.PR_Country_Id_FK;
                    result.PR_S_Id_FK = lead.PR_S_Id_FK;
                    result.PR_D_Id_FK = lead.PR_D_Id_FK;
                    result.PR_Taluk_Id = lead.PR_Taluk_Id;
                    result.PR_Gram_Id = lead.PR_Gram_Id;
                    result.PR_Village = lead.PR_Village;
                    result.PR_Postalcode = lead.PR_Postalcode;
                    result.PR_MobileNumber = lead.PR_MobileNumber;
                    result.PR_Email = lead.PR_Email;
                    result.PR_PassportNo = lead.PR_PassportNo;
                    result.PR_RegistrationDateTime = lead.PR_RegistrationDateTime;
                    result.PR_Photo = uniqueFilename;
                    result.PR_UserId_FK = lead.PR_UserId_FK;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    //var PDOC = await PatientDocumentRepository.UpdatePatientDocument(lead.PatientDocument, lead.PR_Id);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllPatient>> GetAllPatient()
        {
            using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
            {
                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetAllPatient", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                PR_Religion = Convert.ToString(reader["PR_Religion"]),
                PR_Nationality = Convert.ToString(reader["PR_Nationality"]),
                PR_Caste = Convert.ToString(reader["PR_Caste"]),
                PR_BloodGroup = Convert.ToString(reader["PR_BloodGroup"]),
                PR_MotherTongue = Convert.ToString(reader["PR_MotherTongue"]),
                PR_Occupation = Convert.ToString(reader["PR_Occupation"]),
                PR_Income = Convert.ToString(reader["PR_Income"]),
                PR_Insurance = Convert.ToString(reader["PR_Insurance"]),
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
                PR_UserId_FK = Convert.ToInt32(reader["PR_UserId_FK"]),
                delete_flag = Convert.ToBoolean(reader["delete_flag"]),
                status = Convert.ToInt32(reader["status"]),

            };
        }
        public async Task<Patient> DeletePatient(int PR_Id)
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
                    return result;
                }
                return null;
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
                PR_Religion = Convert.ToString(reader["PR_Religion"]),
                PR_Nationality = Convert.ToString(reader["PR_Nationality"]),
                PR_Caste = Convert.ToString(reader["PR_Caste"]),
                PR_BloodGroup = Convert.ToString(reader["PR_BloodGroup"]),
                PR_MotherTongue = Convert.ToString(reader["PR_MotherTongue"]),
                PR_Occupation = Convert.ToString(reader["PR_Occupation"]),
                PR_Income = Convert.ToString(reader["PR_Income"]),
                PR_Insurance = Convert.ToString(reader["PR_Insurance"]),
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
                PR_UserId_FK = Convert.ToInt32(reader["PR_UserId_FK"]),
                delete_flag = Convert.ToBoolean(reader["delete_flag"]),
                status = Convert.ToInt32(reader["status"]),
                UserId =Convert.ToString(reader["UserId"])

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
