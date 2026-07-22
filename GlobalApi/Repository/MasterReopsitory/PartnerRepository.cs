using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Models.Master.YourNamespace.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using GlobalContext = GlobalApi.Data.GlobalContext;

namespace GlobalApi.Repository.MasterRepository
{
    public class PartnerRepository : IPartnerRepository
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly FileUpload fileUpload;
        public PartnerRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();

        }


        public async Task<List<Partner>> GetAllPartnersList()
        {
            var partners = new List<Partner>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllPartnerList";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            partners.Add(new Partner
                            {
                                PartnerId = reader.GetInt32(reader.GetOrdinal("PartnerId")),
                                PartnerCode = reader["PartnerCode"].ToString(),
                                PartnerName = reader["PartnerName"].ToString(),
                                PartGender = reader["PartGender"] as string,
                                PartMobileNumber = reader["PartMobileNumber"] as string,
                                PartEmail = reader["PartEmail"] as string,
                                PartAddress = reader["PartAddress"] as string,
                                PartCity = reader["PartCity"] as string,
                                PartState = reader["PartState"] as string,
                                PartZipcode = reader["PartZipcode"] as string,
                                PartLicenseNumber = reader["PartLicenseNumber"] as string,
                                PartLicenseExpiryDate = reader["PartLicenseExpiryDate"] as DateTime?,
                                PartIDNumber = reader["PartIDNumber"] as string,
                                PartVehicleType = reader["PartVehicleType"] as string,
                                PartVehicleNumber = reader["PartVehicleNumber"] as string,
                                Inactive = Convert.ToChar(reader["Inactive"]),
                                CreatedBy = reader["CreatedBy"] as string,
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                ModifiedBy = reader["ModifiedBy"] as string,
                                ModifiedDate = reader["ModifiedDate"] as DateTime?
                            });
                        }
                    }
                }
            }

            return partners;

        }



        public async Task<string> UpdatePartnerDetails(Partner partner)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.Database.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdatePartnerDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PartnerId", partner.PartnerId);
                        cmd.Parameters.AddWithValue("@PartnerName", (object?)partner.PartnerName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartGender", (object?)partner.PartGender ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartMobileNumber", (object?)partner.PartMobileNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartEmail", (object?)partner.PartEmail ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartAddress", (object?)partner.PartAddress ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartCity", (object?)partner.PartCity ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartState", (object?)partner.PartState ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartZipcode", (object?)partner.PartZipcode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartLicenseNumber", (object?)partner.PartLicenseNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartLicenseExpiryDate", (object?)partner.PartLicenseExpiryDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartIDNumber", (object?)partner.PartIDNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartVehicleType", (object?)partner.PartVehicleType ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartVehicleNumber", (object?)partner.PartVehicleNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ModifiedBy", (object?)partner.ModifiedBy ?? "System");

                        await con.OpenAsync();
                        int rows = await cmd.ExecuteNonQueryAsync();

                        return rows > 0 ? "No record found for the given PartnerId." : "Partner details updated successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }



        public async Task<Partner?> GetPartnerDetailsByID(int partnerId)
        {
            Partner? partner = null;

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetPartnerDetailsByPartnerId";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@PartnerId";
                    param.Value = partnerId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            partner = new Partner
                            {
                                PartnerId = reader.GetInt32(reader.GetOrdinal("PartnerId")),
                                PartnerCode = reader["PartnerCode"]?.ToString(),
                                PartnerName = reader["PartnerName"]?.ToString(),
                                PartGender = reader["PartGender"]?.ToString(),
                                PartMobileNumber = reader["PartMobileNumber"]?.ToString(),
                                PartEmail = reader["PartEmail"]?.ToString(),
                                PartAddress = reader["PartAddress"]?.ToString(),
                                PartCity = reader["PartCity"]?.ToString(),
                                PartState = reader["PartState"]?.ToString(),
                                PartZipcode = reader["PartZipcode"]?.ToString(),
                                PartLicenseNumber = reader["PartLicenseNumber"]?.ToString(),
                                PartLicenseExpiryDate = reader["PartLicenseExpiryDate"] as DateTime?,
                                PartIDNumber = reader["PartIDNumber"]?.ToString(),
                                PartVehicleType = reader["PartVehicleType"]?.ToString(),
                                PartVehicleNumber = reader["PartVehicleNumber"]?.ToString(),
                                Part_UserId = reader["Part_UserId"]?.ToString(),
                                Inactive = Convert.ToChar(reader["Inactive"]),
                                CreatedBy = reader["CreatedBy"]?.ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                ModifiedBy = reader["ModifiedBy"]?.ToString(),
                                ModifiedDate = reader["ModifiedDate"] as DateTime?
                            };
                        }
                    }
                }
            }

            return partner;
        }




    }
}