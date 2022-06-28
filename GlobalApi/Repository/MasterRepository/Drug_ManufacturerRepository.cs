using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Drug_ManufacturerRepository : IDrug_Manufacturer
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public Drug_ManufacturerRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<List<Drug_Manufacturer>> GetAllDrug_Manufacturer()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Manufacturers
                                 where a.Status != 0 && a.Drg_manuf_delete_flag == false
                                 orderby a.Drg_manuf_id descending
                                 select new Drug_Manufacturer
                                 {
                                     Drg_manuf_id = a.Drg_manuf_id,
                                     Drg_manuf_code = a.Drg_manuf_code,
                                     Drg_manuf_name = a.Drg_manuf_name,
                                     Status = a.Status,
                                     Drg_manuf_delete_flag = a.Drg_manuf_delete_flag,
                                     Remarks = a.Remarks
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

        public async Task<List<Drug_ManufacturerDD>> GetDrug_Manufacturer_DD()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Drug_Manufacturers
                                 where a.Status != 6 && a.Drg_manuf_delete_flag == false
                                 orderby a.Drg_manuf_id descending
                                 select new Drug_ManufacturerDD
                                 {
                                     Drg_manuf_id = a.Drg_manuf_id,
                                     Drg_manuf_code = a.Drg_manuf_code,
                                     Drg_manuf_name = a.Drg_manuf_name,
                                     Status = a.Status
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
    }
}
