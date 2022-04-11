using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISection
    {
        Task<Section> InsertSection(Section lead);
        Task<Section> UpdateSection(Section lead);
        Task<List<GetAllSection>> GetAllSection();
        Task<List<Section_DD>> GetSection_DD();
        Task<SectionById> GetSectionById(int Section_Id);
        Task<Section> DeleteSection(int Section_Id);
    }
}
