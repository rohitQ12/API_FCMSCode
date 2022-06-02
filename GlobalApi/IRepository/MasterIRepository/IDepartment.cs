using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDepartment
    {
        Task<Department> InsertDepartment(Department lead);
        Task<Department> UpdateDepartment(Department lead);
        Task<List<GetAllDepartment>> GetAllDepartment();
        Task<List<Department_DD>> GetDepartment_DD();
        Task<DepartmentById> GetDepartmentById(int Dept_Id);
        Task<Department> DeleteDepartment(int Dept_Id);
    }
}
