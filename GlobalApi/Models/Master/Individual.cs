using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{

    public class Individual
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Ind_Id { get; set; }
        public string? Ind_code { get; set; } 
        public string? Ind_Name { get; set; }

        public DateTime? Ind_DOB { get; set; }
        public string? Ind_Gender { get; set; }


        public string? Ind_Address { get; set; }


        [Display(Name = "Countries")]
        public virtual int Ind_Country_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Ind_Country_Id_FK")]
        public virtual Countries? Countries { get; set; }


        [Display(Name = "States")]
        public virtual int Ind_ST_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Ind_ST_Id_FK")]
        public virtual States? States { get; set; }

        public string? Ind_DI_Name { get; set; }

        public string Ind_MobileNumber { get; set; }

        public string Ind_Email { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int Ind_status { get; set; }
        public string? Ind_fromDate { get; set; }
        public string? Ind_ToDate { get; set; }

        public string? Remarks { get; set; }

        public string? Ind_UserID { get; set; }

        public class GetAllIndividual
        {

            public int Ind_Id { get; set; }

            public string? Ind_code { get; set; }
            public string? Ind_Name { get; set; }

            public DateTime? Ind_DOB { get; set; }
            public string? Ind_Gender { get; set; }

            public string? Ind_Image { get; set; }
            public string? Ind_Address { get; set; }
            public int? Ind_Country_Id_FK { get; set; }
            public string? Ind_Country_name { get; set; }
            public int? Ind_ST_Id_FK { get; set; }
            public string? state_name { get; set; }
            public int? Ind_DI_Id_FK { get; set; }
            public string? district_name { get; set; }
            public int? taluk_Id_Fk { get; set; }
            public string? taluk_name { get; set; }
            public string Ind_MobileNumber { get; set; }
            public string? Ind_Email { get; set; }
            public bool delete_flag { get; set; }
            public int Ind_status { get; set; }
            public string? sts_name { get; set; }
            public string? Ind_fromDate { get; set; }
            public string? Ind_ToDate { get; set; }

            public string? Ind_DI_Name { get; set; }
            public string? Remarks { get; set; }
            public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

        }
        public class Individual_DD
        {
            public int Ind_Id { get; set; }
            //public string? ASISfxPrfxId { get; set; }
            public string? Ind_code { get; set; }
            public string? Ind_FirstName { get; set; }
            public string? Ind_LastName { get; set; }
        }
        public class IndividualById
        {
            public int Ind_Id { get; set; }
            //public string? ASISfxPrfxId { get; set; }
            public string? Ind_code { get; set; }
            public string? Ind_Name { get; set; }
            //public string? Ind_LastName { get; set; }
            public Nullable<System.DateTime> Ind_DOB { get; set; }
            public string? Ind_Gender { get; set; }
            //  public int? Ind_MotherTongue { get; set; }
            // public string? Language { get; set; }
            //public int? Ind_Hos_Id_FK { get; set; }
            //public string? Ind_Hos_HospitalName { get; set; }
            //  public int? Ind_Qua_Id_FK { get; set; }
            //  public string? Ind_qualification { get; set; }
            //public int? Ind_Des_Id_FK { get; set; }
            //  public string? Ind_Designation { get; set; }
            //  public int? Ind_skill_id { get; set; }
            // public string? Ind_Skill { get; set; }
            // public string? Ind_Photo { get; set; }
            // public byte[]? Imagebyte { get; set; }
            //  public string? Ind_Image { get; set; }
            public string? Ind_Address { get; set; }
            public int? Ind_Country_Id_FK { get; set; }
            public string? Ind_Country_name { get; set; }
            public int? Ind_ST_Id_FK { get; set; }
            public string? state_name { get; set; }
            //  public int? Ind_DI_Id_FK { get; set; }
            public string? district_name { get; set; }
            // public int? taluk_Id_Fk { get; set; }
            //  public string? taluk_name { get; set; }
            // public int? gram_Id_Fk { get; set; }
            // public string? gram_name { get; set; }
            //  public string? Ind_Village { get; set; }
            //  public int? Ind_PostalCode { get; set; }
            public string Ind_MobileNumber { get; set; }
            //public string? Assi_LandLineNumber { get; set; }
            // public string? Ind_AlternativeNumber { get; set; }
            public string? Ind_Email { get; set; }
            public bool delete_flag { get; set; }
            public int Ind_status { get; set; }
            public string? sts_name { get; set; }
            public string? Ind_UserID { get; set; }
            public string? Ind_fromDate { get; set; }
            public string? Ind_ToDate { get; set; }

            public string? Remarks { get; set; }
            //  public string? Ind_Skill_Desc { get; set; }
            public List<GetAllStudentCourse>? GetAllStudentCourse { get; set; } = null!;

        }


        public class Individual_Images
        {

            public Individual_Images()
            {
                Ind_Id = 0;
                Ind_UserID = string.Empty;
                Ind_MobileNumber = "0";
                Ind_Email = string.Empty;
                Ind_Country_Id_FK = 0;
                Ind_ST_Id_FK = 0;
                Ind_DI_Name = string.Empty;
            }
            public int Ind_Id { get; set; }

           
            public string? Ind_Name { get; set; }

            public Nullable<System.DateTime> Ind_DOB { get; set; }
            public string? Ind_Gender { get; set; }

            //  public IFormFile? Ind_Photo { get; set; }
            public string? Ind_Address { get; set; }
            public int Ind_Country_Id_FK { get; set; }
            public int Ind_ST_Id_FK { get; set; }


            public string Ind_MobileNumber { get; set; }
            //public string? Assi_LandLineNumber { get; set; }
            public string? Ind_DI_Name { get; set; }

            public string Ind_Email { get; set; }
            public int? created_by { get; set; }
            public string? Ind_fromDate { get; set; }
            public string? Ind_ToDate { get; set; }
            public Nullable<System.DateTime> created_date { get; set; }
            public int? modified_by { get; set; }
            public Nullable<System.DateTime> modified_date { get; set; }
            public int? deleted_by { get; set; }
            public Nullable<System.DateTime> deleted_date { get; set; }
            public bool delete_flag { get; set; }
            public int Ind_status { get; set; }
            //public string? Ind_Skill_Desc { get; set; }
            //public IFormFile? Ind_Choose_Document { get; set; }
            //public IFormFile? MOUDocument { get; set; }
            public string? Ind_UserID { get; set; }
            public string? order_id { get; set; }
            public List<StudentCourse>? Student_Course { get; set; } = null!;


        }



        public class ApproveIndividual
        {
            public int Ind_Id { get; set; }
            public string? Remarks { get; set; }
        }
    }
}
