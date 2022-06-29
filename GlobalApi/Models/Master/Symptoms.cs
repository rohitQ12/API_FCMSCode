using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Symptoms
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
        //public virtual int SYM_PR_Id_FK { get; set; }
        //[JsonIgnore]
        //[ForeignKey("SYM_PR_Id_FK")]
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
	public class GetAllSymptoms
	{
		public int SYM_Id { get; set; }
		public int? Smst_Id { get; set; }
		public string? Smst_Name { get; set; }
		public int? Appt_Id { get; set; }
		public int? Phc_Appt_Id { get; set; }

		//public int? SYM_APPT_PR_Id_FK { get; set; }
		//public int SYM_PR_Id_FK { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
	public class SymptomsBy_Id
	{
		public int SYM_Id { get; set; }
		public int? Smst_Id { get; set; }
		public string? Smst_Name { get; set; }
		public int? Appt_Id { get; set; }
		public int? Phc_Appt_Id { get; set; }

		//public int? SYM_APPT_PR_Id_FK { get; set; }
		//public int SYM_PR_Id_FK { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
}
