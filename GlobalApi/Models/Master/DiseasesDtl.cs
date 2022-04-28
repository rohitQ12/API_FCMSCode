using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DiseasesDtl
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Ddtl_Id { get; set; }


		[Display(Name = "Diseases")]
		public virtual int? Dis_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Dis_Id_FK")]
		public virtual Diseases? Diseases { get; set; }


		[Display(Name = "AppointmentModel")]
		public virtual int? Ddtl_APPT_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Ddtl_APPT_Id_FK")]
		public virtual AppointmentModel? PatientAppointment { get; set; }

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
	public class GetAllDiseasesDtl
    {
		public int Ddtl_Id { get; set; }
		public int? Dis_Id_FK { get; set; }
		public int? Ddtl_APPT_Id_FK { get; set; }
		public string? Dis_Name { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
	public class GetDiseaseDtlById
    {
		public int Ddtl_Id { get; set; }
		public int? Dis_Id_FK { get; set; }
		public int? Ddtl_APPT_Id_FK { get; set; }
		public string? Dis_Name { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
}
