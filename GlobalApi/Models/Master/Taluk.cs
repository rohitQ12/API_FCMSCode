using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Taluk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Taluk_id { get; set; }
        public int Taluk_code { get; set; }

        [StringLength(100)]
        public string Taluk_name { get; set; }

        [Display(Name= "Districts")]
        public virtual int district_id { get; set; }
        [JsonIgnore]
        [ForeignKey("district_id")]
        public virtual Districts? Districts { get; set; }

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
    public class Taluk_DD
    {
        public int Taluk_id { get; set; }
        public int Taluk_code { get; set; }
        public string Taluk_name { get; set; }

    }
    public class GetTalukDistricts
    {
        public int Taluk_id { get; set; }
        public int Taluk_code { get; set; }
        public string Taluk_name { get; set; }
        public int district_id { get; set; }
        public string? district_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

}
