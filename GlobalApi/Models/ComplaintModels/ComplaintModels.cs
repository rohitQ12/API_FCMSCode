namespace GlobalApi.Models.ComplaintModels
{
    public class ComplaintModels
    {

    }

    //public class ComplaintInsertModel
    //{
    //    // Personal
    //    public string CP_Name { get; set; }
    //    public string CP_FatherName { get; set; }
    //    public DateTime CP_DOB { get; set; }
    //    public string CP_Gender { get; set; }
    //    public string CP_Mobile { get; set; }
    //    public string CP_AltMobile { get; set; }
    //    public string CP_Email { get; set; }
    //    public int CP_CatId { get; set; }
    //    public int CP_SubCatId { get; set; }

    //    // Address
    //    public string Addr_Full { get; set; }
    //    public string Addr_City { get; set; }
    //    public string Addr_State { get; set; }
    //    public string Addr_Pin { get; set; }

    //    // Details
    //    public string CP_PrevRefNo { get; set; }
    //    public string Det_Subject { get; set; }
    //    public string Det_Desc { get; set; }
    //    public DateTime Det_IncDate { get; set; }

    //    // Documents
    //    public string IdentityDoc_Path { get; set; }
    //    public string IdentityDoc_Type { get; set; }
    //    public string IdentityDoc_Name { get; set; }
    //    public string IdentityDoc_FileName { get; set; }
    //    public string IdentityDoc_Base64 { get; set; }
    //    public DateTime IdentityDoc_SizeMB { get; set; }
    //    public string SupportDoc_Path { get; set; }
    //    public string SupportDoc_Type { get; set; }
    //    public string SupportDoc_Name { get; set; }
    //    public string SupportDoc_FileName { get; set; }
    //    public string SupportDoc_Base64 { get; set; }
    //    public DateTime SupportDoc_SizeMB { get; set; }


    //}

    public class ComplaintInsertModel
    {
        // Personal
        public string CP_Name { get; set; }
        public string CP_FatherName { get; set; }
        public DateTime CP_DOB { get; set; }
        public string CP_Gender { get; set; }
        public string CP_Mobile { get; set; }
        public string CP_AltMobile { get; set; }
        public string CP_Email { get; set; }
        public int CP_CatId { get; set; }
        public int CP_SubCatId { get; set; }
        public string CP_Password { get; set; }

        // Address
        public string Addr_Full { get; set; }
        public string Addr_City { get; set; }
        public string Addr_State { get; set; }
        public string Addr_Pin { get; set; }

        // Details
        public string CP_PrevRefNo { get; set; }
        public string Det_Subject { get; set; }
        public string Det_Desc { get; set; }
        public DateTime Det_IncDate { get; set; }

        // Documents
        public string IdentityDoc_Path { get; set; }
        public string IdentityDoc_Type { get; set; }
        public string IdentityDoc_Name { get; set; }
        public string IdentityDoc_FileName { get; set; }
        public string IdentityDoc_Base64 { get; set; }
        public decimal IdentityDoc_SizeMB { get; set; }  // ✅ decimal not DateTime

        public string SupportDoc_Path { get; set; }
        public string SupportDoc_Type { get; set; }
        public string SupportDoc_Name { get; set; }
        public string SupportDoc_FileName { get; set; }
        public string SupportDoc_Base64 { get; set; }
        public decimal SupportDoc_SizeMB { get; set; }  // ✅ decimal not DateTime
    }

    public class ComplaintDetailsModels
    {
        public string CP_RefNo { get; set; }
        public string CP_Name { get; set; }
        public int CP_CatId { get; set; }
        public string CategoryName { get; set; }
        public string CP_Priority { get; set; }
        public string CP_Status { get; set; }
        public DateTime CP_CreatedAt { get; set; }
        public DateTime Det_IncDate { get; set; }
        public string Remarks { get; set; }
    }



    public class ComplaintDetailsModel
    {
        // Main
        public string? CP_RefNo { get; set; }
        public string? CP_Name { get; set; }
        public string? CP_FatherName { get; set; }
        public DateTime? CP_DOB { get; set; }
        public string? CP_Gender { get; set; }
        public string? CP_Mobile { get; set; }
        public string? CP_AltMobile { get; set; }
        public string? CP_Email { get; set; }

        // Category
        public int CP_CatId { get; set; }
        public string? Category_Name { get; set; }
        public int CP_SubCatId { get; set; }
        public string? SubCategoriesName { get; set; }

        // Status
        public string? CP_Status { get; set; }
        public string? CP_Priority { get; set; }
        public DateTime? CP_CreatedAt { get; set; }
        public DateTime? CP_UpdatedAt { get; set; }

        // Details
        public string? CP_PrevRefNo { get; set; }
        public string? Det_Subject { get; set; }
        public string? Det_Desc { get; set; }
        public DateTime? Det_IncDate { get; set; }

        // Address
        public string? Addr_Full { get; set; }
        public string? Addr_City { get; set; }
        public string? Addr_State { get; set; }
        public string? Addr_Pin { get; set; }

        // Documents
        public string? IdentityDoc_Path { get; set; }
        public string? IdentityDoc_Type { get; set; }
        public string? IdentityDoc_Name { get; set; }
        public string? SupportDoc_Path { get; set; }
        public string? SupportDoc_Type { get; set; }
        public string? SupportDoc_Name { get; set; }
    }

    public class UserRoleDto
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class CustomMsg
    {
        public string message { get; set; }
        public string message_desc { get; set; }
    }


    public class StatusModels
    {
        public int Status_id { get; set; }
        public string Status_Name { get; set; }

    }

    public class PriorityModels
    {
        public int Priority_id { get; set; }
        public string Priority_Name { get; set; }

    }

    //public class UpdateComplaintStatusPriorityModel
    //{
    //    public string CP_RefNo { get; set; }
    //    public int Status_id { get; set; }
    //    public int Priority_id { get; set; }
    //}
    public class UpdateComplaintStatusPriorityModel
    {
        public string CP_RefNo { get; set; }
        public int Status_id { get; set; }
        public int Priority_id { get; set; }
        public int DepartmentID { get; set; }
        public string Remarks { get; set; }
    }

    public class DepartmentModels
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public int? ParentDepartmentID_Fk { get; set; }
        public string JurisdictionType { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }



        public class UserDetailsModel
        {
        //    public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Role { get; set; }
            public string Department { get; set; }
            public string Status { get; set; }
        }

    public class DepartmentModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        //public int? ParentDepartmentID_Fk { get; set; }
        //public string JurisdictionType { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool IsActive { get; set; }
    }


    public class DepartmentUserModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public int? DepartmentID { get; set; }
        public bool IsActive { get; set; }
    }

    public class DepartmentDetails
    {
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public int? ParentDepartmentID_Fk { get; set; }
        public string ParentDepartmentName { get; set; }
        //public string JurisdictionType { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? DepartmentUserID { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string DPMobile { get; set; }
        public string DPEmail { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }


}
