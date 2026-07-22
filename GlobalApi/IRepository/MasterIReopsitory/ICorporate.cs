using GlobalApi.JsonFile;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface ICorporate
    {
        Task<UserRegisterResult> InsertCorporate(Corporate_Images lead, string UserId);
        Task<UserRegisterResult> UpdateCorporate(Corporate_ImagesUP lead);
        //Task<string> UpdateDoctor_Online(Doctor_ImagesUP_Online lead);
        Task<List<GetAllCorporate>> GetAllCorporate();
        Task<CorporateById> GetCorporateById(int CO_Id);
        Task<UserRegisterResult> DeleteCorporate(int CO_Id);
        //Task<List<Doctor_DD>> Doctor_DD(int SP_Id);
        Task<dynamic> GetCorporateDetails(int corporateId);
        Task<UserRegisterResult> ApproveCorporate(ApproveCorporate lead);
        Task<List<GetAllCorporateIds>> GetAllCorporateIds();
        //Task<List<Doctor_DD>> DoctorDD(int? DO_HO_Id_FK, int? DO_Id_Fk, string? Rolecategory, string? RoleName);
        //Task<dynamic> GetGeneralDoctorDetails();
        //Task<List<getclinicdoctor>> GetSpecialityDoctorDetails();
        //Task<dynamic> GetMultiSpecialityDoctorDetails();
        //Task<dynamic> GetDoctorByHospitalId(int HospitalId);
        //Task<List<getclinicdoctor>> GetClinicDoctorDetails();
        //Task<List<getclinicdoctor>> GetHospitalDoctorDetails();
        //Task<List<getdisplinedoctor>> GetDoctorbyDispline(int Des_Id);

        //Task<dynamic> GetHospitallogo_doctor(int? DoctorId, string? rolename);
        //Task<dynamic> GetHospitallogo_MediclAssistant(int? assid, string? rolename);
        //Task<dynamic> GetHospitallogo_HospitalAdmin(int? hospitalid, string? rolename);
        //Task<List<avilabledoctor_today>> GetAvailableDoctor_Todaydate();
        //Task<List<gethospitaldd_online>> GetHospital_DD_Online();
        //Task<List<getmothertounge_dd>> Getlang_DD_Online();
        //Task<UserCustomResponse> UploadDoctorPhoto_Online(Doctor_Profile_Photo profile_Photo, string user_Name);
        //Task<UserCustomResponse> UploadDoctorExpDocs_Online(Doctor_Exp_Documents_Online documents_online, string user_Name);
        //Task<UserCustomResponse> UploadDoctorEduDocs_Online(Doctor_Edu_Documents_Online documents_online, string user_Name);
        //Task<List<getdisplinedoctor_ById>> getdisplinedoctor_ById(int DO_Id);
        //Task<List<getdisplinedoctor_ById>> getSMS_support();
    }
}
