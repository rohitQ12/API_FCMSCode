using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Gram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Gram_id { get; set; }

        [StringLength(10)]
        public string? Gram_code { get; set; }

        [StringLength(50)]
        public string Gram_name { get; set; }

        [Display(Name = "Countries")]
        public virtual int cntry_id { get; set; }
        [JsonIgnore]
        [ForeignKey("cntry_id")]
        public virtual Countries? Countries { get; set; }


        [Display(Name = "States")]
        public virtual int state_id { get; set; }
        [JsonIgnore]
        [ForeignKey("state_id")]
        public virtual States? States { get; set; }


        [Display(Name = "Districts")]
        public virtual int dist_id { get; set; }
        [JsonIgnore]
        [ForeignKey("dist_id")]
        public virtual Districts? Districts { get; set; }


        [Display(Name = "Taluk")]
        public virtual int Taluk_id { get; set; }
        [JsonIgnore]
        [ForeignKey("Taluk_id")]
        public virtual Taluk? Taluk { get; set; }

        public int? Postal_Code { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        
        [StringLength(250)]
        public string? Remarks { get; set; }

    }
    public class Gram_DD
    {
        public int Gram_id { get; set; }
        public string? Gram_code { get; set; }
        public string Gram_name { get; set; }
        public int? Postal_Code { get; set; }

    }
    public class GetGramTaluk
    {
        public int Gram_id { get; set; }
        public string? Gram_code { get; set; }
        public string Gram_name { get; set; }
        public int cntry_id { get; set; }
        public string? cntry_name { get; set; }
        public int state_id { get; set; }
        public string? state_name { get; set; }
        public int dist_id { get; set; }
        public string? dist_name { get; set; }
        public int Taluk_id { get; set; }
        public string Taluk_name { get; set; }
        public int? Postal_Code { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }

    }
    public class ApproveGram
    {
        public int Gram_id { get; set; }
        public string? Remarks { get; set; }
    }

}
