using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DiseasesDtlRepository : IDiseasesDtl
    {
        GlobalContext db;
        //public readonly string _connectionString;
        private IPrimarykeyvalue primarykeyvalue;
        public DiseasesDtlRepository(GlobalContext _db)
        {
            db = _db;
            primarykeyvalue = new Primarykeyvalue(_db);
        }
        public async Task<string> InsertDiseasesDtl(List<DiseasesDtl> lead , int Appt_Id)
        {
            try
            {
                foreach(DiseasesDtl ddtl in lead)
                {
                    var duplicate = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Dis_Id_FK == ddtl.Dis_Id_FK && x.Ddtl_APPT_Id_FK == Appt_Id);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DiseasesDtl");
                        DiseasesDtl obj = new DiseasesDtl()
                        {
                            Ddtl_Id = id,
                            Dis_Id_FK = ddtl.Dis_Id_FK,
                            Ddtl_APPT_Id_FK = Appt_Id,
                            Remarks = ddtl.Remarks,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var result = await db.DiseasesDtl.AddAsync(obj);
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
        public async Task<DiseasesDtl> UpdateDiseasesDtl(DiseasesDtl lead)
        {
            try
            {
                var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == lead.Ddtl_Id);
                if (result != null)
                {
                    result.Ddtl_Id = lead.Ddtl_Id;
                    result.Dis_Id_FK = lead.Dis_Id_FK;
                    result.Ddtl_APPT_Id_FK = lead.Ddtl_APPT_Id_FK;
                    result.Remarks = lead.Remarks;
                    result.modified_by = 1;
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
        public async Task<List<GetAllDiseasesDtl>> GetAllDiseasesDtl()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiseasesDtl
                                 orderby a.Ddtl_Id descending
                                 select new GetAllDiseasesDtl
                                 {
                                     Ddtl_Id = a.Ddtl_Id,
                                     Dis_Id_FK = a.Dis_Id_FK,
                                     Ddtl_APPT_Id_FK = a.Ddtl_APPT_Id_FK,
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

        public async Task<DiseasesDtl> DeleteDiseasesDtl(int Ddtl_Id)
        {
            try
            {
                var result = await db.DiseasesDtl.FirstOrDefaultAsync(x => x.Ddtl_Id == Ddtl_Id);
                if (result != null)
                {
                    result.Ddtl_Id = Ddtl_Id;
                    result.delete_flag = true;
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
        public async Task<GetDiseaseDtlById> GetDiseasesDtlById(int Ddtl_Id)
        {
            if (db != null)
            {
                var query = (from a in db.DiseasesDtl
                             where a.Ddtl_Id == Ddtl_Id
                             select new GetDiseaseDtlById
                             {
                                 Ddtl_Id = a.Ddtl_Id,
                                 Dis_Id_FK = a.Dis_Id_FK,
                                 Ddtl_APPT_Id_FK = a.Ddtl_APPT_Id_FK,
                                 Remarks = a.Remarks,
                                 delete_flag = a.delete_flag,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
