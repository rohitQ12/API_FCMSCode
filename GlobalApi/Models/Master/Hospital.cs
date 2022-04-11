using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Hospital
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Hos_Id { get; set; }

		[StringLength(100)]
		public string? Hos_HospitalCode { get; set; }

		[StringLength(50)]
		public string? Hos_HospitalName { get; set; }

		[StringLength(50)]
		public string? Hos_HospitalType { get; set; }

		[StringLength(50)]
		public string? Hos_Branch { get; set; }

		[StringLength(50)]
		public string? Hos_HospitalEmail { get; set; }
		public long? Hos_HospitalPhoneNo { get; set; }

		//max
		public string? Hos_HospitalAddress { get; set; }

		[StringLength(50)]
		public string? PrimaryorBranch { get; set; }

		//[StringLength(50)]
		//public string? Hos_Country { get; set; }

		[Display(Name = "Countries")]
		public virtual int Hos_Country_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Hos_Country_Id_FK")]
		public virtual Countries? Countries { get; set; }


		[Display(Name = "States")]
		public virtual int Hos_ST_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Hos_ST_Id_FK")]
		public virtual States? States { get; set; }


		[Display(Name = "Districts")]
		public virtual int Hos_DI_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Hos_DI_Id_FK")]
		public virtual Districts? Districts { get; set; }

		[StringLength(50)]
		public string? Hos_Taluk { get; set; }
		public int Hos_PostalCode { get; set; }


		[Display(Name = "Network")]
		public virtual int Hos_NE_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Hos_NE_Id_FK")]
		public virtual Network? Network { get; set; }

		[StringLength(20)]
		public string? Hos_village { get; set; }
		public long? Hos_Alterno { get; set; }
		public long? Hos_Landline { get; set; }

		[StringLength(250)]
		public string? Hos_HospitalLogo { get; set; }

		//[StringLength(50)]
		//public string? Hos_Category { get; set; }
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

	}
	public class GetAllHospital
	{
		public int Hos_Id { get; set; }
		public string? Hos_HospitalCode { get; set; }
		public string? Hos_HospitalName { get; set; }
		public string? Hos_HospitalType { get; set; }
		public string? Hos_Branch { get; set; }
		public string? Hos_HospitalEmail { get; set; }
		public long? Hos_HospitalPhoneNo { get; set; }
		public string? Hos_HospitalAddress { get; set; }
		public string? PrimaryorBranch { get; set; }
		public int Hos_Country_Id_FK { get; set; }
		public string? Hos_Country_name { get; set; }
		public int Hos_ST_Id_FK { get; set; }
		public string? Hos_state_name { get; set; }
		public int Hos_DI_Id_FK { get; set; }
		public string? Hos_district_name { get; set; }
		public string? Hos_Taluk { get; set; }
		public int Hos_PostalCode { get; set; }
		public int Hos_NE_Id_FK { get; set; }
		public string? Hos_Description { get; set; }
		public string? Hos_village { get; set; }
		public long? Hos_Alterno { get; set; }
		public long? Hos_Landline { get; set; }
		public string? Hos_HospitalLogo { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class Hospital_Images
	{
		public int Hos_Id { get; set; }
		public string? Hos_HospitalCode { get; set; }
		public string? Hos_HospitalName { get; set; }
		public string? Hos_HospitalType { get; set; }
		public string? Hos_Branch { get; set; }
		public string? Hos_HospitalEmail { get; set; }
		public long? Hos_HospitalPhoneNo { get; set; }
		public string? Hos_HospitalAddress { get; set; }
		public string? PrimaryorBranch { get; set; }
		public int Hos_Country_Id_FK { get; set; }
		public string? Hos_Country_name { get; set; }
		public int Hos_ST_Id_FK { get; set; }
		//public string? state_name { get; set; }
		public int Hos_DI_Id_FK { get; set; }
		//public string? district_name { get; set; }
		public string? Hos_Taluk { get; set; }
		public int Hos_PostalCode { get; set; }
		public int Hos_NE_Id_FK { get; set; }
		//public string? NE_Description { get; set; }
		//public string? NE_Code { get; set; }
		public string? Hos_village { get; set; }
		public long? Hos_Alterno { get; set; }
		public long? Hos_Landline { get; set; }
		public IFormFile Hos_HospitalLogo { get; set; }
		//public string? Hos_Category { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class Edit_ImageModel_Hos
	{
		public string? Hos_HospitalLogo { get; set; }
	}
	public class Hospital_DD
	{
		public int Hos_Id { get; set; }
		public string? Hos_HospitalCode { get; set; }
		public string? Hos_HospitalName { get; set; }
		public string? Hos_HospitalType { get; set; }
		public string? Hos_Branch { get; set; }
	}
	public class HospitalById
	{
		public int Hos_Id { get; set; }
		public string? Hos_HospitalCode { get; set; }
		public string? Hos_HospitalName { get; set; }
		public string? Hos_HospitalType { get; set; }
		public string? Hos_Branch { get; set; }
		public string? Hos_HospitalEmail { get; set; }
		public long? Hos_HospitalPhoneNo { get; set; }
		public string? Hos_HospitalAddress { get; set; }
		public string? PrimaryorBranch { get; set; }
		public int Hos_Country_Id_FK { get; set; }
		public string? Hos_Country_name { get; set; }
		public int Hos_ST_Id_FK { get; set; }
		public string? Hos_state_name { get; set; }
		public int Hos_DI_Id_FK { get; set; }
		public string? Hos_district_name { get; set; }
		public string? Hos_Taluk { get; set; }
		public int Hos_PostalCode { get; set; }
		public int Hos_NE_Id_FK { get; set; }
		public string? Hos_Description { get; set; }
		public string? Hos_village { get; set; }
		public long? Hos_Alterno { get; set; }
		public long? Hos_Landline { get; set; }
		public string? Hos_HospitalLogo { get; set; }
		public bool delete_flag { get; set; }
		public int? status { get; set; }

	}
}
