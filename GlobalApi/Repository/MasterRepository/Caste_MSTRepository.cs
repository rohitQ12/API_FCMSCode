using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    public class Caste_MSTRepository: ICaste_MSTRepository
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public Caste_MSTRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }


        public async Task<List<GetAllCasteMst>> GetAllCaste()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Caste_MST
                                 join b in db.Nationality_MST on a.Nationality_ID_FK equals b.Id
                                 join c in db.Religion_MST on a.Religion_ID_FK equals c.Id
                                 join d in db.Status on a.status equals d.sts_id
                                 where a.Id != 0
                                 orderby a.Id descending
                                 select new GetAllCasteMst
                                 {
                                     Id = a.Id,
                                     Nationality_ID_FK = a.Nationality_ID_FK,
                                     Nationality = b.Nationality,
                                     Religion_ID_FK = a.Religion_ID_FK,
                                     Religion = c.Religion,
                                     Caste = a.Caste,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = d.sts_name,
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

        public async Task<List<Caste_DD>> GetCaste_DD(int Religion_id)
        {
            if (db != null)
            {
                var query = (from a in db.Caste_MST
                             where a.Religion_ID_FK == Religion_id && a.delete_flag == false
                             && a.status == 3 && a.Id != 0
                             select new Caste_DD
                             {
                                 Id = a.Id,
                                 Caste = a.Caste,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
