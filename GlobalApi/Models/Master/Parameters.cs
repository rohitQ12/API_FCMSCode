using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Parameters
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int PA_Id { get; set; }

		[StringLength(50)]
		public string? PA_Code { get; set; }


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


		[StringLength(255)]
		public string? PA_Height { get; set; }

		[StringLength(255)]
		public string? PA_Weight { get; set; }

		[StringLength(255)]
		public string? PA_TempInFahrenheit { get; set; }

		[StringLength(255)]
		public string? PA_TempInCelsius { get; set; }


		[StringLength(255)]
		public string? PA_BloodPressure { get; set; }

		[StringLength(255)]
		public string? PA_Sugar { get; set; }

		[StringLength(255)]
		public string? PA_PulseRate { get; set; }

		[StringLength(255)]
		public string? PA_RespiratoryRate { get; set; }

		[StringLength(255)]
		public string? PA_ECG { get; set; }

		[StringLength(255)]
		public string? PA_OxygenSaturation { get; set; }

		public int? PA_UserId_FK { get; set; }

		[StringLength(20)]
		public string? PA_Hemoglobin { get; set; }
		public int? created_by { get; set; }
		public DateTime? created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

		[Required]
		public int status { get; set; }

	}

	public class GetAllParameters
	{
		public int PA_Id { get; set; }
		public string? PA_Code { get; set; }
		public int? Appt_Id { get; set; }
		public int? MAppt_Id { get; set; }
		public string? PA_Height { get; set; }
		public string? PA_Weight { get; set; }
		public string? PA_TempInFahrenheit { get; set; }
		public string? PA_TempInCelsius { get; set; }
		public string? PA_BloodPressure { get; set; }
		public string? PA_Sugar { get; set; }
		public string? PA_PulseRate { get; set; }
		public string? PA_RespiratoryRate { get; set; }
		public string? PA_ECG { get; set; }
		public string? PA_OxygenSaturation { get; set; }
		public string? PA_Hemoglobin { get; set; }
		public int? PA_UserId_FK { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	//public class Parameters_DD
	//   {
	//	public int PA_Id { get; set; }
	//	public string? PA_Code { get; set; }
	//	public string? PA_Height { get; set; }
	//	public string? PA_Weight { get; set; }
	//	public string? PA_Temperature { get; set; }
	//	public string? PA_BloodPressure { get; set; }
	//	public string? PA_Sugar { get; set; }
	//	public string? PA_PulseRate { get; set; }
	//	public string? PA_RespiratoryRate { get; set; }
	//	public string? PA_ECG { get; set; }
	//	public string? PA_OxygenSaturation { get; set; }


	//}
	public class ParametersBy_Id
	{
		public int PA_Id { get; set; }
		public string? PA_Code { get; set; }
		public int? Appt_Id { get; set; }
		public int? MAppt_Id { get; set; }
		public string? PA_Height { get; set; }
		public string? PA_Weight { get; set; }
		public string? PA_TempInFahrenheit { get; set; }
		public string? PA_TempInCelsius { get; set; }
		public string? PA_BloodPressure { get; set; }
		public string? PA_Sugar { get; set; }
		public string? PA_PulseRate { get; set; }
		public string? PA_RespiratoryRate { get; set; }
		public string? PA_ECG { get; set; }
		public string? PA_OxygenSaturation { get; set; }
		public string? PA_Hemoglobin { get; set; }
		public int? PA_UserId_FK { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
}
