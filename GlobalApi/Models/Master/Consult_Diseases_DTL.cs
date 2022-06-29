using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Consult_Diseases_DTL
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Ddtl_Id { get; set; }


		[Display(Name = "Diseases")]
		public virtual int? Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Id")]
		public virtual Diseases? Diseases { get; set; }


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
	public class GetAllCDDtl
	{
		public int Ddtl_Id { get; set; }
		public int? Id { get; set; }
		public int? CON_Id { get; set; }
		public string? Diseases_Name { get; set; }
		public bool delete_flag { get; set; }

	}
	public class GetAllCons_Diseases
	{
		public int? Id { get; set; }
		public string? Diseases_Code { get; set; }
		public string? Acronyms { get; set; }
		public string? Diseases_Name { get; set; }

	}

	public class GetCDDtlById
	{
		public int Ddtl_Id { get; set; }
		public int? Id { get; set; }
		public int? CON_Id { get; set; }
		public string? Diseases_Name { get; set; }
		public bool delete_flag { get; set; }

	}
}
