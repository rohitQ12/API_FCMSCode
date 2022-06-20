using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Assistant
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Assi_Id { get; set; }
		public string Asssi_UserID { get; set; }

		public string? ASISfxPrfxId { get; set; }

		[StringLength(3)]
		public string? Assi_code { get; set; }

		[StringLength(50)]
		public string? Assi_FirstName { get; set; }

		[StringLength(50)]
		public string? Assi_LastName { get; set; }
		public DateTime? Assi_DOB { get; set; }

		[StringLength(20)]
		public string? Assi_Gender { get; set; }

		public int? Assi_MotherTongue { get; set; }

		[Display(Name = "Hospital")]
		public virtual int? Assi_Hos_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Assi_Hos_Id_FK")]
		public virtual Hospital? Hospital { get; set; }


		[Display(Name = "Qualification")]
		public virtual int? Assi_Qua_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Assi_Qua_Id_FK")]
		public virtual Qualification? Qualification { get; set; }


		[Display(Name = "Designation")]
		public virtual int? Assi_Des_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Assi_Des_Id_FK")]
		public virtual Designation? Designation { get; set; }


		public int? Assi_skill_id { get; set; }


		[StringLength(255)]
		public string? Assi_Photo { get; set; }
		public string? Assi_Address { get; set; }

		[Display(Name = "Countries")]
		public virtual int? Assi_Country_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Assi_Country_Id_FK")]
		public virtual Countries? Countries { get; set; }

		[Display(Name = "States")]
		public virtual int? Assi_ST_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Assi_ST_Id_FK")]
		public virtual States? States { get; set; }


		[Display(Name = "Districts")]
		public virtual int? Assi_DI_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Assi_DI_Id_FK")]
		public virtual Districts? Districts { get; set; }

		public int? taluk_Id_Fk { get; set; }
		public int? gram_Id_Fk { get; set; }

		//[StringLength(50)]
		//public string? Assi_Village { get; set; }
		public int? Assi_PostalCode { get; set; }
		public long Assi_MobileNumber { get; set; }
		public long? Assi_LandLineNumber { get; set; }
		public long? Assi_AlternativeNumber { get; set; }

		[StringLength(50)]
		public string? Assi_Email { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

		[Required]
		public int status { get; set; }
		
		[StringLength(250)]
		public string? Remarks { get; set; }


	}
	public class GetAllAssistant
	{
		public int Assi_Id { get; set; }
		public string? ASISfxPrfxId { get; set; }
		public string? Assi_code { get; set; }
		public string? Assi_FirstName { get; set; }
		public string? Assi_LastName { get; set; }
		public DateTime? Assi_DOB { get; set; }
		public string? Assi_Gender { get; set; }
		public int? Assi_MotherTongue { get; set; }
		public string? Language { get; set; }
		public int? Assi_Hos_Id_FK { get; set; }
		public string? Assi_Hos_HospitalName { get; set; }
		public int? Assi_Qua_Id_FK { get; set; }
		public string? Assi_qualification { get; set; }
		public int? Assi_Des_Id_FK { get; set; }
		public string? Assi_Designation { get; set; }
		public int? Assi_skill_id { get; set; }
		public string? Assi_Skill { get; set; }
		public string? Assi_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public string? Assi_Address { get; set; }
		public int? Assi_Country_Id_FK { get; set; }
		public string? Assi_Country_name { get; set; }
		public int? Assi_ST_Id_FK { get; set; }
		public string? state_name { get; set; }
		public int? Assi_DI_Id_FK { get; set; }
		public string? district_name { get; set; }
		public int? taluk_Id_Fk { get; set; }
		public string? taluk_name { get; set; }
		public int? gram_Id_Fk { get; set; }
		public string? gram_name { get; set; }

		//public string? Assi_Village { get; set; }
		public int? Assi_PostalCode { get; set; }
		public long Assi_MobileNumber { get; set; }
		public long? Assi_LandLineNumber { get; set; }
		public long? Assi_AlternativeNumber { get; set; }
		public string? Assi_Email { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class Assistant_DD
	{
		public int Assi_Id { get; set; }
		public string? ASISfxPrfxId { get; set; }
		public string? Assi_code { get; set; }
		public string? Assi_FirstName { get; set; }
		public string? Assi_LastName { get; set; }
	}
	public class AssistantById
	{
		public int Assi_Id { get; set; }
		public string? ASISfxPrfxId { get; set; }
		public string? Assi_code { get; set; }
		public string? Assi_FirstName { get; set; }
		public string? Assi_LastName { get; set; }
		public Nullable<System.DateTime> Assi_DOB { get; set; }
		public string? Assi_Gender { get; set; }
		public int? Assi_MotherTongue { get; set; }
		public string? Language { get; set; }
		public int? Assi_Hos_Id_FK { get; set; }
		public string? Assi_Hos_HospitalName { get; set; }
		public int? Assi_Qua_Id_FK { get; set; }
		public string? Assi_qualification { get; set; }
		public int? Assi_Des_Id_FK { get; set; }
		public string? Assi_Designation { get; set; }
		public int? Assi_skill_id { get; set; }
		public string? Assi_Skill { get; set; }
		public string? Assi_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public string? Assi_Address { get; set; }
		public int? Assi_Country_Id_FK { get; set; }
		public string? Assi_Country_name { get; set; }
		public int? Assi_ST_Id_FK { get; set; }
		public string? state_name { get; set; }
		public int? Assi_DI_Id_FK { get; set; }
		public string? district_name { get; set; }
		public int? taluk_Id_Fk { get; set; }
		public string? taluk_name { get; set; }
		public int? gram_Id_Fk { get; set; }
		public string? gram_name { get; set; }
		//public string? Assi_Village { get; set; }
		public int? Assi_PostalCode { get; set; }
		public long Assi_MobileNumber { get; set; }
		public long? Assi_LandLineNumber { get; set; }
		public long? Assi_AlternativeNumber { get; set; }
		public string? Assi_Email { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class Assistant_Images
	{
		public int Assi_Id { get; set; }
		public string? ASISfxPrfxId { get; set; }
		public string? Assi_code { get; set; }
		public string? Assi_FirstName { get; set; }
		public string? Assi_LastName { get; set; }
		public Nullable<System.DateTime> Assi_DOB { get; set; }
		public string? Assi_Gender { get; set; }
		public int? Assi_MotherTongue { get; set; }
		public int? Assi_Hos_Id_FK { get; set; }
		public int? Assi_Qua_Id_FK { get; set; }
		public int? Assi_Des_Id_FK { get; set; }
		public int? Assi_skill_id { get; set; }
		public IFormFile? Assi_Photo { get; set; }
		public string? Assi_Address { get; set; }
		public int? Assi_Country_Id_FK { get; set; }
		public int? Assi_ST_Id_FK { get; set; }
		public int? Assi_DI_Id_FK { get; set; }
		public int? taluk_Id_Fk { get; set; }
		public int? gram_Id_Fk { get; set; }
		//public string? Assi_Village { get; set; }
		public int? Assi_PostalCode { get; set; }
		public long Assi_MobileNumber { get; set; }
		public long? Assi_LandLineNumber { get; set; }
		public long? Assi_AlternativeNumber { get; set; }
		public string? Assi_Email { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class Edit_ImageModel_Ass
	{
		public string? Assi_Photo { get; set; }
	}
	public class ApproveAssistant
	{
		public int Assi_Id { get; set; }
		public string? Remarks { get; set; }
	}
}
