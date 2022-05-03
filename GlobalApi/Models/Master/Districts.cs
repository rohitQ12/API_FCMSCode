using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Districts
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int district_id { get; set; }

		//[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
		[Required]
		public string district_name { get; set; }

		[Required]
		public string district_code { get; set; }

		[Display(Name = "States")]
		public virtual int stat_id { get; set; }
		[JsonIgnore]
		[ForeignKey("stat_id")]
		public virtual States? States { get; set; }
		public int created_by { get; set; }
		public DateTime? created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

		[Required]
		public int status { get; set; }
		//public virtual int stat_id { get; set; }

	}
	public class District_DD
	{
		public int district_id { get; set; }
		public string district_name { get; set; }
	}

	public class DistrictById
	{
		public int district_id { get; set; }
		public string district_name { get; set; }
		public string district_code { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

		//public string currency { get; set; }
	}
	public class GetStateDistrict
	{
		public int district_id { get; set; }
		public string district_name { get; set; }
		public string district_code { get; set; }
		public int stat_id { get; set; }
		public string? state_name { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }


	}
}