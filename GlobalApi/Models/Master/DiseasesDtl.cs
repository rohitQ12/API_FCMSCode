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
		public virtual int? Id { get; set; }
		[JsonIgnore]
		[ForeignKey("Id")]
		public virtual Diseases? Diseases { get; set; }


		public int? Appt_Id { get; set; }
        public virtual int? Phc_Appt_Id { get; set; }


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
		//public int Ddtl_Id { get; set; }
		public int? Id { get; set; }
		public string? Diseases_Code { get; set; }
		public string? Acronyms { get; set; }
		public string? Diseases_Name { get; set; }
		//public int? Appt_Id { get; set; }
		//public int? Phc_Appt_Id { get; set; }
		//public string? Remarks { get; set; }
		//public bool delete_flag { get; set; }

	}
	public class GetDiseaseDtlById
    {
		public int Ddtl_Id { get; set; }
		public int? Id { get; set; }
		public int? Appt_Id { get; set; }
		public int? Phc_Appt_Id { get; set; }
		public string? Diseases_Name { get; set; }
		public string? Remarks { get; set; }
		public bool delete_flag { get; set; }

	}
}
