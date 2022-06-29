using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Consult_Symptoms_DTL
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int SYM_Id { get; set; }

		[Display(Name = "SymptomsMst")]
		public virtual int? Smst_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Smst_Id")]
		public virtual SymptomsMst? SymptomsMst { get; set; }

		//[Display(Name = "AppointmentModel")]
		public int? CON_Id { get; set; }
		//[JsonIgnore]
		//[ForeignKey("Appt_Id")]
		//public virtual AppointmentModel? PatientAppointment { get; set; }

		public int created_by { get; set; }
		public DateTime created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

	}
	public class GetAllCSdtl
	{
		public int SYM_Id { get; set; }
		public int? Smst_Id { get; set; }
		public string? Smst_Name { get; set; }
		public int? CON_Id { get; set; }
		public bool delete_flag { get; set; }

	}
	public class GetAllCons_Symptoms
    {
		public int? Smst_Id { get; set; }
		public string? Smst_Code { get; set; }
		public string? Smst_Name { get; set; }

	}
	public class CSdtlBy_Id
	{
		public int SYM_Id { get; set; }
		public int? Smst_Id { get; set; }
		public string? Smst_Name { get; set; }
		public int? CON_Id { get; set; }
		public bool delete_flag { get; set; }

	}
}
