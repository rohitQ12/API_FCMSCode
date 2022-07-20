using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IGram
    {
        Task<bool> InsertGram(Gram lead);
        Task<bool> UpdateGram(Gram lead);
        Task<List<Gram_DD>> GetGram_DD(int Taluk_id);
        Task<bool> DeleteGram(int Gram_id);
        Task<List<GetGramTaluk>> GetAllGram();
        Task<bool> ApproveGram(ApproveGram lead);

    }
}
