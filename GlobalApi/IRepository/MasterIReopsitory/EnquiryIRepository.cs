using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{

    public interface EnquiryIRepository
    {
        Task<string> InsertEnquiry(Enquiry_details lead);
        Task<List<GetAllEnquire_Type>> GetAllEnquire_Type();
        Task<List<GetAllEnquiry_details>> GetAllEnquiry_details();
        Task<List<GetAllEnquiry_detailsById>> GetAllEnquiry_detailsById(string cus_eq_phoneNo);
        Task<List<GetAllEnquiry_detailsById>> GetAllEnquiryInformation();
        Task<string> DeleteEnquiry(int id);

    }
}
