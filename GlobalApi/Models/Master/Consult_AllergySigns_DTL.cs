using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Consult_AllergySigns_DTL
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Ddtl_Id { get; set; }


		[Display(Name = "AllergySigns")]
		public virtual int? Al_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Al_Id")]
		public virtual AllergySigns? AllergySigns { get; set; }


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
	public class GetAllCASdtl
	{
		public int Ddtl_Id { get; set; }
		public int? Al_Id { get; set; }
		public int? CON_Id { get; set; }
		public string? Al_Name { get; set; }
		public bool delete_flag { get; set; }

	}
	public class GetAllCons_Allergys
	{
		public int? Al_Id { get; set; }
		public string? Al_Code { get; set; }
		public string? Acronyms { get; set; }
		public string? Al_Name { get; set; }

	}

	public class GetCASdtlById
	{
		public int Ddtl_Id { get; set; }
		public int? Al_Id { get; set; }
		public int? CON_Id { get; set; }
		public string? Al_Name { get; set; }
		public bool delete_flag { get; set; }

	}
}
