using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IGram
    {
        Task<Gram> InsertGram(Gram lead);
        Task<Gram> UpdateGram(Gram lead);
        Task<List<Gram_DD>> GetGram_DD(int Taluk_id);
        Task<Gram> DeleteGram(int Gram_id);
        Task<List<GetGramTaluk>> GetAllGram();
        Task<string> ApproveGram(int Gram_id, string? Remarks);

    }
}
