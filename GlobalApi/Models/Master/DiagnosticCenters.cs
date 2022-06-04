using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class DiagnosticCenters
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int DGSTC_Id { get; set; }

		[StringLength(10)]
		public string? DGSTC_Code { get; set; }

		[StringLength(100)]
		public string? DGSTC_Name { get; set; }
		
		[StringLength(50)]
		public string? PrimaryOrBranch { get; set; }
		public int? DGSTC_Branch { get; set; }
		

		[Display(Name = "DiagnosticType")]
		public virtual int DGSTC_Type_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_Type_Id")]
		public virtual DiagnosticType? DiagnosticType { get; set; }

		[Display(Name = "DiagnoCategory")]
		public virtual int? cat_id { get; set; }
		[JsonIgnore]
		[ForeignKey("cat_id")]
		public virtual DiagnoCategory? DiagnoCategory { get; }

		[Display(Name = "Network")]
		public virtual int DGSTC_NE_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_NE_Id")]
		public virtual Network? Network { get; set; }

		//max
		public string? DGSTC_Address { get; set; }

		[Display(Name = "Hospital")]
		public virtual int? DGSTC_HO_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_HO_Id_FK")]
		public virtual Hospital? Hospital { get; set; }

		[Display(Name = "Countries")]
		public virtual int DGSTC_COUN_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_COUN_Id_FK")]
		public virtual Countries? Countries { get; set; }


		[Display(Name = "States")]
		public virtual int DGSTC_ST_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_ST_Id_FK")]
		public virtual States? States { get; set; }


		[Display(Name = "Districts")]
		public virtual int DGSTC_DI_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_DI_Id_FK")]
		public virtual Districts? Districts { get; set; }


		[Display(Name = "Taluk")]
		public virtual int DGSTC_TL_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_TL_Id_FK")]
		public virtual Taluk? Taluk { get; set; }


		[Display(Name = "Gram")]
		public virtual int DGSTC_GR_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_GR_Id_FK")]
		public virtual Gram? Gram { get; set; }

		public int DGSTC_PostalCode { get; set; }
		public long? DGSTC_MobileNumber { get; set; }
		public long? DGSTC_AlterNumber { get; set; }
		public long? DGSTC_LandLineNo { get; set; }

		[StringLength(50)]
		public string? DGSTC_Email { get; set; }
		[StringLength(50)]
		public string? GSTno { get; set; }

		[StringLength(50)]
		public string? PANno { get; set; }

		[StringLength(50)]
		public string? RegNo { get; set; }
		
		[StringLength(250)]
		public string? DGSTC_Logo { get; set; }

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
	public class GetAllDiagnosticCenters
	{
		public int DGSTC_Id { get; set; }
		public string? DGSTC_Code { get; set; }
		public string? DGSTC_Name { get; set; }
		public string? PrimaryOrBranch { get; set; }
		public int? DGSTC_Branch { get; set; }
		public string? branch_name { get; set; }
		public int DGSTC_Type_Id { get; set; }
		public string Type { get; set; }
		public int? cat_id { get; set; }
		public string name { get; set; }
		public int DGSTC_NE_Id { get; set; }
		public string? NE_Description { get; set; }
		public string? DGSTC_Address { get; set; }
		public int? DGSTC_HO_Id_FK { get; set; }
		public string? Hos_HospitalName { get; set; }
		public int DGSTC_COUN_Id_FK { get; set; }
		public string? country_name { get; set; }
		public int DGSTC_ST_Id_FK { get; set; }
		public string? state_name { get; set; }
		public int DGSTC_DI_Id_FK { get; set; }
		public string? district_name { get; set; }
		public int DGSTC_TL_Id_FK { get; set; }
		public string? Taluk_name { get; set; }
		public int DGSTC_GR_Id_FK { get; set; }
		public string? Gram_name { get; set; }
		public int DGSTC_PostalCode { get; set; }
		public long? DGSTC_MobileNumber { get; set; }
		public long? DGSTC_AlterNumber { get; set; }
		public long? DGSTC_LandLineNo { get; set; }
		public string? DGSTC_Email { get; set; }
		public string? GSTno { get; set; }
		public string? PANno { get; set; }
		public string? RegNo { get; set; }
		public string? DGSTC_Logo { get; set; }
		public byte[]? Logobyte { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class DiagnosticCentersById
	{
		public int DGSTC_Id { get; set; }
		public string? DGSTC_Code { get; set; }
		public string? DGSTC_Name { get; set; }
		public string? PrimaryOrBranch { get; set; }
		public int? DGSTC_Branch { get; set; }
		public string? branch_name { get; set; }
		public int DGSTC_Type_Id { get; set; }
		public string Type { get; set; }
		public int? cat_id { get; set; }
		public string name { get; set; }
		public int DGSTC_NE_Id { get; set; }
		public string? NE_Description { get; set; }
		public string? DGSTC_Address { get; set; }
		public int? DGSTC_HO_Id_FK { get; set; }
		public string? Hos_HospitalName { get; set; }
		public int DGSTC_COUN_Id_FK { get; set; }
		public string? country_name { get; set; }
		public int DGSTC_ST_Id_FK { get; set; }
		public string? state_name { get; set; }
		public int DGSTC_DI_Id_FK { get; set; }
		public string? district_name { get; set; }
		public int DGSTC_TL_Id_FK { get; set; }
		public string? Taluk_name { get; set; }
		public int DGSTC_GR_Id_FK { get; set; }
		public string? Gram_name { get; set; }
		public int DGSTC_PostalCode { get; set; }
		public long? DGSTC_MobileNumber { get; set; }
		public long? DGSTC_AlterNumber { get; set; }
		public long? DGSTC_LandLineNo { get; set; }
		public string? DGSTC_Email { get; set; }
		public string? GSTno { get; set; }
		public string? PANno { get; set; }
		public string? RegNo { get; set; }
		public string? DGSTC_Logo { get; set; }
		public byte[]? Logobyte { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class DiagnosticCenters_DD
	{
		public int DGSTC_Id { get; set; }
		public string? DGSTC_Code { get; set; }
		public string? DGSTC_Name { get; set; }
		public int DGSTC_NE_Id { get; set; }
		public string? NE_Description { get; set; }


	}
	public class Diagnostic_Images
    {
		public int DGSTC_Id { get; set; }
		public string? DGSTC_Code { get; set; }
		public string? DGSTC_Name { get; set; }
		public string? PrimaryOrBranch { get; set; }
		public int? DGSTC_Branch { get; set; }
		public int DGSTC_Type_Id { get; set; }
		public int? cat_id { get; set; }
		public int DGSTC_NE_Id { get; set; }
		public string? DGSTC_Address { get; set; }
		public int? DGSTC_HO_Id_FK { get; set; }
		public int DGSTC_COUN_Id_FK { get; set; }
		public int DGSTC_ST_Id_FK { get; set; }
		public int DGSTC_DI_Id_FK { get; set; }
		public int DGSTC_TL_Id_FK { get; set; }
		public int DGSTC_GR_Id_FK { get; set; }
		public int DGSTC_PostalCode { get; set; }
		public long? DGSTC_MobileNumber { get; set; }
		public long? DGSTC_AlterNumber { get; set; }
		public long? DGSTC_LandLineNo { get; set; }
		public string? DGSTC_Email { get; set; }
		public string? GSTno { get; set; }
		public string? PANno { get; set; }
		public string? RegNo { get; set; }
		public IFormFile? DGSTC_Logo { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class ApproveDiagnosticCenter
    {
		public int DGSTC_Id { get; set; }
		public string? Remarks { get; set; }
	}

}
