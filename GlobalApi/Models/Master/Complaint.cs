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
		public virtual int? Cmst_Id { get; set; } = null!;
		[JsonIgnore]
		[ForeignKey("Cmst_Id")]
		public virtual ComplaintMst? ComplaintMst { get; set; }


		[Display(Name = "AppointmentModel")]
		public virtual int? Appt_Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Appt_Id")]
		public virtual AppointmentModel? PatientAppointment { get; set; }


        [Display(Name = "ManualAppointment")]
        public virtual int? MAppt_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("MAppt_Id")]
        public virtual ManualAppointment? ManualAppointment { get; set; }


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
		public int? Cmst_Id { get; set; }
		public string Cmst_Name { get; set; }
		public int? Appt_Id { get; set; }
        public int? MAppt_Id { get; set; }

        //public int? CPT_APPT_PR_Id_FK { get; set; }
        public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
	public class ComplaintBy_Id
	{
		public int CPT_Id { get; set; }
		public int? Cmst_Id { get; set; }
		public string Cmst_Name { get; set; }
		public int? Appt_Id { get; set; }
        public int? MAppt_Id { get; set; }

        //public int? CPT_APPT_PR_Id_FK { get; set; }
        //public int CPT_PR_Id_FK { get; set; }
        public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
}
