using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IHos_Type
    {
        Task<Hos_Type> InsertHos_Type(Hos_Type lead);
        Task<Hos_Type> UpdateHos_Type(Hos_Type lead);
        Task<List<Hos_Type>> GetAllHos_Type();
        Task<List<HosType_DD>> GetHos_Type_DD();
        //Task<Hos_TypeBy_Id> GetHos_TypeById(int Id);
        Task<Hos_Type> DeleteHos_Type(int Id);

    }
}
