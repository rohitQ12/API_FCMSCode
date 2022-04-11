using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class SHReferrals
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int SHR_Id { get; set; }


		[Display(Name = "PatientAppointment")]
		public virtual int SHR_Appt_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("SHR_Appt_Id_FK")]
		public virtual AppointmentModel? PatientAppointment { get; set; }


		[Display(Name = "Consultation")]
		public virtual int? SHR_CON_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("SHR_CON_Id_FK")]
		public virtual Consultation? Consultation { get; set; }


		[Display(Name = "Patient")]
		public virtual int? SHR_PR_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("SHR_PR_Id_FK")]
		public virtual Patient? Patient { get; set; }


		//[Display(Name = "Discipline")]
		//public virtual int? SHR_CD_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("SHR_CD_Id_FK")]
		//public virtual Discipline? Discipline { get; set; }


		//[Display(Name = "Specialization")]
		//public virtual int? SHR_S_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("SHR_S_Id_FK")]
		//public virtual Specialization? Specialization { get; set; }


		//[Display(Name = "Hospital")]
		//public virtual int? SHR_H_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("SHR_H_Id_FK")]
		//public virtual Hospital? Hospital { get; set; }


		[Display(Name = "Doctor")]
		public virtual int? SHR_Ref_D_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("SHR_Ref_D_Id_FK")]
		public virtual Doctor? Doctor { get; set; }

		public string? Remarks { get; set; }

		public Nullable<System.DateTime> SHR_RH_DoctorRefferdTime { get; set; }
		public int SHR_UserId_FK { get; set; }
		//public Nullable<System.DateTime> SHR_INSTS { get; set; }

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
	public class GetAllSHReferrals
	{
		public int SHR_Id { get; set; }
		public int? SHR_Appt_Id_FK { get; set; }
		public int? SHR_CON_Id_FK { get; set; }
		//public string? SHR_CON_Weight { get; set; }
		public int? SHR_PR_Id_FK { get; set; }
		public string? SHR_PR_Name { get; set; }
		//public int? SHR_CPT_Id_FK { get; set; }
		public string? SHR_CPT_Complaint { get; set; }
		//public int? SHR_SYM_Id_FK { get; set; }
		public string? SHR_SYM_Symptoms { get; set; }
		//public int? SHR_PA_Id_FK { get; set; }
		public string? SHR_Height { get; set; }
		public string? SHR_Weight { get; set; }
		public string? SHR_TempInFahrenheit { get; set; }
		public string? SHR_TempInCelsius { get; set; }
		public string? SHR_BloodPressure { get; set; }
		public string? SHR_Sugar { get; set; }
		public string? SHR_RespiratoryRate { get; set; }
		public string? SHR_PulseRate { get; set; }
		public string? SHR_ECG { get; set; }
		public string? SHR_OxygenSaturation { get; set; }
		//public int? SHR_Disease_Id { get; set; }
		public string? SHR_Disease_Name { get; set; }
		public int? SHR_CD_Id_FK { get; set; }
		public string? SHR_CD_Name { get; set; }
		public int? SHR_S_Id_FK { get; set; }
		public string? SHR_S_Specialization { get; set; }
		public int? SHR_H_Id_FK { get; set; }
		public string? SHR_H_Name { get; set; }
		public int? SHR_Ref_D_Id_FK { get; set; }
		public string? SHR_Ref_D_Name { get; set; }
		public string? Remarks { get; set; }
		public Nullable<System.DateTime> SHR_RH_DoctorRefferdTime { get; set; }
		public int SHR_UserId_FK { get; set; }
		//public Nullable<System.DateTime> SHR_INSTS { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class SHReferralsBy_Id
	{
		public int SHR_Id { get; set; }
		public int SHR_Appt_Id_FK { get; set; }
		public int? SHR_CON_Id_FK { get; set; }
		public int? SHR_PR_Id_FK { get; set; }
		public string? SHR_PR_Name { get; set; }
		//public int? SHR_CPT_Id_FK { get; set; }
		public string? SHR_CPT_Complaint { get; set; }
		//public int? SHR_SYM_Id_FK { get; set; }
		public string? SHR_SYM_Symptoms { get; set; }
		//public int? SHR_PA_Id_FK { get; set; }
		public string? SHR_Height { get; set; }
		public string? SHR_Weight { get; set; }
		public string? SHR_TempInFahrenheit { get; set; }
		public string? SHR_TempInCelsius { get; set; }
		public string? SHR_BloodPressure { get; set; }
		public string? SHR_Sugar { get; set; }
		public string? SHR_RespiratoryRate { get; set; }
		public string? SHR_PulseRate { get; set; }
		public string? SHR_ECG { get; set; }
		public string? SHR_OxygenSaturation { get; set; }
		//public int? SHR_Disease_Id { get; set; }
		public string? SHR_Disease_Name { get; set; }
		public int? SHR_CD_Id_FK { get; set; }
		public string? SHR_CD_Name { get; set; }
		public int? SHR_S_Id_FK { get; set; }
		public string? SHR_S_Specialization { get; set; }
		public int? SHR_H_Id_FK { get; set; }
		public string? SHR_H_Name { get; set; }
		//public int? From_DO_Id_FK { get; set; }
		public int? SHR_Ref_D_Id_FK { get; set; }
		public string? SHR_Ref_D_Name { get; set; }
		public string? Remarks { get; set; }
		public Nullable<System.DateTime> SHR_RH_DoctorRefferdTime { get; set; }
		public int SHR_UserId_FK { get; set; }
		//public Nullable<System.DateTime> SHR_INSTS { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
}
