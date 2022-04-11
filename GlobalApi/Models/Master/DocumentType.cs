using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class DocumentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int doctype_id { get; set; }

        [Required]
        public string doctype_name { get; set; }

        [Required]
        public string doc_description { get; set; }
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
    public class DocumentType_DD
    {
        public int doctype_id { get; set; }
        public string doctype_name { get; set; }
    }
    public class DocumentTypeById
    {
        public int doctype_id { get; set; }
        public string doctype_name { get; set; }
        public string doc_description { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
    }
}