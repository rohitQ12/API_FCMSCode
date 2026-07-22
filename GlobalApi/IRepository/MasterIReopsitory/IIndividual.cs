using GlobalApi.JsonFile;
using GlobalApi.Models.Master;
using static GlobalApi.Models.Master.Individual;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IIndividual
    {
        Task<UserRegisterResult> InsertIndividual(Individual_Images lead, string UserId);
        Task<UserRegisterResult> UpdateIndividual(Individual_Images lead);
       Task<List<GetAllIndividual>> GetAllIndividual();
        //Task<List<Assistant_DD>> GetAssistant_DD(int? Assi_Hos_Id_FK, string roleaction);
        Task<IndividualById> GetIndividualById(int Ind_Id, string roleaction);
        Task<UserRegisterResult> DeleteIndividual(int individual_id);
        Task<dynamic> GetIndividualDetails();
        Task<UserRegisterResult> ApproveIndividual(ApproveIndividual approveIndividua);

    }
}
