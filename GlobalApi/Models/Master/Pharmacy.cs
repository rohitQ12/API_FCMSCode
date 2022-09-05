using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Pharmacy
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Ph_Id { get; set; }

		[StringLength(3)]
		public string? Ph_Code { get; set; }

		[StringLength(50)]
		public string? Ph_Name { get; set; }

		//max
		public string? Ph_Address { get; set; }

		[StringLength(50)]
		public string? PrimaryOrBranch { get; set; }

		public int? Ph_Branch { get; set; }


		[Display(Name = "PharmacyType")]
		public virtual int? T_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("T_Id")]
		public virtual PharmacyType? PharmacyType { get; set; }


		[Display(Name = "PharmacyCategory")]
		public virtual int? cat_id { get; set; }
		[JsonIgnore]
		[ForeignKey("cat_id")]
		public virtual PharmacyCategory? PharmacyCategory { get; }


		[Display(Name = "Network")]
		public virtual int? Ph_NE_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_NE_Id")]
		public virtual Network? Network { get; set; }

		[Display(Name = "Hospital")]
		public virtual int? Ph_HO_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_HO_Id_FK")]
		public virtual Hospital? Hospital { get; set; }

		[Display(Name = "Countries")]
		public virtual int? Ph_COUN_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_COUN_Id")]
		public virtual Countries? Countries { get; set; }


		[Display(Name = "States")]
		public virtual int Ph_ST_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_ST_Id_FK")]
		public virtual States? States { get; set; }


		[Display(Name = "Districts")]
		public virtual int Ph_DI_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_DI_Id_FK")]
		public virtual Districts? Districts { get; set; }



		[Display(Name = "Taluk")]
		public virtual int? Ph_tl_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_tl_Id")]
		public virtual Taluk? Taluk { get; set; }


		[Display(Name = "Gram")]
		public virtual int? Ph_GR_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_GR_Id")]
		public virtual Gram? Gram { get; set; }

		public int Ph_PostalCode { get; set; }
		public long Ph_MobileNumber { get; set; }
		public long? Ph_AlterNumber { get; set; }

		[StringLength(11)]
		public string? Ph_LandLineNo { get; set; }

		[StringLength(50)]
		public string? Ph_Email { get; set; }
		
		[StringLength(50)]
		public string? GSTno { get; set; }
		
		[StringLength(50)]
		public string? PANno { get; set; }


		[StringLength(50)]
		public string? RegNo { get; set; }
		
		[StringLength(250)]
		public string? Ph_Logo { get; set; }
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
	public class GetAllPharmacy
	{
		public int Ph_Id { get; set; }
		public string? Ph_Code { get; set; }
		public string? Ph_Name { get; set; }
		public string? Ph_Address { get; set; }
		public string? PrimaryOrBranch { get; set; }
		public int? Ph_Branch { get; set; }
		public string? Branch_Name { get; set; }
		public int? T_Id { get; set; }
		public string Type { get; set; }
		public int? cat_id { get; set; }
		public string name { get; set; }
		public int? Ph_NE_Id { get; set; }
		public string? NE_Description { get; set; }
		public int? Ph_HO_Id_FK { get; set; }
		public string? Ph_Hospital { get; set; }
		public int? Ph_COUN_Id_FK { get; set; }
		public string? Countries_name { get; set; }
		public int Ph_ST_Id_FK { get; set; }
		public string? Ph_state_name { get; set; }
		public int Ph_DI_Id_FK { get; set; }
		public string? Ph_district_name { get; set; }
		public int? Ph_tl_Id { get; set; }
		public string? Taluk_Name { get; set; }
		public int? Ph_GR_Id { get; set; }
		public string? gram_Name { get; set; }
		public int Ph_PostalCode { get; set; }
		public long Ph_MobileNumber { get; set; }
		public long? Ph_AlterNumber { get; set; }
		public string? Ph_LandLineNo { get; set; }
		public string? Ph_Email { get; set; }
		public string? GSTno { get; set; }
		public string? PANno { get; set; }
		public string? RegNo { get; set; }
		public string? Ph_Logo { get; set; }
		public byte[]? Logobyte { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class PharmacyById
	{
		public int Ph_Id { get; set; }
		public string? Ph_Code { get; set; }
		public string? Ph_Name { get; set; }
		public string? Ph_Address { get; set; }
		public string? PrimaryOrBranch { get; set; }
		public int? Ph_Branch { get; set; }
		public string? Branch_Name { get; set; }
		public int? T_Id { get; set; }
		public string Type { get; set; }
		public int? cat_id { get; set; }
		public string name { get; set; }
		public int? Ph_NE_Id { get; set; }
		public string? NE_Description { get; set; }
		public int? Ph_HO_Id_FK { get; set; }
		public string? Ph_Hospital { get; set; }
		public int? Ph_COUN_Id_FK { get; set; }
		public string? Countries_name { get; set; }
		public int Ph_ST_Id_FK { get; set; }
		public string? Ph_state_name { get; set; }
		public int Ph_DI_Id_FK { get; set; }
		public string? Ph_district_name { get; set; }
		public int? Ph_tl_Id { get; set; }
		public string? Taluk_Name { get; set; }
		public int? Ph_GR_Id { get; set; }
		public string? gram_Name { get; set; }
		public int Ph_PostalCode { get; set; }
		public long Ph_MobileNumber { get; set; }
		public long? Ph_AlterNumber { get; set; }
		public string? Ph_LandLineNo { get; set; }
		public string? Ph_Email { get; set; }
		public string? GSTno { get; set; }
		public string? PANno { get; set; }
		public string? RegNo { get; set; }
		public string? Ph_Logo { get; set; }
		public byte[]? Logobyte { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class Pharmacy_DD
	{
		public int Ph_Id { get; set; }
		public string? Ph_Code { get; set; }
		public string? Ph_Name { get; set; }
		public int? Ph_NE_Id { get; set; }
		public string? NE_Description { get; set; }


	}
	public class Pharmacy_Images
    {
		public int Ph_Id { get; set; }
		public string? Ph_Code { get; set; }
		public string? Ph_Name { get; set; }
		public string? Ph_Address { get; set; }
		public string? PrimaryOrBranch { get; set; }
		public int? Ph_Branch { get; set; }
		public int? T_Id { get; set; }
		public int? cat_id { get; set; }
		public int? Ph_NE_Id { get; set; }
		public int? Ph_HO_Id_FK { get; set; }
		public string? Ph_Hospital { get; set; }
		public int? Ph_COUN_Id { get; set; }
		public int Ph_ST_Id_FK { get; set; }
		public int Ph_DI_Id_FK { get; set; }
		public int? Ph_tl_Id { get; set; }
		public int? Ph_GR_Id { get; set; }
		public int Ph_PostalCode { get; set; }
		public long Ph_MobileNumber { get; set; }
		public long? Ph_AlterNumber { get; set; }
		public string? Ph_LandLineNo { get; set; }
		public string? Ph_Email { get; set; }
		public string? GSTno { get; set; }
		public string? PANno { get; set; }
		public string? RegNo { get; set; }
		public IFormFile? Ph_Logo { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class ApprovePharmacy
    {
		public int Ph_Id { get; set; }
		public string? Remarks { get; set; }
	}
}
