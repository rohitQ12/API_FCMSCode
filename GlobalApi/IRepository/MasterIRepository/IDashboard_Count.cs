namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDashboard_Count
    {
        int GetPatient_Count();
        int GetNetworkHospital_Count();
        int GetHospital_Count();
        int GetPharmacy_Count();
        int GetDiagnostic_Count();
        int GetTotalAppointment_Count();
        int GetTodayAppointment_Count();
        int GetTodayConsultation_Count();
        int GetTotalConsultation_Count();
        int Getreferal_Count();

    }
}
