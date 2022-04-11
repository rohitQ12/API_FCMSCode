using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DoctorLocation
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }


		[Display(Name = "Doctor")]
		public virtual int doc_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("doc_Id_FK")]
		public virtual Doctor? Doctor { get; set; }

		[StringLength(20)]
		public string Latitude { get; set; }

		[StringLength(20)]
		public string Longitude { get; set; }
		public int created_by { get; set; }
		public DateTime created_date { get; set; }
		public int? modified_by { get; set; }
		public DateTime? modified_date { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class GetDoctorLoc
	{
		public int Id { get; set; }
		public int doc_Id_FK { get; set; }
		public string doc_name { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}

}
