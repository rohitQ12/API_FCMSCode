using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Consult_ParametersRepository : IConsult_Parameters
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Consult_ParametersRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Consult_Parameters> UpdateConsult_Parameters(Consult_Parameters lead)
        {
            try
            {
                var result = await db.Consult_Parameters.FirstOrDefaultAsync(x => x.CON_Id == lead.CON_Id);
                if (result != null)
                {
                    //result.PA_Id = lead.PA_Id;
                    //result.CON_Id = lead.CON_Id;
                    //result.PA_Code = lead.PA_Code;
                    result.PA_Height = lead.PA_Height;
                    result.PA_Weight = lead.PA_Weight;
                    result.PA_TempInFahrenheit = lead.PA_TempInFahrenheit;
                    result.PA_TempInCelsius = lead.PA_TempInCelsius;
                    result.PA_BloodPressure = lead.PA_BloodPressure;
                    result.PA_Sugar = lead.PA_Sugar;
                    result.PA_PulseRate = lead.PA_PulseRate;
                    result.PA_RespiratoryRate = lead.PA_RespiratoryRate;
                    result.PA_ECG = lead.PA_ECG;
                    result.PA_OxygenSaturation = lead.PA_OxygenSaturation;
                    result.PA_Hemoglobin = lead.PA_Hemoglobin;
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
        public async Task<List<GetAllCPara>> GetAllConsult_Parameters()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consult_Parameters
                                 orderby a.PA_Id descending
                                 select new GetAllCPara
                                 {
                                     PA_Id = a.PA_Id,
                                     PA_Code = a.PA_Code,
                                     CON_Id = a.CON_Id,
                                     PA_Height = a.PA_Height,
                                     PA_Weight = a.PA_Weight,
                                     PA_TempInFahrenheit = a.PA_TempInFahrenheit,
                                     PA_TempInCelsius = a.PA_TempInCelsius,
                                     PA_BloodPressure = a.PA_BloodPressure,
                                     PA_Sugar = a.PA_Sugar,
                                     PA_PulseRate = a.PA_PulseRate,
                                     PA_RespiratoryRate = a.PA_RespiratoryRate,
                                     PA_ECG = a.PA_ECG,
                                     PA_OxygenSaturation = a.PA_OxygenSaturation,
                                     PA_Hemoglobin = a.PA_Hemoglobin,
                                     //PA_UserId_FK = a.PA_UserId_FK,
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
        public async Task<Consult_Parameters> DeleteConsult_Parameters(int CON_Id)
        {
            try
            {
                var result = await db.Consult_Parameters.FirstOrDefaultAsync(x => x.CON_Id == CON_Id);
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
        public async Task<List<CParaBy_Id>> GetConsult_ParametersById(int CON_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Consult_Parameters
                             where a.CON_Id == CON_Id
                             select new CParaBy_Id
                             {
                                 PA_Id = a.PA_Id,
                                 PA_Code = a.PA_Code,
                                 CON_Id = a.CON_Id,
                                 PA_Height = a.PA_Height,
                                 PA_Weight = a.PA_Weight,
                                 PA_TempInFahrenheit = a.PA_TempInFahrenheit,
                                 PA_TempInCelsius = a.PA_TempInCelsius,
                                 PA_BloodPressure = a.PA_BloodPressure,
                                 PA_Sugar = a.PA_Sugar,
                                 PA_PulseRate = a.PA_PulseRate,
                                 PA_RespiratoryRate = a.PA_RespiratoryRate,
                                 PA_ECG = a.PA_ECG,
                                 PA_OxygenSaturation = a.PA_OxygenSaturation,
                                 PA_Hemoglobin = a.PA_Hemoglobin,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
