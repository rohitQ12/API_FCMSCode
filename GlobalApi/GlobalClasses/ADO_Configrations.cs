using Microsoft.Data.SqlClient;

namespace GlobalApi.GlobalClasses
{
    public class ADO_Configrations
    {
        private SqlConnection conn=null!;
        private readonly IConfigurationRoot configurationRoot = null!;
        public ADO_Configrations()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }
        public SqlConnection connection()
        {
            try
            {
                string constr = configurationRoot.GetConnectionString("ConnectionString");
                conn = new SqlConnection(constr); 
                return conn;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public SqlConnection connection(string connectionType)
        {
            if (connectionType == "ConnectionString")
            {
                string constr = configurationRoot.GetConnectionString("ConnectionString");
                conn = new SqlConnection(constr);
            }
            return conn;
        }


    }
}
