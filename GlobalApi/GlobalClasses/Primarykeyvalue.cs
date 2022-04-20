using GlobalApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.GlobalClasses
{
    public interface IPrimarykeyvalue
    {
        Task<int> primary_key(string tablename);
    }
    public class Primarykeyvalue : IPrimarykeyvalue
    {

        GlobalContext db;
        public Primarykeyvalue()
        {
            db = new GlobalContext();
        }
        public async Task<int> primary_key(string tablename)
        {
            try
            {
                var pk_present_value = (from d in db.DocPkValue where d.PkName == tablename select d.PkPresentValue + 1).FirstOrDefault();
                var result = await db.DocPkValue.FirstOrDefaultAsync(y => y.PkName == tablename);
                if (result != null)
                {
                    result.PkPresentValue = Convert.ToInt32(pk_present_value);
                    result.PkPreviousValue = Convert.ToInt32(pk_present_value) - 1;
                    result.CreatedDate = DateTime.Now;
                    result.ModifiedDate = DateTime.Now;
                    result.DeletedDate = DateTime.Now;
                    result.DeleteFlag = false;
                    result.Status = 1;
                    await db.SaveChangesAsync();
                }
                return Convert.ToInt32(pk_present_value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
