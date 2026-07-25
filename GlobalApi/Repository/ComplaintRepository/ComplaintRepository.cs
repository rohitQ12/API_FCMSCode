//namespace GlobalApi.Repository.ComplaintRepository
//{
//    public class ComplaintRepository
//    {
//    }
//}



using Dapper;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.ComplaintIRepository;
using GlobalApi.IRepository.StatesAndCitiesIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.ComplaintModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace GlobalApi.Repository.ComplaintRepository
{
    public class ComplaintRepository : ComplaintIRepository
    {
        SqlConnection con;


        private ADO_Configrations ado_Configurations = new ADO_Configrations();





        //Old Working Code For Insert
        //public async Task<string> InsertComplaint(ComplaintInsertModel model)
        //{
        //    con = ado_Configurations.connection();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "InsertComplaint";

        //    // Personal
        //    cmd.Parameters.AddWithValue("@CP_Name", model.CP_Name);
        //    cmd.Parameters.AddWithValue("@CP_FatherName", model.CP_FatherName ?? "");
        //    cmd.Parameters.AddWithValue("@CP_DOB", model.CP_DOB);
        //    cmd.Parameters.AddWithValue("@CP_Gender", model.CP_Gender);
        //    cmd.Parameters.AddWithValue("@CP_Mobile", model.CP_Mobile);
        //    cmd.Parameters.AddWithValue("@CP_AltMobile", model.CP_AltMobile ?? "");
        //    cmd.Parameters.AddWithValue("@CP_Email", model.CP_Email ?? "");
        //    cmd.Parameters.AddWithValue("@CP_CatId", model.CP_CatId);
        //    cmd.Parameters.AddWithValue("@CP_SubCatId", model.CP_SubCatId);

        //    // Address
        //    cmd.Parameters.AddWithValue("@Addr_Full", model.Addr_Full);
        //    cmd.Parameters.AddWithValue("@Addr_City", model.Addr_City);
        //    cmd.Parameters.AddWithValue("@Addr_State", model.Addr_State);
        //    cmd.Parameters.AddWithValue("@Addr_Pin", model.Addr_Pin);

        //    // Details
        //    cmd.Parameters.AddWithValue("@CP_PrevRefNo", model.CP_PrevRefNo ?? "");
        //    cmd.Parameters.AddWithValue("@Det_Subject", model.Det_Subject);
        //    cmd.Parameters.AddWithValue("@Det_Desc", model.Det_Desc);
        //    cmd.Parameters.AddWithValue("@Det_IncDate", model.Det_IncDate);



        //    // Output Parameter
        //    SqlParameter outputParam = new SqlParameter("@CP_RefNo", SqlDbType.NVarChar, 50);
        //    outputParam.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(outputParam);

        //    await con.OpenAsync();
        //    await cmd.ExecuteNonQueryAsync();
        //    con.Close();

        //    return outputParam.Value?.ToString() ?? "";
        //}



        //New Insertcode Add Docuemnt
        //public async Task<string> InsertComplaint(ComplaintInsertModel model)
        //{
        //    con = ado_Configurations.connection();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "InsertComplaint";

        //    // Personal
        //    cmd.Parameters.AddWithValue("@CP_Name", model.CP_Name);
        //    cmd.Parameters.AddWithValue("@CP_FatherName", model.CP_FatherName ?? "");
        //    cmd.Parameters.AddWithValue("@CP_DOB", model.CP_DOB);
        //    cmd.Parameters.AddWithValue("@CP_Gender", model.CP_Gender);
        //    cmd.Parameters.AddWithValue("@CP_Mobile", model.CP_Mobile);
        //    cmd.Parameters.AddWithValue("@CP_AltMobile", model.CP_AltMobile ?? "");
        //    cmd.Parameters.AddWithValue("@CP_Email", model.CP_Email ?? "");
        //    cmd.Parameters.AddWithValue("@CP_CatId", model.CP_CatId);
        //    cmd.Parameters.AddWithValue("@CP_SubCatId", model.CP_SubCatId);

        //    // Address
        //    cmd.Parameters.AddWithValue("@Addr_Full", model.Addr_Full);
        //    cmd.Parameters.AddWithValue("@Addr_City", model.Addr_City);
        //    cmd.Parameters.AddWithValue("@Addr_State", model.Addr_State);
        //    cmd.Parameters.AddWithValue("@Addr_Pin", model.Addr_Pin);

        //    // Details
        //    cmd.Parameters.AddWithValue("@CP_PrevRefNo", model.CP_PrevRefNo ?? "");
        //    cmd.Parameters.AddWithValue("@Det_Subject", model.Det_Subject);
        //    cmd.Parameters.AddWithValue("@Det_Desc", model.Det_Desc);
        //    cmd.Parameters.AddWithValue("@Det_IncDate", model.Det_IncDate);

        //    // ✅ Output Parameter (MUST BE BEFORE EXECUTION)
        //    SqlParameter outputParam = new SqlParameter("@CP_RefNo", SqlDbType.NVarChar, 50);
        //    outputParam.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(outputParam);

        //    await con.OpenAsync();
        //    await cmd.ExecuteNonQueryAsync();
        //    con.Close();

        //    // ✅ NOW get refNo AFTER execution
        //    string refNo = outputParam.Value?.ToString() ?? "";

        //    // ✅ FILE SAVE START (NOW CORRECT POSITION)
        //    if (!string.IsNullOrEmpty(refNo))
        //    {
        //        string identityPath = null;
        //        string supportPath = null;
        //        decimal identitySize = 0;
        //        decimal supportSize = 0;

        //        if (!string.IsNullOrEmpty(model.IdentityDoc_Base64))
        //        {
        //            identityPath = await SaveFile(model.IdentityDoc_Base64, model.IdentityDoc_FileName);
        //            identitySize = GetFileSizeInMB(identityPath);
        //        }

        //        if (!string.IsNullOrEmpty(model.SupportDoc_Base64))
        //        {
        //            supportPath = await SaveFile(model.SupportDoc_Base64, model.SupportDoc_FileName);
        //            supportSize = GetFileSizeInMB(supportPath);
        //        }

        //        using (var connection = ado_Configurations.connection())
        //        {
        //            await connection.OpenAsync();

        //            SqlCommand docCmd = new SqlCommand(@"
        //        INSERT INTO ComplaintDocuments
        //        (CP_RefNo_fk,
        //         IdentityDoc_Path, IdentityDoc_Type, IdentityDoc_Name, IdentityDoc_SizeMB,
        //         SupportDoc_Path, SupportDoc_Type, SupportDoc_Name, SupportDoc_SizeMB)
        //        VALUES
        //        (@RefNo,
        //         @IPath, @IType, @IName, @ISize,
        //         @SPath, @SType, @SName, @SSize)", connection);

        //            docCmd.Parameters.AddWithValue("@RefNo", refNo);

        //            docCmd.Parameters.AddWithValue("@IPath", (object?)identityPath ?? DBNull.Value);
        //            docCmd.Parameters.AddWithValue("@IType", model.IdentityDoc_Type ?? "");
        //            docCmd.Parameters.AddWithValue("@IName", model.IdentityDoc_FileName ?? "");
        //            docCmd.Parameters.AddWithValue("@ISize", identitySize);

        //            docCmd.Parameters.AddWithValue("@SPath", (object?)supportPath ?? DBNull.Value);
        //            docCmd.Parameters.AddWithValue("@SType", model.SupportDoc_Type ?? "");
        //            docCmd.Parameters.AddWithValue("@SName", model.SupportDoc_FileName ?? "");
        //            docCmd.Parameters.AddWithValue("@SSize", supportSize);

        //            await docCmd.ExecuteNonQueryAsync();

        //            connection.Close();
        //        }
        //    }
        //    // ✅ FILE SAVE END

        //    return refNo;
        //}


        //public async Task<string> InsertComplaint(ComplaintInsertModel model)
        //{
        //    try
        //    {
        //        // ✅ STEP 1: Check if Mobile or Email already exists in AspNetUsers
        //        using (GlobalContext globalContext = new GlobalContext())
        //        {
        //            var existingMobile = await globalContext.Users
        //                .Where(x => x.UserName == model.CP_Mobile || x.PhoneNumber == model.CP_Mobile)
        //                .FirstOrDefaultAsync();

        //            if (existingMobile != null)
        //                return "MobileNumber Already Exists";

        //            var existingEmail = await globalContext.Users
        //                .Where(x => x.Email == model.CP_Email)
        //                .FirstOrDefaultAsync();

        //            if (existingEmail != null)
        //                return "Email Already Exists";

        //            // ✅ STEP 2: Create AspNetUser entry
        //            AuthUser objAuthUser = new AuthUser()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                UserName = model.CP_Mobile,
        //                NormalizedUserName =model.CP_Mobile,
        //                PhoneNumber = model.CP_Mobile,
        //                Email = model.CP_Email ?? "",
        //                NormalizedEmail = model.CP_Email ?? "",
        //                FirstName = model.CP_Name,
        //                DOB = model.CP_DOB,
        //                Gender = model.CP_Gender,
        //                Role_Id_FK = "40ea3dcb-e728-4e1b-a42f-934977114b1a", // Citizen Role
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                IsEnabled = true,
        //                Inactive = "N",
        //                UserId = 0,
        //                PhoneNumberConfirmed = false
        //            };

        //            await globalContext.Users.AddAsync(objAuthUser);
        //            await globalContext.SaveChangesAsync();
        //        }

        //        // ✅ STEP 3: Insert Complaint via Stored Procedure → get RefNo
        //        string refNo = "";

        //        using (SqlConnection con = ado_Configurations.connection())
        //        {
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = con;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "InsertComplaint";

        //            // Personal
        //            cmd.Parameters.AddWithValue("@CP_Name", model.CP_Name);
        //            cmd.Parameters.AddWithValue("@CP_FatherName", model.CP_FatherName ?? "");
        //            cmd.Parameters.AddWithValue("@CP_DOB", model.CP_DOB);
        //            cmd.Parameters.AddWithValue("@CP_Gender", model.CP_Gender);
        //            cmd.Parameters.AddWithValue("@CP_Mobile", model.CP_Mobile);
        //            cmd.Parameters.AddWithValue("@CP_AltMobile", model.CP_AltMobile ?? "");
        //            cmd.Parameters.AddWithValue("@CP_Email", model.CP_Email ?? "");
        //            cmd.Parameters.AddWithValue("@CP_CatId", model.CP_CatId);
        //            cmd.Parameters.AddWithValue("@CP_SubCatId", model.CP_SubCatId);

        //            // Address
        //            cmd.Parameters.AddWithValue("@Addr_Full", model.Addr_Full);
        //            cmd.Parameters.AddWithValue("@Addr_City", model.Addr_City);
        //            cmd.Parameters.AddWithValue("@Addr_State", model.Addr_State);
        //            cmd.Parameters.AddWithValue("@Addr_Pin", model.Addr_Pin);

        //            // Details
        //            cmd.Parameters.AddWithValue("@CP_PrevRefNo", model.CP_PrevRefNo ?? "");
        //            cmd.Parameters.AddWithValue("@Det_Subject", model.Det_Subject);
        //            cmd.Parameters.AddWithValue("@Det_Desc", model.Det_Desc);
        //            cmd.Parameters.AddWithValue("@Det_IncDate", model.Det_IncDate);

        //            // ✅ Output Parameter
        //            SqlParameter outputParam = new SqlParameter("@CP_RefNo", SqlDbType.NVarChar, 50);
        //            outputParam.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(outputParam);

        //            await con.OpenAsync();
        //            await cmd.ExecuteNonQueryAsync();

        //            refNo = outputParam.Value?.ToString() ?? "";
        //        }

        //        // ✅ STEP 4: Save Documents (only if refNo was returned)
        //        if (!string.IsNullOrEmpty(refNo))
        //        {
        //            string identityPath = null;
        //            string supportPath = null;
        //            decimal identitySize = 0;
        //            decimal supportSize = 0;

        //            if (!string.IsNullOrEmpty(model.IdentityDoc_Base64))
        //            {
        //                identityPath = await SaveFile(model.IdentityDoc_Base64, model.IdentityDoc_FileName);
        //                identitySize = GetFileSizeInMB(identityPath);
        //            }

        //            if (!string.IsNullOrEmpty(model.SupportDoc_Base64))
        //            {
        //                supportPath = await SaveFile(model.SupportDoc_Base64, model.SupportDoc_FileName);
        //                supportSize = GetFileSizeInMB(supportPath);
        //            }

        //            using (SqlConnection connection = ado_Configurations.connection())
        //            {
        //                await connection.OpenAsync();

        //                SqlCommand docCmd = new SqlCommand(@"
        //            INSERT INTO ComplaintDocuments
        //            (
        //                CP_RefNo_fk,
        //                IdentityDoc_Path, IdentityDoc_Type, IdentityDoc_Name, IdentityDoc_SizeMB,
        //                SupportDoc_Path,  SupportDoc_Type,  SupportDoc_Name,  SupportDoc_SizeMB
        //            )
        //            VALUES
        //            (
        //                @RefNo,
        //                @IPath, @IType, @IName, @ISize,
        //                @SPath, @SType, @SName, @SSize
        //            )", connection);

        //                docCmd.Parameters.AddWithValue("@RefNo", refNo);

        //                docCmd.Parameters.AddWithValue("@IPath", (object?)identityPath ?? DBNull.Value);
        //                docCmd.Parameters.AddWithValue("@IType", model.IdentityDoc_Type ?? "");
        //                docCmd.Parameters.AddWithValue("@IName", model.IdentityDoc_FileName ?? "");
        //                docCmd.Parameters.AddWithValue("@ISize", identitySize);

        //                docCmd.Parameters.AddWithValue("@SPath", (object?)supportPath ?? DBNull.Value);
        //                docCmd.Parameters.AddWithValue("@SType", model.SupportDoc_Type ?? "");
        //                docCmd.Parameters.AddWithValue("@SName", model.SupportDoc_FileName ?? "");
        //                docCmd.Parameters.AddWithValue("@SSize", supportSize);

        //                await docCmd.ExecuteNonQueryAsync();
        //            }
        //        }

        //        return refNo;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception($"InsertComplaint failed: {e.Message}", e);
        //    }
        //}


        // Customer, User
        public async Task<string> InsertComplaint(ComplaintInsertModel model)
        {
            try
            {
                // ✅ STEP 1: Check if Mobile or Email already exists in AspNetUsers
                using (GlobalContext globalContext = new GlobalContext())
                {
                    var existingMobile = await globalContext.Users
                        .Where(x => x.UserName == model.CP_Mobile || x.PhoneNumber == model.CP_Mobile)
                        .FirstOrDefaultAsync();

                    if (existingMobile != null)
                        return "MobileNumber Already Exists";

                    var existingEmail = await globalContext.Users
                        .Where(x => x.Email == model.CP_Email)
                        .FirstOrDefaultAsync();

                    if (existingEmail != null)
                        return "Email Already Exists";

                    // ✅ STEP 2: Hash the password before saving
                    var passwordHasher = new PasswordHasher<AuthUser>();

                    AuthUser objAuthUser = new AuthUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.CP_Mobile,
                        NormalizedUserName = model.CP_Mobile.ToUpper(),  // ✅ should be uppercase
                        PhoneNumber = model.CP_Mobile,
                        Email = model.CP_Email ?? "",
                        NormalizedEmail = model.CP_Email?.ToUpper() ?? "",  // ✅ should be uppercase
                        FirstName = model.CP_Name,
                        DOB = model.CP_DOB,
                        Gender = model.CP_Gender,
                        Role_Id_FK = "40ea3dcb-e728-4e1b-a42f-934977114b1a",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsEnabled = true,
                        Inactive = "N",
                        UserId = 0,
                        PhoneNumberConfirmed = false
                    };

                    // ✅ STEP 2a: Hash and set password AFTER object creation
                    objAuthUser.PasswordHash = passwordHasher.HashPassword(objAuthUser, model.CP_Password);

                    await globalContext.Users.AddAsync(objAuthUser);
                    await globalContext.SaveChangesAsync();
                }

                // ✅ STEP 3: Insert Complaint via Stored Procedure → get RefNo
                string refNo = "";

                using (SqlConnection con = ado_Configurations.connection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsertComplaint";

                    // Personal
                    cmd.Parameters.AddWithValue("@CP_Name", model.CP_Name);
                    cmd.Parameters.AddWithValue("@CP_FatherName", model.CP_FatherName ?? "");
                    cmd.Parameters.AddWithValue("@CP_DOB", model.CP_DOB);
                    cmd.Parameters.AddWithValue("@CP_Gender", model.CP_Gender);
                    cmd.Parameters.AddWithValue("@CP_Mobile", model.CP_Mobile);
                    cmd.Parameters.AddWithValue("@CP_AltMobile", model.CP_AltMobile ?? "");
                    cmd.Parameters.AddWithValue("@CP_Email", model.CP_Email ?? "");
                    cmd.Parameters.AddWithValue("@CP_CatId", model.CP_CatId);
                    cmd.Parameters.AddWithValue("@CP_SubCatId", model.CP_SubCatId);

                    // Address
                    cmd.Parameters.AddWithValue("@Addr_Full", model.Addr_Full);
                    cmd.Parameters.AddWithValue("@Addr_City", model.Addr_City);
                    cmd.Parameters.AddWithValue("@Addr_State", model.Addr_State);
                    cmd.Parameters.AddWithValue("@Addr_Pin", model.Addr_Pin);

                    // Details
                    cmd.Parameters.AddWithValue("@CP_PrevRefNo", model.CP_PrevRefNo ?? "");
                    cmd.Parameters.AddWithValue("@Det_Subject", model.Det_Subject);
                    cmd.Parameters.AddWithValue("@Det_Desc", model.Det_Desc);
                    cmd.Parameters.AddWithValue("@Det_IncDate", model.Det_IncDate);

                    // ✅ Output Parameter
                    SqlParameter outputParam = new SqlParameter("@CP_RefNo", SqlDbType.NVarChar, 50);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    refNo = outputParam.Value?.ToString() ?? "";
                }

                // ✅ STEP 4: Save Documents
                if (!string.IsNullOrEmpty(refNo))
                {
                    string identityPath = null;
                    string supportPath = null;
                    decimal identitySize = 0;
                    decimal supportSize = 0;

                    if (!string.IsNullOrEmpty(model.IdentityDoc_Base64))
                    {
                        identityPath = await SaveFile(model.IdentityDoc_Base64, model.IdentityDoc_FileName);
                        identitySize = GetFileSizeInMB(identityPath);
                    }

                    if (!string.IsNullOrEmpty(model.SupportDoc_Base64))
                    {
                        supportPath = await SaveFile(model.SupportDoc_Base64, model.SupportDoc_FileName);
                        supportSize = GetFileSizeInMB(supportPath);
                    }

                    using (SqlConnection connection = ado_Configurations.connection())
                    {
                        await connection.OpenAsync();

                        SqlCommand docCmd = new SqlCommand(@"
                    INSERT INTO ComplaintDocuments
                    (
                        CP_RefNo_fk,
                        IdentityDoc_Path, IdentityDoc_Type, IdentityDoc_Name, IdentityDoc_SizeMB,
                        SupportDoc_Path,  SupportDoc_Type,  SupportDoc_Name,  SupportDoc_SizeMB
                    )
                    VALUES
                    (
                        @RefNo,
                        @IPath, @IType, @IName, @ISize,
                        @SPath, @SType, @SName, @SSize
                    )", connection);

                        docCmd.Parameters.AddWithValue("@RefNo", refNo);

                        docCmd.Parameters.AddWithValue("@IPath", (object?)identityPath ?? DBNull.Value);
                        docCmd.Parameters.AddWithValue("@IType", model.IdentityDoc_Type ?? "");
                        docCmd.Parameters.AddWithValue("@IName", model.IdentityDoc_FileName ?? "");
                        docCmd.Parameters.AddWithValue("@ISize", identitySize);

                        docCmd.Parameters.AddWithValue("@SPath", (object?)supportPath ?? DBNull.Value);
                        docCmd.Parameters.AddWithValue("@SType", model.SupportDoc_Type ?? "");
                        docCmd.Parameters.AddWithValue("@SName", model.SupportDoc_FileName ?? "");
                        docCmd.Parameters.AddWithValue("@SSize", supportSize);

                        await docCmd.ExecuteNonQueryAsync();
                    }
                }

                return refNo;
            }
            catch (Exception e)
            {
                throw new Exception($"InsertComplaint failed: {e.Message}", e);
            }
        }


        private async Task<string> SaveFile(string base64, string fileName)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Complaint");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string ext = Path.GetExtension(fileName ?? "") ?? ".jpg";
            string newFile = $"{Guid.NewGuid()}{ext}";
            string fullPath = Path.Combine(folder, newFile);

            if (base64.Contains(",")) base64 = base64.Split(',')[1];

            byte[] bytes = Convert.FromBase64String(base64);
            await File.WriteAllBytesAsync(fullPath, bytes);

            return $"/Complaint/{newFile}";
        }

        private decimal GetFileSizeInMB(string path)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));
            FileInfo fi = new FileInfo(fullPath);

            return fi.Exists ? Math.Round((decimal)fi.Length / (1024 * 1024), 2) : 0;
        }

        //Admin
        public async Task<List<ComplaintDetailsModels>> GetAllComplaintDetails()
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAllComplaintDetails";

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<ComplaintDetailsModels> complaintList = new List<ComplaintDetailsModels>();

            complaintList = (from DataRow drr in dt.Rows
                             select new ComplaintDetailsModels()
                             {
                                 CP_RefNo = (string)drr["CP_RefNo"],
                                 CP_Name = (string)drr["CP_Name"],
                                 CP_CatId = (int)drr["CP_CatId"],
                                 CategoryName = (string)drr["CategoryName"],
                                 CP_Priority = (string)drr["CP_Priority"],
                                 CP_Status = (string)drr["CP_Status"],
                                 CP_CreatedAt = (DateTime)drr["CP_CreatedAt"],
                                 Det_IncDate = (DateTime)drr["Det_IncDate"]
                             }).ToList();

            return complaintList;
        }


        //Admin
        public async Task<ComplaintDetailsModel> GetComplaintDetailsById(string CP_RefNo)
        {
            con = ado_Configurations.connection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetComplaintDetailsById";
            cmd.Parameters.AddWithValue("@CP_RefNo", CP_RefNo);

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();
            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];

            return new ComplaintDetailsModel()
            {
                CP_RefNo = dr["CP_RefNo"].ToString(),
                CP_Name = dr["CP_Name"].ToString(),
                CP_FatherName = dr["CP_FatherName"].ToString(),
                CP_DOB = dr["CP_DOB"] == DBNull.Value ? null : Convert.ToDateTime(dr["CP_DOB"]),
                CP_Gender = dr["CP_Gender"].ToString(),
                CP_Mobile = dr["CP_Mobile"].ToString(),
                CP_AltMobile = dr["CP_AltMobile"].ToString(),
                CP_Email = dr["CP_Email"].ToString(),

                CP_CatId = dr["CP_CatId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CP_CatId"]),
                Category_Name = dr["Category_Name"].ToString(),
                CP_SubCatId = dr["CP_SubCatId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CP_SubCatId"]),
                SubCategoriesName = dr["SubCategoriesName"].ToString(),

                CP_Status = dr["CP_Status"].ToString(),
                CP_Priority = dr["CP_Priority"].ToString(),
                CP_CreatedAt = dr["CP_CreatedAt"] == DBNull.Value ? null : Convert.ToDateTime(dr["CP_CreatedAt"]),
                CP_UpdatedAt = dr["CP_UpdatedAt"] == DBNull.Value ? null : Convert.ToDateTime(dr["CP_UpdatedAt"]),

                CP_PrevRefNo = dr["CP_PrevRefNo"].ToString(),
                Det_Subject = dr["Det_Subject"].ToString(),
                Det_Desc = dr["Det_Desc"].ToString(),
                Det_IncDate = dr["Det_IncDate"] == DBNull.Value ? null : Convert.ToDateTime(dr["Det_IncDate"]),

                Addr_Full = dr["Addr_Full"].ToString(),
                Addr_City = dr["Addr_City"].ToString(),
                Addr_State = dr["Addr_State"].ToString(),
                Addr_Pin = dr["Addr_Pin"].ToString(),

                IdentityDoc_Path = dr["IdentityDoc_Path"].ToString(),
                IdentityDoc_Type = dr["IdentityDoc_Type"].ToString(),
                IdentityDoc_Name = dr["IdentityDoc_Name"].ToString(),
                SupportDoc_Path = dr["SupportDoc_Path"].ToString(),
                SupportDoc_Type = dr["SupportDoc_Type"].ToString(),
                SupportDoc_Name = dr["SupportDoc_Name"].ToString(),
            };
        }



        //Admin
        public async Task<UserRoleDto> CheckUserTypeAdmin(string userName)
        {
            UserRoleDto result = null;

            SqlConnection con = ado_Configurations.connection();

            try
            {
                SqlCommand cmd = new SqlCommand("FindRoleNameFromUserName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                await con.OpenAsync();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    result = new UserRoleDto
                    {
                        UserName = dt.Rows[0]["UserName"].ToString(),
                        RoleName = dt.Rows[0]["RoleName"].ToString(),
                        IsAdmin = Convert.ToBoolean(dt.Rows[0]["IsAdmin"])
                    };
                }
            }
            finally
            {
                con.Close();
            }

            return result;
        }



        // Customer, User
        public async Task<UserRoleDto> CheckUserTypeCustomer(string userName)
        {
            UserRoleDto result = null;

            SqlConnection con = ado_Configurations.connection();

            try
            {
                SqlCommand cmd = new SqlCommand("FindRoleNameFromUserName_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                await con.OpenAsync();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    result = new UserRoleDto
                    {
                        UserName = dt.Rows[0]["UserName"].ToString(),
                        RoleName = dt.Rows[0]["RoleName"].ToString(),
                        IsAdmin = Convert.ToBoolean(dt.Rows[0]["IsAdmin"])
                    };
                }
            }
            finally
            {
                con.Close();
            }

            return result;
        }


        // Customer, User
        public async Task<List<ComplaintDetailsModels>> GetComplaintDetailsBy(string UserName)
        {
            con = ado_Configurations.connection();
                    
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetComplaintDetailsByUsername";
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<ComplaintDetailsModels> complaintList = new List<ComplaintDetailsModels>();

            complaintList = (from DataRow drr in dt.Rows
                             select new ComplaintDetailsModels()
                             {
                                 CP_RefNo = drr["CP_RefNo"]?.ToString(),
                                 CP_Name = drr["CP_Name"]?.ToString(),
                                 CP_CatId = drr["CP_CatId"] != DBNull.Value ? Convert.ToInt32(drr["CP_CatId"]) : 0,
                                 CategoryName = drr["CategoryName"]?.ToString(),
                                 CP_Priority = drr["CP_Priority"]?.ToString(),
                                 CP_Status = drr["CP_Status"]?.ToString(),
                                 CP_CreatedAt = drr["CP_CreatedAt"] != DBNull.Value ? Convert.ToDateTime(drr["CP_CreatedAt"]) : DateTime.MinValue,
                                 Det_IncDate = drr["Det_IncDate"] != DBNull.Value ? Convert.ToDateTime(drr["Det_IncDate"]) : DateTime.MinValue,
                                 Remarks = drr["Remarks"]?.ToString()
                             }).ToList();

            return complaintList;
        }

            public async Task<List<StatusModels>> GetAllStatus()
            {
                con = ado_Configurations.connection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllStatus";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();

                await con.OpenAsync();
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                con.Close();

                List<StatusModels> StatusList = new List<StatusModels>();

                StatusList = (from DataRow drr in dt.Rows
                              select new StatusModels()
                              {
                                  Status_id = (int)drr["Status_id"],
                                  Status_Name = (string)drr["Status_Name"],
 
                              }).ToList();

                return StatusList;
            }

        public async Task<List<PriorityModels>> GetAllPriority()
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAllPriority";

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<PriorityModels> PriorityList = new List<PriorityModels>();

            PriorityList = (from DataRow drr in dt.Rows
                          select new PriorityModels()
                          {
                              Priority_id = (int)drr["Priority_id"],
                              Priority_Name = (string)drr["Priority_Name"],

                          }).ToList();

            return PriorityList;
        }

        //public async Task<int> UpdateComplaintStatusPriority(UpdateComplaintStatusPriorityModel model)
        //{
        //    con = ado_Configurations.connection();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "UpdateComplaintStatusPriority";

        //    cmd.Parameters.AddWithValue("@CP_RefNo", model.CP_RefNo);
        //    cmd.Parameters.AddWithValue("@Status_id", model.Status_id);
        //    cmd.Parameters.AddWithValue("@Priority_id", model.Priority_id);

        //    await con.OpenAsync();
        //    int affectedRows = await cmd.ExecuteNonQueryAsync();
        //    con.Close();

        //    return affectedRows;
        //}

        public async Task<int> UpdateComplaintStatusPriority(UpdateComplaintStatusPriorityModel model)
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateComplaintStatusPriority";

            cmd.Parameters.AddWithValue("@CP_RefNo", model.CP_RefNo);
            cmd.Parameters.AddWithValue("@Status_id", model.Status_id);
            cmd.Parameters.AddWithValue("@Priority_id", model.Priority_id);
            cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
            cmd.Parameters.AddWithValue("Remarks", model.Remarks);

            await con.OpenAsync();

            int affectedRows = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            con.Close();

            return affectedRows;
        }

        public async Task<List<DepartmentModels>> GetAllDepartment()
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAllDepartment";

            DataTable dt = new DataTable();

            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();

            adp.SelectCommand = cmd;
            adp.Fill(dt);

            con.Close();


            List<DepartmentModels> DepartmentList = new List<DepartmentModels>();


            DepartmentList = (from DataRow drr in dt.Rows
                              select new DepartmentModels()
                              {
                                  DepartmentID = Convert.ToInt32(drr["DepartmentID"]),

                                  DepartmentName = drr["DepartmentName"].ToString(),

                                  DepartmentCode = drr["DepartmentCode"].ToString(),

                                  ParentDepartmentID_Fk = drr["ParentDepartmentID_Fk"] == DBNull.Value
                                      ? null
                                      : Convert.ToInt32(drr["ParentDepartmentID_Fk"]),

                                  JurisdictionType = drr["JurisdictionType"].ToString(),

                                  ContactEmail = drr["ContactEmail"] == DBNull.Value
                                      ? null
                                      : drr["ContactEmail"].ToString(),

                                  ContactPhone = drr["ContactPhone"] == DBNull.Value
                                      ? null
                                      : drr["ContactPhone"].ToString(),

                                  IsActive = Convert.ToBoolean(drr["IsActive"]),

                                  CreatedDate = Convert.ToDateTime(drr["CreatedDate"])

                              }).ToList();


            return DepartmentList;
        }

        public async Task<List<UserDetailsModel>> GetAllUserDetails()
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAllUserDetails";

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();

            adp.SelectCommand = cmd;
            adp.Fill(dt);

            con.Close();

            List<UserDetailsModel> UserList = new List<UserDetailsModel>();

            UserList = (from DataRow drr in dt.Rows
                        select new UserDetailsModel()
                        {
                            //Id = drr["Id"].ToString(),
                            Name = drr["Name"].ToString(),
                            Email = drr["Email"].ToString(),
                            Mobile = drr["Mobile"].ToString(),
                            Role = drr["Role"].ToString(),
                            Department = drr["Department"].ToString(),
                            Status = drr["Status"].ToString()
                        }).ToList();

            return UserList;
        }


        public async Task<string> AddDepartment(DepartmentModel model)
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddDepartment";

            cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
            cmd.Parameters.AddWithValue("@DepartmentCode", model.DepartmentCode);

            //if (model.ParentDepartmentID_Fk == null)
            //    cmd.Parameters.AddWithValue("@ParentDepartmentID_Fk", DBNull.Value);
            //else
            //    cmd.Parameters.AddWithValue("@ParentDepartmentID_Fk", model.ParentDepartmentID_Fk);

            //cmd.Parameters.AddWithValue("@JurisdictionType", model.JurisdictionType);

            if (string.IsNullOrEmpty(model.ContactEmail))
                cmd.Parameters.AddWithValue("@ContactEmail", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ContactEmail", model.ContactEmail);

            if (string.IsNullOrEmpty(model.ContactPhone))
                cmd.Parameters.AddWithValue("@ContactPhone", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ContactPhone", model.ContactPhone);

            cmd.Parameters.AddWithValue("@IsActive", model.IsActive);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            con.Close();

            return "Department Added Successfully";
        }


        public async Task<string> InsertDepartmentUser(DepartmentUserModel model)
        {
            try
            {
                string userId = "";

                using (GlobalContext globalContext = new GlobalContext())
                {

                    // Mobile Check
                    var existingMobile = await globalContext.Users
                        .Where(x => x.UserName == model.Mobile ||
                                    x.PhoneNumber == model.Mobile)
                        .FirstOrDefaultAsync();

                    if (existingMobile != null)
                        return "MobileNumber Already Exists";


                    // Email Check
                    var existingEmail = await globalContext.Users
                        .Where(x => x.Email == model.Email)
                        .FirstOrDefaultAsync();

                    if (existingEmail != null)
                        return "Email Already Exists";


                    // Password Hash
                    var passwordHasher = new PasswordHasher<AuthUser>();

                    AuthUser objAuthUser = new AuthUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.Mobile,
                        NormalizedUserName = model.Mobile.ToUpper(),
                        PhoneNumber = model.Mobile,

                        Email = model.Email,
                        NormalizedEmail = model.Email.ToUpper(),

                        FirstName = model.FullName,

                        Role_Id_FK = model.RoleId,

                        SecurityStamp = Guid.NewGuid().ToString(),

                        IsEnabled = model.IsActive,

                        Inactive = "N",

                        UserId = 0,

                        PhoneNumberConfirmed = false
                    };

                    objAuthUser.PasswordHash =
                        passwordHasher.HashPassword(objAuthUser,
                                                    model.Password);

                    await globalContext.Users.AddAsync(objAuthUser);

                    await globalContext.SaveChangesAsync();

                    userId = objAuthUser.Id;
                }


                // Insert into DepartmentUser

                using (SqlConnection con = ado_Configurations.connection())
                {

                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "InsertDepartmentUser";

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.Parameters.AddWithValue("@DepartmentID",
                        (object?)model.DepartmentID ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@RoleId",
                        model.RoleId);

                    cmd.Parameters.AddWithValue("@DPMobile",
                        model.Mobile);

                    cmd.Parameters.AddWithValue("@DPEmail",
                        model.Email);

                    cmd.Parameters.AddWithValue("@IsActive",
                        model.IsActive);

                    await con.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();

                    con.Close();

                }

                return "Success";
            }
            catch (Exception ex)
            {
                throw new Exception("InsertDepartmentUser failed : " + ex.Message);
            }
        }



        public async Task<List<DepartmentDetails>> GetDepartmentDetailsBy(string UserName)
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetDepartmentDetailsByUsername";
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;

            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<DepartmentDetails> departmentList = (from DataRow drr in dt.Rows
                                                      select new DepartmentDetails()
                                                      {
                                                          DepartmentID = drr["DepartmentID"] != DBNull.Value
                                                              ? Convert.ToInt32(drr["DepartmentID"])
                                                              : (int?)null,

                                                          DepartmentName = drr["DepartmentName"]?.ToString(),

                                                          DepartmentCode = drr["DepartmentCode"]?.ToString(),

                                                          ParentDepartmentID_Fk = drr["ParentDepartmentID_Fk"] != DBNull.Value
                                                              ? Convert.ToInt32(drr["ParentDepartmentID_Fk"])
                                                              : (int?)null,

                                                          ParentDepartmentName = drr["ParentDepartmentName"]?.ToString(),

                                                          //JurisdictionType = drr["JurisdictionType"]?.ToString(),

                                                          ContactEmail = drr["ContactEmail"]?.ToString(),

                                                          ContactPhone = drr["ContactPhone"]?.ToString(),

                                                          IsActive = drr["IsActive"] != DBNull.Value
                                                              ? Convert.ToBoolean(drr["IsActive"])
                                                              : (bool?)null,

                                                          CreatedDate = drr["CreatedDate"] != DBNull.Value
                                                              ? Convert.ToDateTime(drr["CreatedDate"])
                                                              : (DateTime?)null,

                                                          DepartmentUserID = drr["DepartmentUserID"] != DBNull.Value
                                                              ? Convert.ToInt32(drr["DepartmentUserID"])
                                                              : (int?)null,

                                                          UserId = drr["UserId"]?.ToString(),

                                                          RoleId = drr["RoleId"]?.ToString(),

                                                          DPMobile = drr["DPMobile"]?.ToString(),

                                                          DPEmail = drr["DPEmail"]?.ToString(),

                                                          FirstName = drr["FirstName"]?.ToString(),

                                                          LastName = drr["LastName"]?.ToString(),

                                                          UserName = drr["UserName"]?.ToString(),

                                                          Email = drr["Email"]?.ToString()
                                                      }).ToList();

            return departmentList;
        }



        public async Task<List<ComplaintDetailsModels>> GetAllComplaintDetailsbyDepartment(string userId, string roleId,int departmentID)
        {
            con = ado_Configurations.connection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAllComplaintDetailsbyDepartment";

            cmd.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userId;
            cmd.Parameters.Add("@RoleId", SqlDbType.NVarChar).Value = roleId;
            cmd.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = departmentID.ToString();


            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();

            await con.OpenAsync();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            con.Close();

            List<ComplaintDetailsModels> complaintList = (from DataRow drr in dt.Rows
                                                          select new ComplaintDetailsModels()
                                                          {
                                                              CP_RefNo = drr["CP_RefNo"]?.ToString(),
                                                              CP_Name = drr["CP_Name"]?.ToString(),
                                                              CP_CatId = drr["CP_CatId"] != DBNull.Value ? Convert.ToInt32(drr["CP_CatId"]) : 0,
                                                              CategoryName = drr["CategoryName"]?.ToString(),
                                                              CP_Priority = drr["CP_Priority"]?.ToString(),
                                                              CP_Status = drr["CP_Status"]?.ToString(),
                                                              CP_CreatedAt = drr["CP_CreatedAt"] != DBNull.Value ? Convert.ToDateTime(drr["CP_CreatedAt"]) : DateTime.MinValue,
                                                              Det_IncDate = drr["Det_IncDate"] != DBNull.Value ? Convert.ToDateTime(drr["Det_IncDate"]) : DateTime.MinValue
                                                          }).ToList();

            return complaintList;
        }



    }
}