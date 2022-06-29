using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class AllergySigns_DTL
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


		[Display(Name = "AppointmentModel")]
		public virtual int? Appt_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Appt_Id")]
		public virtual AppointmentModel? PatientAppointment { get; set; }

		[Display(Name = "PHC_Appointment")]
		public virtual int? Phc_Appt_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Phc_Appt_Id")]
		public virtual PHC_Appointment? PHC_Appointment { get; set; }


		//[Display(Name = "Patient")]
		//public virtual int Ddtl_PR_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("Ddtl_PR_Id_FK")]
		//public virtual Patient? Patient { get; set; }

		[StringLength(255)]
		public string? Remarks { get; set; }
		public int created_by { get; set; }
		public DateTime created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

	}
	public class GetAllAllergySigns_DTL
	{
		//public int Ddtl_Id { get; set; }
		public int? Al_Id { get; set; }
		public string? Al_Code { get; set; }
		public string? Acronyms { get; set; }
		public string? Al_Name { get; set; }
		//public int? Appt_Id { get; set; }
		//public int? Phc_Appt_Id { get; set; }
		//public string? Remarks { get; set; }
		//public bool delete_flag { get; set; }

	}
	public class GetAllergySigns_DTLById
	{
		public int Ddtl_Id { get; set; }
		public int? Al_Id { get; set; }
		public int? Appt_Id { get; set; }
		public int? Phc_Appt_Id { get; set; }
		public string? Al_Name { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
}
