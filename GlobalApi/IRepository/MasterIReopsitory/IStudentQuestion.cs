using GlobalApi.JsonFile;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIReopsitory
{
    public interface IStudentQuestion
    {
        Task<string> InsertStuQue(StuqueInsert lead);
        Task <string> SendStuQue(StuQueries lead);
        Task<List<StuqueGetAll>> GetAll_StuQue();
        Task<string> UpdateStuQue(StuqueUpdate lead);

        Task<List<StuqueGetAll>> GetAll_StuQueById(string cus_eq_phoneNo);


        Task<string> DeleteStuQue(int id);




    }
}
