using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Religion_MSTRepository: IReligion_MSTRepository
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public Religion_MSTRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<List<Religion_MST>> GetAllReligion()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Religion_MST
                                 where a.Id != 0
                                 orderby a.Id descending
                                 select a);
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<Religion_DD>> GetReligion_DD(int Nationality_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Religion_MST
                             where a.Nationality_ID_FK == Nationality_Id && a.delete_flag == false
                             && a.status == 3 && a.Id != 0
                             select new Religion_DD
                             {
                                 Id = a.Id,
                                 Religion = a.Religion,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }



    }
}
