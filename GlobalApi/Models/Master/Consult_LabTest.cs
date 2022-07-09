using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Consult_LabTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        public int CON_Id { get; set; }
        public int Category_Id { get; set; }
        public int Description_Id { get; set; }
        public int Created_by { get; set; }
        public DateTime Created_date { get; set; }
        public int? Modified_by { get; set; }
        public DateTime? Modified_date { get; set; }
        public int? Deleted_by { get; set; }
        public DateTime? Deleted_date { get; set; }

        [Required]
        public bool Delete_flag { get; set; }

        [Required]
        public int Status { get; set; }

    }
    public class GetConsult_LabTest
    {
        public int Id { get; set; }
        public int CON_Id { get; set; }
        public int Category_Id { get; set; }
        public string? Cat_Name { get; set; }
        public int Description_Id { get; set; }
        public string? Description { get; set; }
        public bool Delete_flag { get; set; }
        public int Status { get; set; }
        public string sts_name { get; set; }
    }

}
