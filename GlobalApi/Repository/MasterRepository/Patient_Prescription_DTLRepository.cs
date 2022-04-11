using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class Patient_Prescription_DTLRepository : IPatient_Prescription_DTL
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public Patient_Prescription_DTLRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }

        public async Task<string> InsertPatient_Prescription_DTL(List<Patient_Prescription_DTL> lead , int Rx_Id_FK)
        {
            try
            {
                foreach (Patient_Prescription_DTL PPDTL in lead)
                {
                    //var duplicate = await db.Patient_Prescription_DTL.FirstOrDefaultAsync(x => x.Rx_Id_FK == PPDTL.Rx_Id_FK
                    //                && x.Rx_Desc == PPDTL.Rx_Desc);
                    var duplicate = await db.Patient_Prescription_DTL.FirstOrDefaultAsync(x => x.Rx_Id_FK == PPDTL.Rx_Id_FK
                       && x.DrugMst_Id_FK == PPDTL.DrugMst_Id_FK);

                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("Patient_Prescription_DTL");
                        Patient_Prescription_DTL obj = new Patient_Prescription_DTL()
                        {
                            Dtl_Id = id,
                            Rx_Id_FK = Rx_Id_FK,
                            DrugMst_Id_FK = PPDTL.DrugMst_Id_FK,
                            //Rx_Desc = PPDTL.Rx_Desc,
                            Rx_Dosage = PPDTL.Rx_Dosage,
                            Rx_Course = PPDTL.Rx_Course,
                            Remarks = PPDTL.Remarks,
                            delete_flag = false,
                        };
                        var result = await db.Patient_Prescription_DTL.AddAsync(obj);
                        await db.SaveChangesAsync();

                    }
                    else
                        return "Data already inserted";
                }
                return "Record insert successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Patient_Prescription_DTL> UpdatePatient_Prescription_DTL(Patient_Prescription_DTL lead)
        {
            try
            {
                var result = await db.Patient_Prescription_DTL.FirstOrDefaultAsync(x => x.Dtl_Id == lead.Dtl_Id);
                if (result != null)
                {
                    result.Dtl_Id = lead.Dtl_Id;
                    result.Rx_Id_FK = lead.Rx_Id_FK;
                    result.DrugMst_Id_FK = lead.DrugMst_Id_FK;
                    //result.Rx_Desc = lead.Rx_Desc;
                    result.Rx_Dosage = lead.Rx_Dosage;
                    result.Rx_Course = lead.Rx_Course;
                    result.Remarks = lead.Remarks;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
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
        public async Task<List<GetAllPPD>> GetAllPatient_Prescription_DTL()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Patient_Prescription_DTL
                                 join b in db.PatientRxDetails on a.Rx_Id_FK equals b.Rx_Id
                                 join c in db.DrugMaster on a.DrugMst_Id_FK equals c.Id
                                 join d in db.DrugType on c.DT_Id_FK equals d.Id
                                 join e in db.Unit on c.UT_Id_FK equals e.Id
                                 orderby a.Dtl_Id descending
                                 select new GetAllPPD
                                 {
                                     Dtl_Id = a.Dtl_Id,
                                     Rx_Id_FK = a.Rx_Id_FK,
                                     PrescriptionDate = b.Prescription_date,
                                     DrugMst_Id_FK = a.DrugMst_Id_FK,
                                     DrugMst_Name = c.DrugName,
                                     //DrugMst_DT_Id_FK = c.DT_Id_FK,
                                     DM_DT_Type = d.Type,
                                     DrugMst_Strength = c.Strength,
                                     //DrugMst_UT_Id_FK = c.UT_Id_FK,
                                     DM_UT_Unit = e.DrugUnit,
                                     Desc = c.Description,
                                     //Rx_Desc = a.Rx_Desc,
                                     Rx_Dosage = a.Rx_Dosage,
                                     Rx_Course = a.Rx_Course,
                                     Remarks = a.Remarks,
                                     delete_flag = a.delete_flag,
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
        public async Task<Patient_Prescription_DTL> DeletePatient_Prescription_DTL(int Dtl_Id)
        {
            try
            {
                var result = await db.Patient_Prescription_DTL.FirstOrDefaultAsync(x => x.Dtl_Id == Dtl_Id);
                if (result != null)
                {
                    result.Dtl_Id = Dtl_Id;
                    result.delete_flag = true;
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
        public async Task<PPD_By_Id> GetPatient_Prescription_DTLById(int Dtl_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Patient_Prescription_DTL
                             join b in db.PatientRxDetails on a.Rx_Id_FK equals b.Rx_Id
                             join c in db.DrugMaster on a.DrugMst_Id_FK equals c.Id
                             join d in db.DrugType on c.DT_Id_FK equals d.Id
                             join e in db.Unit on c.UT_Id_FK equals e.Id
                             where a.Dtl_Id == Dtl_Id
                             select new PPD_By_Id
                             {
                                 Dtl_Id = a.Dtl_Id,
                                 Rx_Id_FK = a.Rx_Id_FK,
                                 PrescriptionDate = b.Prescription_date,
                                 DrugMst_Id_FK = a.DrugMst_Id_FK,
                                 DrugMst_Name = c.DrugName,
                                 //DrugMst_DT_Id_FK = c.DT_Id_FK,
                                 DM_DT_Type = d.Type,
                                 DrugMst_Strength = c.Strength,
                                 //DrugMst_UT_Id_FK = c.UT_Id_FK,
                                 DM_UT_Unit = e.DrugUnit,
                                 Desc = c.Description,
                                 //Rx_Desc = a.Rx_Desc,
                                 Rx_Dosage = a.Rx_Dosage,
                                 Rx_Course = a.Rx_Course,
                                 Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
