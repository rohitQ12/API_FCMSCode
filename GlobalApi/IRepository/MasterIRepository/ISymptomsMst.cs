using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ISymptomsMst
    {
        Task<SymptomsMst> InsertSymptomsMst(SymptomsMst lead);
        Task<SymptomsMst> UpdateSymptomsMst(SymptomsMst lead);
        Task<List<SymptomsMst>> GetAllSymptomsMst();
        Task<List<SymptomsMst_DD>> GetSymptomsMst_DD();
        Task<SymptomsMst> GetSymptomsMstById(int Id);
        Task<SymptomsMst> DeleteSymptomsMst(int Id);

    }
}
