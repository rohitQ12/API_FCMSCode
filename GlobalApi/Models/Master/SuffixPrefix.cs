using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class SuffixPrefix
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SuffixprefixId { get; set; }


        [Display(Name = "DocPkValue")]
        public virtual int? DocPkTblId { get; set; }
        [JsonIgnore]
        [ForeignKey("DocPkTblId")]
        public virtual DocPkValue? DocPkValue { get; set; }

        public decimal StartIndex { get; set; }

        [StringLength(50)]
        public string? Prefix { get; set; }

        [StringLength(50)]
        public string? Suffix { get; set; }
        public int WidthOfNumericalPart { get; set; }
        public bool PrefillWithZero { get; set; }
        public int created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
    public class viewdetail_suffixprefix
    {
        public int SuffixprefixId { get; set; }
        public int? DocPkTblId { get; set; }
        public decimal StartIndex { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public int WidthOfNumericalPart { get; set; }
        public bool PrefillWithZero { get; set; }

    }
}
