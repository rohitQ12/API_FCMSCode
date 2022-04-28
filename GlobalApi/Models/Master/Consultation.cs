using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Consultation
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int CON_Id { get; set; }

		[StringLength(50)]
		public string? CON_Code { get; set; }

		[StringLength(50)]
		public string? CON_Type { get; set; }

		[Display(Name = "AppointmentModel")]
		public virtual int CON_APPT_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CON_APPT_Id_FK")]
		public virtual AppointmentModel? AppointmentModel { get; set; }


		[Display(Name = "Patient")]
		public virtual int? CON_PR_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CON_PR_Id_FK")]
		public virtual Patient? Patient { get; set; }


		[Display(Name = "Doctor")]
		public virtual int? CON_DO_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CON_DO_Id_FK")]
		public virtual Doctor? Doctor { get; set; }


		[Display(Name = "Hospital")]
		public virtual int? CON_HO_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CON_HO_Id_FK")]
		public virtual Hospital? Hospital { get; set; }


		[Display(Name = "Discipline")]
		public virtual int? CON_CD_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CON_CD_Id_FK")]
		public virtual Discipline? Discipline { get; set; }


		[Display(Name = "Specialization")]
		public virtual int? CON_SP_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CON_SP_Id_FK")]
		public virtual Specialization? Specialization { get; set; }


		[Display(Name = "Diseases")]
		public virtual int? Dis_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Dis_Id_FK")]
		public virtual Diseases? Diseases { get; set; }
		public int? CON_Ref_AS_Id { get; set; }
		public Nullable<System.DateTime> CON_ConsultedDate { get; set; }
		public int CON_UserId_FK { get; set; }

		[StringLength(1)]
		public string? Inactive { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

		[Required]
		public int status { get; set; }
	}

	public class GetAllConsultation
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		public int CON_APPT_Id_FK { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public Nullable<System.DateTime> CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public int? CON_DO_Id_FK { get; set; }
		public string? CON_DO_Name { get; set; }
		public int? CON_HO_Id_FK { get; set; }
		public string? CON_HospitalName { get; set; }
		public int? CON_CD_Id_FK { get; set; }
		public string? CON_ClinicalDiscipline { get; set; }
		public int? CON_SP_Id_FK { get; set; }
		public string? CON_Specialization { get; set; }
		public int? CON_Ref_AS_Id { get; set; }
		public string? CON_Ref_AS_Name { get; set; }
		public List<GetAllComplaint> complaintslist { get; set; }

		public List<GetAllSymptoms> symptomslist { get; set; }

		public List<GetAllDiseasesDtl> diseaseslist { get; set; }

		public string? CON_Height { get; set; }
		public string? CON_Weight { get; set; }
		public string? CON_TempInFahrenheit { get; set; }
		public string? CON_TempInCelsius { get; set; }
		public string? CON_BloodPressure { get; set; }
		public string? CON_Sugar { get; set; }
		public string? CON_RespiratoryRate { get; set; }
		public string? CON_PulseRate { get; set; }
		public string? CON_ECG { get; set; }
		public string? CON_OxygenSaturation { get; set; }
		public DateTime? CON_ConsultedDate { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? Inactive { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
	}

	public class ConsultationBy_Id
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		public int CON_APPT_Id_FK { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public Nullable<System.DateTime> CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public int? CON_DO_Id_FK { get; set; }
		public string? CON_DO_Name { get; set; }
		public int? CON_HO_Id_FK { get; set; }
		public string? CON_HospitalName { get; set; }
		public int? CON_CD_Id_FK { get; set; }
		public string? CON_ClinicalDiscipline { get; set; }
		public int? CON_SP_Id_FK { get; set; }
		public string? CON_Specialization { get; set; }
		public int? CON_Ref_AS_Id { get; set; }
		public string? CON_Ref_AS_Name { get; set; }
		public List<GetAllComplaint> complaintslist { get; set; }

		public List<GetAllSymptoms> symptomslist { get; set; }

		public List<GetAllDiseasesDtl> diseaseslist { get; set; }

		public string? CON_Height { get; set; }
		public string? CON_Weight { get; set; }
		public string? CON_TempInFahrenheit { get; set; }
		public string? CON_TempInCelsius { get; set; }
		public string? CON_BloodPressure { get; set; }
		public string? CON_Sugar { get; set; }
		public string? CON_RespiratoryRate { get; set; }
		public string? CON_PulseRate { get; set; }
		public string? CON_ECG { get; set; }
		public string? CON_OxygenSaturation { get; set; }
		public DateTime? CON_ConsultedDate { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? Inactive { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
	}
}
