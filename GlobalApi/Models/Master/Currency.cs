using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int currency_id { get; set; }
        public string? currency_code { get; set; }
        public string? currency_name { get; set; }


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

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }

    }
    public class GetCountryCurrency
    {
        public int currency_id { get; set; }
        public string? currency_code { get; set; }
        public string? currency_name { get; set; }
        public int cntry_id { get; set; }
        public string? country_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Currency_DD
    {
        public int currency_id { get; set; }
        public string? currency_name { get; set; }

    }
    public class CurrencyById
    {
        public int currency_id { get; set; }
        public string? currency_code { get; set; }
        public string? currency_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
}