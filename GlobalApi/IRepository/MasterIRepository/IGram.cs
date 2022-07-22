using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IGram
    {
        Task<string> InsertGram(Gram lead);
        Task<string> UpdateGram(Gram lead);
        Task<List<Gram_DD>> GetGram_DD(int Taluk_id);
        Task<string> DeleteGram(int Gram_id);
        Task<List<GetGramTaluk>> GetAllGram();
        Task<string> ApproveGram(ApproveGram lead);

    }
}
