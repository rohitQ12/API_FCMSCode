using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DoctorLanguage
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }


		[Display(Name="Doctor")]
		public virtual int doc_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("doc_Id_FK")]
		public virtual Doctor? Doctor { get; set; }


		[Display(Name ="Language")]
		public virtual int Lang_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("Lang_Id_FK")]
		public virtual Language? Language { get; set; }

		public int created_by { get; set; }
		public DateTime created_date { get; set; }
		public int? modified_by { get; set; }
		public DateTime? modified_date { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class GetDoctorlang
    {
		public int Id { get; set; }
		public int doc_Id_FK { get; set; }
		public string doc_name { get; set; }
		public int Lang_Id_FK { get; set; }
		public string lang { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
	public class Language_DD
	{
		public int Id { get; set; }
		public string Languages { get; set; }

	}
}
