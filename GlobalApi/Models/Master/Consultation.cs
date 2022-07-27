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

		public int? CON_APPT_Id_FK { get; set; }
		
		public int? Phc_ApptId { get; set; }


		[Display(Name = "Patient")]
		public virtual int? CON_PR_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("CON_PR_Id_FK")]
		public virtual Patient? Patient { get; set; }


		//[Display(Name = "Doctor")]
		public int? CON_DO_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("CON_DO_Id_FK")]
		//public virtual Doctor? Doctor { get; set; }


		//[Display(Name = "Hospital")]
		public int? CON_HO_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("CON_HO_Id_FK")]
		//public virtual Hospital? Hospital { get; set; }


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


		//[Display(Name = "Diseases")]
		//public virtual int? Dis_Id_FK { get; set; }
		//[JsonIgnore]
		//[ForeignKey("Dis_Id_FK")]
		//public virtual Diseases? Diseases { get; set; }
		public int? CON_Ref_AS_Id { get; set; }

		[StringLength(50)]
		public string? CON_ConsultedDate { get; set; }

		[StringLength(50)]
		public string? CON_ConsultedTime { get; set; }

		public int CON_UserId_FK { get; set; }
		
		[StringLength(3)]
		public string? UnderBPMedication { get; set; }

		[StringLength(3)]
		public string? UnderSugarMedication { get; set; }

		[StringLength(10)]
		public string? Appt_Category { get; set; }
		
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

		[StringLength(250)]
		public string? Remarks { get; set; }
	}

	public class GetAllConsultation
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		public int? CON_APPT_Id_FK { get; set; }
		public int? Phc_ApptId { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? PR_Code { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public Nullable<System.DateTime> CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public string? PR_MobileNumber { get; set; }
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
		public List<GetAllCons_Complaints>? complaintslist { get; set; }

		public List<GetAllCons_Symptoms>? symptomslist { get; set; }

		public List<GetAllCons_Diseases>? diseaseslist { get; set; }
		public List<GetAllCons_Allergys>? Allergylist { get; set; }
		public int? CON_PA_Id { get; set; }
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
		public string? CON_Hemoglobin { get; set; }
		public string? CON_ConsultedDate { get; set; }
		public string? CON_ConsultedTime { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? UnderBPMedication { get; set; }
		public string? UnderSugarMedication { get; set; }
		public string? Appt_Category { get; set; }
		public string? Inactive { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}

	public class ConsultationBy_Id
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		public int? CON_APPT_Id_FK { get; set; }
		public string? Appt_Date { get; set; }
		public string? Appt_FrmTime { get; set; }
		public string? Appt_ToTime { get; set; }
		//public int? Phc_ApptId { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? PR_Code { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public Nullable<System.DateTime> CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public string? PR_MobileNumber { get; set; }
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
		public List<GetAllCons_Complaints>? complaintslist { get; set; }

		public List<GetAllCons_Symptoms>? symptomslist { get; set; }

		public List<GetAllCons_Diseases>? diseaseslist { get; set; }
		public List<GetAllCons_Allergys>? Allergylist { get; set; }
		public int? CON_PA_Id { get; set; }
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
		public string? CON_Hemoglobin { get; set; }
		public string? CON_ConsultedDate { get; set; }
		public string? CON_ConsultedTime { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? UnderBPMedication { get; set; }
		public string? UnderSugarMedication { get; set; }
		public string? Appt_Category { get; set; }
		public string? Inactive { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}

	public class GetAllPhcConsultation
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		public int? CON_APPT_Id_FK { get; set; }
		public int? Phc_ApptId { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? PR_Code { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public DateTime? CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public string? PR_MobileNumber { get; set; }
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
		public List<GetAllCons_Complaints>? complaintslist { get; set; }

		public List<GetAllCons_Symptoms>? symptomslist { get; set; }

		public List<GetAllCons_Diseases>? diseaseslist { get; set; }
		public List<GetAllCons_Allergys>? Allergylist { get; set; }
		public int? CON_PA_Id { get; set; }
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
		public string? CON_Hemoglobin { get; set; }
		public string? CON_ConsultedDate { get; set; }
		public string? CON_ConsultedTime { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? UnderBPMedication { get; set; }
		public string? UnderSugarMedication { get; set; }
		public string? Appt_Category { get; set; }
		public string? Inactive { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class PhcConsultationBy_Id
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		//public int? CON_APPT_Id_FK { get; set; }
		public int? Phc_ApptId { get; set; }
		public string? Appt_Date { get; set; }
		public string? Appt_FrmTime { get; set; }
		public string? Appt_ToTime { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? PR_Code { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public Nullable<System.DateTime> CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public string? PR_MobileNumber { get; set; }
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
		public List<GetAllCons_Complaints>? complaintslist { get; set; }

		public List<GetAllCons_Symptoms>? symptomslist { get; set; }

		public List<GetAllCons_Diseases>? diseaseslist { get; set; }
		public List<GetAllCons_Allergys>? Allergylist { get; set; }
		public int? CON_PA_Id { get; set; }
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
		public string? CON_Hemoglobin { get; set; }
		public string? CON_ConsultedDate { get; set; }
		public string? CON_ConsultedTime { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? Inactive { get; set; }
		public string? UnderBPMedication { get; set; }
		public string? UnderSugarMedication { get; set; }
		public string? Appt_Category { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class ConsultationBy_ApptId
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		public int? CON_APPT_Id_FK { get; set; }
		//public int? Phc_ApptId { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? PR_Code { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public Nullable<System.DateTime> CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public string? PR_MobileNumber { get; set; }
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
		public List<GetAllCons_Complaints>? complaintslist { get; set; }

		public List<GetAllCons_Symptoms>? symptomslist { get; set; }

		public List<GetAllCons_Diseases>? diseaseslist { get; set; }
		public List<GetAllCons_Allergys>? Allergylist { get; set; }
		public int? CON_PA_Id { get; set; }
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
		public string? CON_Hemoglobin { get; set; }
		public string? CON_ConsultedDate { get; set; }
		public string? CON_ConsultedTime { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? UnderBPMedication { get; set; }
		public string? UnderSugarMedication { get; set; }
		public string? Appt_Category { get; set; }
		public string? Inactive { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class PhcConsultationBy_MAppt_Id
	{
		public int CON_Id { get; set; }
		public string? CON_Code { get; set; }
		public string? CON_Type { get; set; }
		public int? CON_APPT_Id_FK { get; set; }
		//public int? Phc_ApptId { get; set; }
		public int? CON_PR_Id_FK { get; set; }
		public string? PR_Code { get; set; }
		public string? CON_PR_Name { get; set; }
		public string? CON_PR_Gender { get; set; }
		public Nullable<System.DateTime> CON_PR_DOB { get; set; }
		public string? CON_PR_Age { get; set; }
		public string? CON_PR_BloodGroup { get; set; }
		public string? CON_PR_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public string? PR_MobileNumber { get; set; }
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
		public List<GetAllCons_Complaints>? complaintslist { get; set; }

		public List<GetAllCons_Symptoms>? symptomslist { get; set; }

		public List<GetAllCons_Diseases>? diseaseslist { get; set; }
		public List<GetAllCons_Allergys>? Allergylist { get; set; }
		public int? CON_PA_Id { get; set; }
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
		public string? CON_Hemoglobin { get; set; }
		public string? CON_ConsultedDate { get; set; }
		public string? CON_ConsultedTime { get; set; }
		public int? CON_UserId_FK { get; set; }
		public string? UnderBPMedication { get; set; }
		public string? UnderSugarMedication { get; set; }
		public string? Appt_Category { get; set; }
		public string? Inactive { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class Other_Info
    {
		public int CON_Id { get; set; }
		public List<Consult_Complaint_DTL> Consult_Complaint_DTL { get; set; } = null!;
		public List<Consult_Symptoms_DTL> Consult_Symptoms_DTL { get; set; } = null!;
		public List<Consult_Diseases_DTL> Consult_Diseases_DTL { get; set; } = null!;
		public List<Consult_AllergySigns_DTL> Consult_AllergySigns_DTL { get; set; } = null!;
		public string? UnderBPMedication { get; set; }
		public string? UnderSugarMedication { get; set; }

	}



}
