using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IConsultation
    {
        //Task<Consultation> InsertConsultation(Consultation lead);
        Task<Consultation> UpdateConsultation(Consultation lead);
        Task<Consultation> UpdatePhcConsultation(Consultation lead);
        Task<List<GetAllConsultation>> GetAllConsultation();
        Task<List<GetAllPhcConsultation>> GetAllPhcConsultation();
        Task<List<ConsultationBy_Id>> GetConsultationById(int CON_PR_Id_FK);
        Task<List<ConsultationBy_Id>> GetAdminConsultationById(int CON_Id);
        Task<List<PhcConsultationBy_Id>> GetPhcConsultationById(int CON_Id);
        Task<Consultation> DeleteConsultation(int CON_Id);
        Task<Consultation> CloseConsultation(int CON_Id);
    }
}
