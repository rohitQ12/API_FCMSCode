using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class DoctorLocationRepository : IDoctorLocation
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DoctorLocationRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<string> InsertDoctorLocation(List<DoctorLocation> lead , int DO_Id)
        {
            try
            {
                foreach(DoctorLocation loc in lead)
                {
                    var duplicate = await db.DoctorLocation.FirstOrDefaultAsync(x => x.doc_Id_FK == loc.doc_Id_FK && x.Latitude == loc.Latitude && x.Longitude == loc.Longitude);
                    if (duplicate == null)
                    {
                        int id = await primarykeyvalue.primary_key("DoctorLocation");
                        DoctorLocation obj = new DoctorLocation()
                        {
                            Id = id,
                            doc_Id_FK = DO_Id,
                            Latitude = loc.Latitude,
                            Longitude = loc.Longitude,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                            status = 1
                        };
                        var result = await db.DoctorLocation.AddAsync(obj);
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
        public async Task<DoctorLocation> UpdateDoctorLocation(List<DoctorLocation> lead , int DO_Id)
        {
            try
            {
                foreach(DoctorLocation dloc in lead)
                {
                    var result = await db.DoctorLocation.FirstOrDefaultAsync(x => x.Id == dloc.Id);
                    if (result != null)
                    {
                        result.Id = dloc.Id;
                        result.doc_Id_FK = DO_Id;
                        result.Latitude = dloc.Latitude;
                        result.Longitude = dloc.Longitude;
                        result.modified_by = 1;
                        result.modified_date = DateTime.Now;
                        result.delete_flag = false;
                        result.status = 2;
                        await db.SaveChangesAsync();
                        return result;
                    }

                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<GetDoctorLoc>> GetAllDoctorLocation()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DoctorLocation
                                 join b in db.Doctor on a.doc_Id_FK equals b.DO_Id
                                 orderby a.Id descending
                                 select new GetDoctorLoc
                                 {
                                     Id = a.Id,
                                     doc_Id_FK = a.doc_Id_FK,
                                     doc_name = String.Concat(b.DO_FirstName,b.DO_LastName),
                                     Latitude = a.Latitude,
                                     Longitude = a.Longitude,
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
        public async Task<DoctorLocation> DeleteDoctorLocation(int Id)
        {
            try
            {
                var result = await db.DoctorLocation.FirstOrDefaultAsync(x => x.Id == Id);
                if (result != null)
                {
                    result.Id = Id;
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
        public async Task<GetDoctorLoc> GetDoctorLocationById(int Id)
        {
            if (db != null)
            {
                var query = (from a in db.DoctorLocation
                             join b in db.Doctor on a.doc_Id_FK equals b.DO_Id
                             where a.Id == Id
                             select new GetDoctorLoc
                             {
                                 Id = a.Id,
                                 doc_Id_FK = a.doc_Id_FK,
                                 doc_name = String.Concat(b.DO_FirstName, b.DO_LastName),
                                 Latitude = a.Latitude,
                                 Longitude = a.Longitude,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }

    }
}
