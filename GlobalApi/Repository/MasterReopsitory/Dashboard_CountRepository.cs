using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;



namespace GlobalApi.Repository.MasterRepository
{
    public class Dashboard_CountRepository : IDashboard_Count
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private readonly IConfigurationRoot configurationRoot = null!;
        public Dashboard_CountRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();

        }

        public Dashboard MapToDashboardData(Microsoft.Data.SqlClient.SqlDataReader reader)
{
    return new Dashboard()
    {

        student_count = Convert.ToInt32(reader["student_count"]),
        corporate_count = Convert.ToInt32(reader["corporate_count"]),
        Institution_count = Convert.ToInt32(reader["Institution_count"]),
        Individual_count = Convert.ToInt32(reader["Individual_count"]),
        today_student_count = Convert.ToInt32(reader["today_student_count"]),
        today_Institutestudent_count = Convert.ToInt32(reader["today_Institutestudent_count"]),
        today_Corporatestudent_count = Convert.ToInt32(reader["today_Corporatestudent_count"]),
        today_Institution_count = Convert.ToInt32(reader["today_Institution_count"]),
        today_corporate_count = Convert.ToInt32(reader["today_corporate_count"]),
        today_Individual_count = Convert.ToInt32(reader["today_Individual_count"]),
        total_InstitutionSub_count = Convert.ToInt32(reader["total_InstitutionSub_count"]),
        today_InstitutionSub_count = Convert.ToInt32(reader["today_InstitutionSub_count"]),
        total_CorporateSub_count = Convert.ToInt32(reader["total_CorporateSub_count"]),
        today_CorporateSub_count = Convert.ToInt32(reader["today_CorporateSub_count"]),

    };
}

        //      =    };
        //}


        //public Refund_Details MapToRefundDetailsData(Microsoft.Data.SqlClient.SqlDataReader reader)
        //{
        //    return new Refund_Details()
        //    {
        //        tracking_id = Convert.ToString(reader["tracking_id"]),
        //        refund_ref_no = Convert.ToString(reader["refund_ref_no"]),
        //        refund_date = Convert.ToDateTime(reader["refund_date"]),
        //        refund_amount = Convert.ToDecimal(reader["refund_amount"]),
        //        sts_name = Convert.ToString(reader["sts_name"]),
        //        Pr_Name = Convert.ToString(reader["Pr_Name"]),
        //        Hos_HospitalName = Convert.ToString(reader["Hos_HospitalName"]),
        //        PR_PatientCode = Convert.ToString(reader["PR_PatientCode"]),

        //    };
        //}



        //public async Task<List<Payment_Details>> GetDailyPaymentReportData(string reportType, int hos_branch_id, string selectedDate)
        //{
        //    using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
        //    {
        //        using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Daily_ReportDetails", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ReportType", reportType);
        //            cmd.Parameters.AddWithValue("@hos_branch_id", hos_branch_id);
        //            cmd.Parameters.AddWithValue("@selectedDate", selectedDate);

        //            var response = new List<Payment_Details>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapTopamentDetails(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}

        //public async Task<List<patient_Details>> GetDailyPatientReportData(string reportType, int hos_branch_id, string selectedDate)
        //{
        //    using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
        //    {
        //        using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Daily_ReportDetails", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ReportType", reportType);
        //            cmd.Parameters.AddWithValue("@hos_branch_id", hos_branch_id);
        //            cmd.Parameters.AddWithValue("@selectedDate", selectedDate);

        //            var response = new List<patient_Details>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapTopatientDetailsData(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}






        //public async Task<List<Phc_Appoinment_Details>> GetDailyPhcAppoinmentReportData(string reportType, int hos_branch_id, string selectedDate)
        //{
        //    using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
        //    {
        //        using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Daily_ReportDetails", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ReportType", reportType);
        //            cmd.Parameters.AddWithValue("@hos_branch_id", hos_branch_id);
        //            cmd.Parameters.AddWithValue("@selectedDate", selectedDate);

        //            var response = new List<Phc_Appoinment_Details>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapTophcAppoinmentDetailsData(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}



        //public async Task<List<Refund_Details>> GetDailyRefundReportData(string reportType, int hos_branch_id, string selectedDate)
        //{
        //    using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
        //    {
        //        using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("Daily_ReportDetails", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ReportType", reportType);
        //            cmd.Parameters.AddWithValue("@hos_branch_id", hos_branch_id);
        //            cmd.Parameters.AddWithValue("@selectedDate", selectedDate);

        //            var response = new List<Refund_Details>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToRefundDetailsData(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}

        public async Task<List<Dashboard>> GetDashboardData()
        {
            using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
            {
                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("usp_GetDashboard_Data", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                  

                    var response = new List<Dashboard>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToDashboardData(reader));
                        }
                    }
                    return response;
                }
            }
        }








    }
}
