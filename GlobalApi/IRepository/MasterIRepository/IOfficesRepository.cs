using GlobalApi.Models.Master;
using GlobalApi.Models.Authentication;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IOfficesRepository
    {
        Task<Offices> InsertOffice(Offices offices);
        Task<OfficeRoles> AddOfficeRoles(string userid, int? OfficeId);
        Task<Offices> UpdateOffice(Offices offices);
        //Task<List<SubMenuPage>> GetAllAppPage();
        Task<Offices> DeleteOffice(int Id);
        Task<Offices> GetOfficeById(int Id);
        Task<List<Offices>> GetOffice();
        Task<List<Usercategory_DD>> GetOfficeCategory_DD();
    }
}
