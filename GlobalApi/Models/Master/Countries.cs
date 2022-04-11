using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
	public class Countries
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int cntry_id { get; set; }

		[Required]
		public string? country_code { get; set; }

		[Required]
		public string? country_name { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
	}
	public class Country_DD
	{
		public int cntry_id { get; set; }
		public string? country_name { get; set; }
	}

	public class CountryById
	{
		public int cntry_id { get; set; }
		public string? country_name { get; set; }
		public string? country_code { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }

	}
}