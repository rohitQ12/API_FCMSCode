using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class ParametersRepository : IParameters
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public ParametersRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        //public async Task<string> InsertParameters(List<Parameters> lead , int Appt_Id)
        //{
        //    try
        //    {
        //        foreach(Parameters para in lead)
        //        {
        //            var duplicate = await db.Parameters.FirstOrDefaultAsync(x => x.PA_APPT_Id_FK == Appt_Id);
        //            if (duplicate == null)
        //            {
        //                int id = await primarykeyvalue.primary_key("Parameters");
        //                Parameters obj = new Parameters()
        //                {
        //                    PA_Id = id,
        //                    PA_Code = id <= 09 ? "PA" + '0' + Convert.ToString(id) : "PA" + Convert.ToString(id),
        //                    //PA_Code = lead.PA_Code,
        //                    PA_APPT_Id_FK = Appt_Id,
        //                    PA_Height = para.PA_Height,
        //                    PA_Weight = para.PA_Weight,
        //                    PA_TempInFahrenheit = para.PA_TempInFahrenheit,
        //                    PA_TempInCelsius = para.PA_TempInCelsius,
        //                    //PA_Temperature = lead.PA_Temperature,
        //                    PA_BloodPressure = para.PA_BloodPressure,
        //                    PA_Sugar = para.PA_Sugar,
        //                    PA_PulseRate = para.PA_PulseRate,
        //                    PA_RespiratoryRate = para.PA_RespiratoryRate,
        //                    PA_ECG = para.PA_ECG,
        //                    PA_OxygenSaturation = para.PA_OxygenSaturation,
        //                    PA_UserId_FK = para.PA_UserId_FK,
        //                    created_by = 1,
        //                    created_date = DateTime.Now,
        //                    delete_flag = false,
        //                    status = 1
        //                };
        //                var result = await db.Parameters.AddAsync(obj);
        //                await db.SaveChangesAsync();

        //            }
        //            return "Data already inserted";
        //        }
        //        return "Record insert successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        public async Task<Parameters> UpdateParameters(Parameters lead)
        {
            try
            {
                var result = await db.Parameters.FirstOrDefaultAsync(x => x.PA_Id == lead.PA_Id);
                if (result != null)
                {
                    result.PA_Id = lead.PA_Id;
                    result.PA_Code = lead.PA_Code;
                    result.Appt_Id = lead.Appt_Id;
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
                    result.PA_UserId_FK = lead.PA_UserId_FK;
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
        public async Task<List<GetAllParameters>> GetAllParameters()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Parameters
                                 //join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                                 orderby a.PA_Id descending
                                 select new GetAllParameters
                                 {
                                     PA_Id = a.PA_Id,
                                     PA_Code = a.PA_Code,
                                     Appt_Id = a.Appt_Id,
                                     MAppt_Id = a.MAppt_Id,
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
                                     PA_UserId_FK = a.PA_UserId_FK,
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

        public async Task<Parameters> DeleteParameters(int PA_Id)
        {
            try
            {
                var result = await db.Parameters.FirstOrDefaultAsync(x => x.PA_Id == PA_Id);
                if (result != null)
                {
                    result.PA_Id = PA_Id;
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
        public async Task<List<ParametersBy_Id>> GetParametersById(int PA_PR_Id_FK)
        {
            if (db != null)
            {
                var query = (from a in db.Parameters
                             //join b in db.PatientAppointment on a.Appt_Id equals b.Appt_Id
                             //where b.Appt_PatientId_FK == PA_PR_Id_FK
                             select new ParametersBy_Id
                             {
                                 PA_Id = a.PA_Id,
                                 PA_Code = a.PA_Code,
                                 Appt_Id = a.Appt_Id,
                                 MAppt_Id = a.MAppt_Id,
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
                                 PA_UserId_FK = a.PA_UserId_FK,
                                 delete_flag = a.delete_flag,
                                 status = a.status
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
