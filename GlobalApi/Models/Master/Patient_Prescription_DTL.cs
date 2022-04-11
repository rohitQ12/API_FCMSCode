using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Patient_Prescription_DTL
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Dtl_Id { get; set; }

		[Display(Name = "PatientRxDetails")]
		public virtual int Rx_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Rx_Id_FK")]
		public virtual PatientRxDetails? PatientRxDetails { get; set; }


		[Display(Name = "DrugMaster")]
		public virtual int DrugMst_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("DrugMst_Id_FK")]
		public virtual DrugMaster? DrugMaster { get; set; }


        //[StringLength(300)]
        //public string? Rx_Desc { get; set; }

        [StringLength(100)]
        public string? Rx_Dosage { get; set; }

        [StringLength(100)]
        public string? Rx_Course { get; set; }

        [StringLength(100)]
		public string? Remarks { get; set; }

		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

	}
	public class GetAllPPD
	{
		public int Dtl_Id { get; set; }
		public int Rx_Id_FK { get; set; }
		public DateTime? PrescriptionDate { get; set; }
		public int DrugMst_Id_FK { get; set; }
		public string DrugMst_Name { get; set; }
		//public int DrugMst_DT_Id_FK { get; set; }
		public string DM_DT_Type { get; set; }
		public string DrugMst_Strength {get; set;}
		//public int DrugMst_UT_Id_FK { get; set; }
		public string DM_UT_Unit { get; set; }
		public string Desc { get; set; }

        //public string? Rx_Desc { get; set; }
        public string? Rx_Dosage { get; set; }
        public string? Rx_Course { get; set; }
        public string? Remarks { get; set; }
		public bool delete_flag { get; set; }


	}
	public class PPD_By_Id
	{
		public int Dtl_Id { get; set; }
		public int Rx_Id_FK { get; set; }
		public DateTime? PrescriptionDate { get; set; }
		public int DrugMst_Id_FK { get; set; }
		public string DrugMst_Name { get; set; }
		//public int DrugMst_DT_Id_FK { get; set; }
		public string DM_DT_Type { get; set; }
		public string DrugMst_Strength { get; set; }
		//public int DrugMst_UT_Id_FK { get; set; }
		public string DM_UT_Unit { get; set; }
		public string Desc { get; set; }
		//public string? Rx_Desc { get; set; }
		public string? Rx_Dosage { get; set; }
        public string? Rx_Course { get; set; }
        public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
}
