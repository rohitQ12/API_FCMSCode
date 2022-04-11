using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsultation
    {
        //Task<Consultation> InsertConsultation(Consultation lead);
        Task<Consultation> UpdateConsultation(Consultation lead);
        Task<List<GetAllConsultation>> GetAllConsultation();
        Task<ConsultationBy_Id> GetConsultationById(int CON_Id);
        Task<Consultation> DeleteConsultation(int CON_Id);

    }
}
