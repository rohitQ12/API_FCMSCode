using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class PatientRxDetailsRepository : IPatientRxDetails
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private Patient_Prescription_DTLRepository patient_Prescription_DTLRepository;
        public readonly string _connectionString;
        public PatientRxDetailsRepository(IConfiguration configuration)
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            patient_Prescription_DTLRepository = new Patient_Prescription_DTLRepository();
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public async Task<PatientRxDetails> InsertPatientRxDetails(Prescription_Details lead)
        {
            try
            {
                var duplicate = await db.PatientRxDetails.FirstOrDefaultAsync(x => x.Prescription_date == lead.Prescription_date && x.Rx_CON_Id_FK == lead.Rx_CON_Id_FK);
                if (duplicate == null)
                {
                    int id = await primarykeyvalue.primary_key("PatientRxDetails");
                    PatientRxDetails obj = new PatientRxDetails()
                    {
                        Rx_Id = id,
                        Prescription_date = lead.Prescription_date,
                        Rx_CON_Id_FK = lead.Rx_CON_Id_FK,
                        //Delivery_status = lead.Delivery_status,
                        AcceptPrescription = 0,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.PatientRxDetails.AddAsync(obj);
                    await db.SaveChangesAsync();
                    var PPD = await patient_Prescription_DTLRepository.InsertPatient_Prescription_DTL(lead.Patient_Prescription_DTL, id);
                    return result.Entity;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AcceptPatientRxDetails(int Rx_Id, int Rx_CON_Id_FK, int AcceptPrescription)
        {
            try
            {
                var result = await db.PatientRxDetails.FirstOrDefaultAsync(x => x.Rx_Id == Rx_Id && x.Rx_CON_Id_FK == Rx_CON_Id_FK);
                if (result != null)
                {

                    result.Rx_Id = Rx_Id;
                    result.Rx_CON_Id_FK = Rx_CON_Id_FK;
                    result.AcceptPrescription = AcceptPrescription;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 1;
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<PatientRxDetails> UpdatePatientRxDetails(PatientRxDetails lead)
        {
            try
            {
                var result = await db.PatientRxDetails.FirstOrDefaultAsync(x => x.Rx_Id == lead.Rx_Id && x.Rx_CON_Id_FK == lead.Rx_CON_Id_FK);
                //&& x.AcceptPrescription == 1
                if (result != null)
                {
                    result.Rx_Id = lead.Rx_Id;
                    result.Prescription_date = lead.Prescription_date;
                    result.Rx_CON_Id_FK = lead.Rx_CON_Id_FK;
                    result.Delivery_status = lead.Delivery_status;
                    result.AcceptPrescription = lead.AcceptPrescription;
                    result.modified_by = 2;
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

        public async Task<PatientRxDetails> DeletePatientRxDetails(int Rx_Id)
        {
            try
            {
                var result = await db.PatientRxDetails.FirstOrDefaultAsync(x => x.Rx_Id == Rx_Id);
                if (result != null)
                {
                    result.Rx_Id = Rx_Id;
                    result.delete_flag = true;
                    result.status = 0;
                    result.deleted_by = 3;
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
        public async Task<List<GetAllPatientRxDetails>> GetAllPatientRxDetails()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.PatientRxDetails
                                 join b in db.Consultation on a.Rx_CON_Id_FK equals b.CON_Id
                                 join c in db.Patient on b.CON_PR_Id_FK equals c.PR_Id
                                 orderby a.Rx_Id descending
                                 select new GetAllPatientRxDetails
                                 {
                                     Rx_Id = a.Rx_Id,
                                     Prescription_date = a.Prescription_date,
                                     Rx_CON_Id_FK = a.Rx_CON_Id_FK,
                                     Rx_CON_PR_ID_FK = b.CON_PR_Id_FK,
                                     Rx_CON_PR_Name = string.Concat(c.PR_FirstName, c.PR_LastName),
                                     Rx_CON_Type = b.CON_Type,
                                     Delivery_status = a.Delivery_status,
                                     AcceptPrescription = a.AcceptPrescription,
                                     delete_flag = a.delete_flag,
                                     status = a.status,

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
        public async Task<PatientRxDetailsById> GetPatientRxDetailsById(int Rx_Id)
        {
            if (db != null)
            {
                var query = (from a in db.PatientRxDetails
                             join b in db.Consultation on a.Rx_CON_Id_FK equals b.CON_Id
                             join c in db.Patient on b.CON_PR_Id_FK equals c.PR_Id
                             where a.Rx_Id == Rx_Id
                             select new PatientRxDetailsById
                             {
                                 Rx_Id = a.Rx_Id,
                                 Prescription_date = a.Prescription_date,
                                 Rx_CON_Id_FK = a.Rx_CON_Id_FK,
                                 Rx_CON_PR_ID_FK = b.CON_PR_Id_FK,
                                 Rx_CON_PR_Name = string.Concat(c.PR_FirstName, c.PR_LastName),
                                 Rx_CON_Type = b.CON_Type,
                                 Delivery_status = a.Delivery_status,
                                 AcceptPrescription = a.AcceptPrescription,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

        public async Task<List<GetDrugForSpeedSearch>> GetDrugForSpeedSearch(string EnteredText)
        {
            using (Microsoft.Data.SqlClient.SqlConnection sql = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
            {
                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetDrugForSpeedSearch", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@EnteredText", EnteredText));
                    var response = new List<GetDrugForSpeedSearch>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToSearchValue(reader));
                        }
                    }
                    await sql.CloseAsync();
                    return response;
                }
            }            
        }
        public GetDrugForSpeedSearch MapToSearchValue(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            return new GetDrugForSpeedSearch()
            {
                Id = Convert.ToInt32(reader["Id"]),
                DrugName = Convert.ToString(reader["DrugName"]),
                DT_Id_FK = Convert.ToInt32(reader["DT_Id_FK"]),
                Strength = Convert.ToString(reader["Strength"]),
                UT_Id_FK = Convert.ToInt32(reader["UT_Id_FK"]),
                Description = Convert.ToString(reader["Description"]),
                Instruction = Convert.ToString(reader["Instruction"]),

            };
        }
    
    }
    
}
