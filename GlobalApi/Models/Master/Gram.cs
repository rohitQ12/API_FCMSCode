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
        public int Gram_code { get; set; }

        [StringLength(100)]
        public string Gram_name { get; set; }

        [Display(Name = "Taluk")]
        public virtual int Taluk_id { get; set; }
        [JsonIgnore]
        [ForeignKey("Taluk_id")]
        public virtual Taluk? Taluk { get; set; }

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Gram_DD
    {
        public int Gram_id { get; set; }
        public int Gram_code { get; set; }
        public string Gram_name { get; set; }

    }
    public class GetGramTaluk
    {
        public int Gram_id { get; set; }
        public int Gram_code { get; set; }
        public string Gram_name { get; set; }
        public int Taluk_id { get; set; }
        public string Taluk_name { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

}
