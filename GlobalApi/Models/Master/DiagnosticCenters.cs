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
		public string? DGSTC_Code { get; set; }

		[StringLength(50)]
		public string? DGSTC_Name { get; set; }

		//max
		public string? DGSTC_Address { get; set; }

		[Display(Name = "Hospital")]
		public virtual int? DGSTC_HO_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DGSTC_HO_Id_FK")]
		public virtual Hospital? Hospital { get; set; }

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

		[StringLength(50)]
		public string? DGSTC_Village { get; set; }
		public int DGSTC_PostalCode { get; set; }
		public long? DGSTC_MobileNumber { get; set; }
		public long? DGSTC_AlterNumber { get; set; }
		public long? DGSTC_LandLineNo { get; set; }

		[StringLength(50)]
		public string? DGSTC_Email { get; set; }
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
	public class GetAllDiagnosticCenters
	{
		public int DGSTC_Id { get; set; }
		public string? DGSTC_Code { get; set; }
		public string? DGSTC_Name { get; set; }
		public string? DGSTC_Address { get; set; }
		public int? DGSTC_HO_Id_FK { get; set; }
		public string? DGSTC_Hospital { get; set; }
		public int DGSTC_ST_Id_FK { get; set; }
		public string? DGSTC_state_name { get; set; }
		public int DGSTC_DI_Id_FK { get; set; }
		public string? DGSTC_district_name { get; set; }
		public string? DGSTC_Village { get; set; }
		public int DGSTC_PostalCode { get; set; }
		public long? DGSTC_MobileNumber { get; set; }
		public long? DGSTC_AlterNumber { get; set; }
		public long? DGSTC_LandLineNo { get; set; }
		public string? DGSTC_Email { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class DiagnosticCentersById
	{
		public int DGSTC_Id { get; set; }
		public string? DGSTC_Code { get; set; }
		public string? DGSTC_Name { get; set; }
		public string? DGSTC_Address { get; set; }
		public int? DGSTC_HO_Id_FK { get; set; }
		public string? DGSTC_Hospital { get; set; }
		public int DGSTC_ST_Id_FK { get; set; }
		public string? DGSTC_state_name { get; set; }
		public int DGSTC_DI_Id_FK { get; set; }
		public string? DGSTC_district_name { get; set; }
		public string? DGSTC_Village { get; set; }
		public int DGSTC_PostalCode { get; set; }
		public long? DGSTC_MobileNumber { get; set; }
		public long? DGSTC_AlterNumber { get; set; }
		public long? DGSTC_LandLineNo { get; set; }
		public string? DGSTC_Email { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class DiagnosticCenters_DD
	{
		public int DGSTC_Id { get; set; }
		public string? DGSTC_Code { get; set; }
		public string? DGSTC_Name { get; set; }

	}

}
