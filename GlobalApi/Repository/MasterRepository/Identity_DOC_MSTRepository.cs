using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterRepository
{
    
    public class Identity_DOC_MSTRepository: IIdentity_DOC_MSTRepository
    {
        private readonly GlobalContext db;
        private readonly IPrimarykeyvalue primarykeyvalue;
        public Identity_DOC_MSTRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }

        public async Task<List<Identity_DOC_MST>> GetAllIdentity()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Identity_DOC_MST
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
        public async Task<List<IdentityDD>> GetIdentityDD()
        {
            if (db != null)
            {
                var query = (from a in db.Identity_DOC_MST
                             where a.delete_flag == false && a.status != 6
                             && a.Id != 0
                             select new IdentityDD
                             {
                                 PR_IDN_Id_FK = a.Id,
                                 DOC_Name= a.DOC_Name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }

    }
}
