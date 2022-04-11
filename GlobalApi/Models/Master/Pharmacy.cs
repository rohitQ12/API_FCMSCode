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
		public string? Ph_Code { get; set; }

		[StringLength(50)]
		public string? Ph_Name { get; set; }

		//max
		public string? Ph_Address { get; set; }


		[Display(Name = "Hospital")]
		public virtual int? Ph_HO_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Ph_HO_Id_FK")]
		public virtual Hospital? Hospital { get; set; }


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

		[StringLength(50)]
		public string? Ph_Village { get; set; }
		public int Ph_PostalCode { get; set; }
		public long Ph_MobileNumber { get; set; }
		public long? Ph_AlterNumber { get; set; }
		public long? Ph_LandLineNo { get; set; }

		[StringLength(50)]
		public string? Ph_Email { get; set; }
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
	public class GetAllPharmacy
	{
		public int Ph_Id { get; set; }
		public string? Ph_Code { get; set; }
		public string? Ph_Name { get; set; }
		public string? Ph_Address { get; set; }
		public int? Ph_HO_Id_FK { get; set; }
		public string? Ph_Hospital { get; set; }
		public int Ph_ST_Id_FK { get; set; }
		public string? Ph_state_name { get; set; }
		public int Ph_DI_Id_FK { get; set; }
		public string? Ph_district_name { get; set; }
		public string? Ph_Village { get; set; }
		public int Ph_PostalCode { get; set; }
		public long Ph_MobileNumber { get; set; }
		public long? Ph_AlterNumber { get; set; }
		public long? Ph_LandLineNo { get; set; }
		public string? Ph_Email { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class PharmacyById
	{
		public int Ph_Id { get; set; }
		public string? Ph_Code { get; set; }
		public string? Ph_Name { get; set; }
		public string? Ph_Address { get; set; }
		public int? Ph_HO_Id_FK { get; set; }
		public string? Ph_Hospital { get; set; }
		public int Ph_ST_Id_FK { get; set; }
		public string? Ph_state_name { get; set; }
		public int Ph_DI_Id_FK { get; set; }
		public string? Ph_district_name { get; set; }
		public string? Ph_Village { get; set; }
		public int Ph_PostalCode { get; set; }
		public long Ph_MobileNumber { get; set; }
		public long? Ph_AlterNumber { get; set; }
		public long? Ph_LandLineNo { get; set; }
		public string? Ph_Email { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class Pharmacy_DD
	{
		public int Ph_Id { get; set; }
		public string? Ph_Code { get; set; }
		public string? Ph_Name { get; set; }

	}
}
