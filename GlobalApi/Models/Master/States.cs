using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class States
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int stat_id { get; set; }

		[StringLength(10)]
		public string? state_code { get; set; }
		public string? state_name { get; set; }

		[Display(Name = "Countries")]
		public virtual int cntry_id { get; set; }
		[JsonIgnore]
		[ForeignKey("cntry_id")]
		public virtual Countries? Countries { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		
		[StringLength(250)]
		public string? Remarks { get; set; }

		//public virtual int cntry_id { get; set; }
	}
	public class State_DD
	{
		public int stat_id { get; set; }
		public string? state_code { get; set; }
		public string? state_name { get; set; }
	}

	public class StateById
	{
		public int stat_id { get; set; }
		public int cntry_id { get; set; }
		public string? country_name { get; set; }
		public string? state_code { get; set; }
		public string? state_name { get; set; }
		//public string currency { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class GetStateCountry
	{
		public int stat_id { get; set; }
		public string? state_name { get; set; }
		public int cntry_id { get; set; }
		public string? country_name { get; set; }
		public string? state_code { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}
	public class ApproveState
    {
		public int stat_id { get; set; }
		public string? Remarks { get; set; }

	}

	public class NoStateFound
	{
		public int? stat_id { get; set; }
		public string? state_code { get; set; }
		public string? state_name { get; set; }
	}
	public class NameDomainValidator : ValidationAttribute
	{
		public string AllowedDomain { get; set; }

		protected override ValidationResult IsValid(object value,
			ValidationContext validationContext)
		{
			string strings = value.ToString();
			if (strings.ToUpper() == AllowedDomain.ToUpper())
			{
				return null;
			}

			return new ValidationResult($"Domain must be {AllowedDomain}" + $"Not like {value}",
			new[] { validationContext.MemberName });
		}
	}
}