using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Complaint
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int CPT_Id { get; set; }


		[Display(Name = "ComplaintMst")]
		public virtual int? CPT_MST_Id_FK { get; set; } = null!;
		[JsonIgnore]
		[ForeignKey("CPT_MST_Id_FK")]
		public virtual ComplaintMst? ComplaintMst { get; set; }


		[Display(Name = "AppointmentModel")]
		public virtual int? CPT_APPT_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CPT_APPT_Id_FK")]
		public virtual AppointmentModel? PatientAppointment { get; set; }

		//[Display(Name = "Patient")]
		//public virtual int CPT_PR_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("CPT_PR_Id_FK")]
		//public virtual Patient? Patient { get; set; }

		[StringLength(255)]
		public string? Remarks { get; set; }
		public int? created_by { get; set; }
		public DateTime created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }


	}
	public class GetAllComplaint
	{
		public int CPT_Id { get; set; }
		public int? CPT_MST_Id_FK { get; set; }
		public string? CPT_MST_Name { get; set; }
		public int? CPT_APPT_Id_FK { get; set; }
		//public int? CPT_APPT_PR_Id_FK { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
	public class ComplaintBy_Id
	{
		public int CPT_Id { get; set; }
		public int? CPT_MST_Id_FK { get; set; }
		public string? CPT_MST_Name { get; set; }
		public int? CPT_APPT_Id_FK { get; set; }
		//public int? CPT_APPT_PR_Id_FK { get; set; }
		//public int CPT_PR_Id_FK { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
}
