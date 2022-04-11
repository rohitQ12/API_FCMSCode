using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IEmpType
    {
        Task<Emp_Type> InsertEmpType(Emp_Type lead);
        Task<Emp_Type> UpdateEmpType(Emp_Type lead);
        Task<List<Emp_Type>> GetAllEmpType();
        Task<List<Emp_Type_DD>> GetEmpType_DD();
        Task<Emp_TypeById> GetEmpTypeById(int emptype_id);
        Task<Emp_Type> DeleteEmpType(int emptype_id);

    }
}
