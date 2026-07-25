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
    public class CustomerRepository : ICustomer
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly FileUpload fileUpload;
        public CustomerRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();

        }

        public async Task<List<UserCustomer>> GetAllCustomerList()
        {
            var customers = new List<UserCustomer>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetAllCustomerList";  // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            customers.Add(new UserCustomer
                            {
                                Cust_Id = reader.GetInt32(reader.GetOrdinal("Cust_Id")),
                                Cust_Name = reader.GetString(reader.GetOrdinal("Cust_Name")),
                                Cust_Email_Id = reader["Cust_Email_Id"] as string,
                                Cust_Mobile_Number = reader["Cust_Mobile_Number"] as string,
                                Cust_Photo = reader["Cust_Photo"] as string,
                                Cust_Address = reader["Cust_Address"] as string,
                                Cust_Zip_Code = reader["Cust_Zip_Code"] as string,
                                Cust_UserId = reader["Cust_UserId"] as string,
                                Cust_status = reader["Cust_status"] as string,
                                create_by = reader["create_by"] as string,
                                created_date = reader["created_date"] as DateTime?,
                                modified_by = reader["modified_by"] as string,
                                modified_date = reader["modified_date"] as DateTime?,
                                delete_flag = reader.GetBoolean(reader.GetOrdinal("delete_flag"))
                            });
                        }
                    }
                }
            }

            return customers;
        }


        public async Task<UserCustomer?> GetCustomerByID(int custId)
        {
            UserCustomer? customer = null;

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetCustomerByCust_Id"; // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@Cust_Id";
                    param.Value = custId;
                    command.Parameters.Add(param);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            customer = new UserCustomer
                            {
                                Cust_Id = reader.GetInt32(reader.GetOrdinal("Cust_Id")),
                                Cust_Name = reader["Cust_Name"]?.ToString(),
                                Cust_Email_Id = reader["Cust_Email_Id"]?.ToString(),
                                Cust_Mobile_Number = reader["Cust_Mobile_Number"]?.ToString(),
                                Cust_Photo = reader["Cust_Photo"]?.ToString(),
                                Cust_Address = reader["Cust_Address"]?.ToString(),
                                Cust_Zip_Code = reader["Cust_Zip_Code"]?.ToString(),
                                Cust_UserId = reader["Cust_UserId"]?.ToString(),
                                Cust_status = reader["Cust_status"]?.ToString(),
                                create_by = reader["create_by"]?.ToString(),
                                created_date = reader["created_date"] as DateTime?,
                                modified_by = reader["modified_by"]?.ToString(),
                                modified_date = reader["modified_date"] as DateTime?,
                                delete_flag = Convert.ToBoolean(reader["delete_flag"])
                            };
                        }
                    }
                }
            }

            return customer;
        }



    }
}
