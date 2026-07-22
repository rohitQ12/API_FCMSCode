using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.ComplaintIRepository;
using GlobalApi.IRepository.StatesAndCitiesIRepository;
using GlobalApi.Models.ComplaintModels;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GlobalApi.Repository.StatesAndCitiesRepository
{
    public class StatesAndCitiesRepository : StatesAndCitiesIRepository
    {
        SqlConnection con;


        private ADO_Configrations ado_Configurations = new ADO_Configrations();


        public async Task<List<StatesModels>> GetAllStates()
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAllStates";

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<StatesModels> StatesList = new List<StatesModels>();

            StatesList = (from DataRow drr in dt.Rows
                            select new StatesModels()
                            {
                                state_id = (int)drr["state_id"],
                                state_name = (string)drr["state_name"],
                                state_code = (string)drr["state_code"],
                                category = (string)drr["category"]
                            }).ToList();

            return StatesList;
        }


        public async Task<List<CitiesModels>> GetCitiesbByState_id(int state_id)
        {
            con = ado_Configurations.connection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetCitiesbByState_id";
            cmd.Parameters.AddWithValue("@state_id", state_id);

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<CitiesModels> CitiesList = new List<CitiesModels>();

            CitiesList = (from DataRow drr in dt.Rows
                               select new CitiesModels()
                               {
                                   city_id = (int)drr["city_id"],
                                   city_name = (string)drr["city_name"],
                                   state_id = (int)drr["state_id"]
                         
                               }).ToList();

            return CitiesList;
        }


    }
}