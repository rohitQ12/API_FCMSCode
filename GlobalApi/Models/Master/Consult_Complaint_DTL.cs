using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Consult_Complaint_DTL
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


		//[Display(Name = "AppointmentModel")]
		public int CON_Id { get; set; }
		//[JsonIgnore]
		//[ForeignKey("Appt_Id")]
		//public virtual AppointmentModel? PatientAppointment { get; set; }


		//[Display(Name = "ManualAppointment")]
		//public virtual int? MAppt_Id { get; set; }
		//[JsonIgnore]
		//[ForeignKey("MAppt_Id")]
		//public virtual ManualAppointment? ManualAppointment { get; set; }
		public int? created_by { get; set; }
		public DateTime created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }


	}
	public class GetAllCCdtl
	{
		public int CPT_Id { get; set; }
		public int? Cmst_Id { get; set; }
		public string Cmst_Name { get; set; }
		public int? CON_Id { get; set; }
		public bool delete_flag { get; set; }

	}
	public class GetAllCons_Complaints
	{
		public int? Cmst_Id { get; set; }
		public string Cmst_Code { get; set; }
		public string Cmst_Name { get; set; }

	}

	public class CCdtlBy_Id
	{
		public int CPT_Id { get; set; }
		public int? Cmst_Id { get; set; }
		public string Cmst_Name { get; set; }
		public int? CON_Id { get; set; }
		public bool delete_flag { get; set; }

	}
}
