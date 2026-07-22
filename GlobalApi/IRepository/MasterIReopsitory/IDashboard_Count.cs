using GlobalApi.Models.Master;

namespace GlobalApi.IRepository.MasterIRepository
{
    public interface IDashboard_Count
    {       
        Task<List<Dashboard>> GetDashboardData();
       // Task<List<Dashboard_MedicalAssistant>> GetDashboardData_MedicalAssistant(string roleaction, string rolename, int hos_branch_id, int doctor_id, int pr_id);
       // Task<List<Dashboard_Doctor>> GetDashboardData_Doctor(string roleaction, string rolename, int hos_branch_id, int doctor_id, int pr_id);
       // //Landing Page
       //// Task<List<Dashboard>> Get_Homepage_statusCount();      

       // Task<Patient_DashAppt_Mobile> GetPatientDashAppt_Mobile(string username);
       // Task<Patient_DashCons_Mobile> GetPatientDashCons_Mobile(string username);

       // Task<Doctor_DashAppt_Mobile> GetDoctorDashAppt_Mobile(string username);
       // Task<Doctor_DashCons_Mobile> GetDoctorDashCons_Mobile(string username);

       // Task<List<CD_Dashboard_Mobile>> GetAllCDisciplines_Mobile();
       
       // Task<List<object>> GetCDDoctorDetail_Mobile(int cd_id);

       // Task<List<Refund_Details>> GetDailyRefundReportData(string reportType, int hos_branch_id, string selectedDate);
       // Task<List<Phc_Appoinment_Details>> GetDailyPhcAppoinmentReportData(string reportType, int hos_branch_id, string selectedDate);
       // Task<List<patient_Details>> GetDailyPatientReportData(string reportType, int hos_branch_id, string selectedDate);
       // Task<List<Payment_Details>> GetDailyPaymentReportData(string reportType, int hos_branch_id, string selectedDate);
       // Task<List<Dashboard_Patient>> GetDashboardData_Patinet(string roleaction, string rolename, int hos_branch_id, int doctor_id, int pr_Id);


    }
}
