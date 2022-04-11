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
		[Required]
		public string PA_Code { get; set; }


		[Display(Name = "AppointmentModel")]
		public virtual int PA_APPT_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("PA_APPT_Id_FK")]
		public virtual AppointmentModel? PatientAppointment { get; set; }

		[Required]
		[StringLength(255)]
		public string PA_Height { get; set; }

		[Required]
		[StringLength(255)]
		public string PA_Weight { get; set; }

		[Required]
		[StringLength(255)]
		public string? PA_TempInFahrenheit { get; set; }

		[Required]
		[StringLength(255)]
		public string? PA_TempInCelsius { get; set; }


		[Required]
		[StringLength(255)]
		public string? PA_BloodPressure { get; set; }

		[Required]
		[StringLength(255)]
		public string? PA_Sugar { get; set; }

		[Required]
		[StringLength(255)]
		public string? PA_PulseRate { get; set; }

		[Required]
		[StringLength(255)]
		public string? PA_RespiratoryRate { get; set; }

		[Required]
		[StringLength(255)]
		public string? PA_ECG { get; set; }

		[Required]
		[StringLength(255)]
		public string? PA_OxygenSaturation { get; set; }

		[Required]
		public int PA_UserId_FK { get; set; }

		public int created_by { get; set; }
		public DateTime created_date { get; set; }
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
		public string PA_Code { get; set; }
		public int PA_APPT_Id_FK { get; set; }
		public string PA_Height { get; set; }
		public string PA_Weight { get; set; }
		public string? PA_TempInFahrenheit { get; set; }
		public string? PA_TempInCelsius { get; set; }
		public string? PA_BloodPressure { get; set; }
		public string? PA_Sugar { get; set; }
		public string? PA_PulseRate { get; set; }
		public string? PA_RespiratoryRate { get; set; }
		public string? PA_ECG { get; set; }
		public string? PA_OxygenSaturation { get; set; }
		public int PA_UserId_FK { get; set; }
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
		public string PA_Code { get; set; }
		public int PA_APPT_Id_FK { get; set; }
		public string PA_Height { get; set; }
		public string PA_Weight { get; set; }
		public string? PA_TempInFahrenheit { get; set; }
		public string? PA_TempInCelsius { get; set; }
		public string? PA_BloodPressure { get; set; }
		public string? PA_Sugar { get; set; }
		public string? PA_PulseRate { get; set; }
		public string? PA_RespiratoryRate { get; set; }
		public string? PA_ECG { get; set; }
		public string? PA_OxygenSaturation { get; set; }
		public int PA_UserId_FK { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
}
