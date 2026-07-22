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
		[StringLength(50)]
		public string? district_name { get; set; }

		[StringLength(10)]
		public string? district_code { get; set; }


		[Display(Name = "Countries")]
		public virtual int cntry_id { get; set; }
		[JsonIgnore]
		[ForeignKey("cntry_id")]
		public virtual Countries? Countries { get; set; }


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
		
		[StringLength(250)]
		public string? Remarks { get; set; }

		//public virtual int stat_id { get; set; }

	}
	public class District_DD
	{
		public int district_id { get; set; }
		public string? district_code { get; set; }
		public string? district_name { get; set; }
	}

	public class DistrictById
	{
		public int district_id { get; set; }
		public string? district_name { get; set; }
		public string? district_code { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

		//public string currency { get; set; }
	}
	public class GetDistrictState
	{
		public int district_id { get; set; }
		public string? district_name { get; set; }
		public string? district_code { get; set; }
		public int cntry_id { get; set; }
		public string? cntry_name { get; set; }
		public int stat_id { get; set; }
		public string? state_name { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }


	}
	public class ApproveDistrict
    {
		public int district_id { get; set; }
		public string? Remarks { get; set; }
	}

	public class NoDistFound
	{
		public int? district_id { get; set; }
		public string? district_code { get; set; }
		public string? district_name { get; set; }
	}
}