using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Diagnostic_Test
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DT_Id { get; set; }

        [StringLength(10)]
        public string? DT_Code { get; set; }
        public int? DT_Type { get; set; }
        public int? DT_Category { get; set; }
        public string? DT_Desc { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetAllDiagno_Test
    {
        public int DT_Id { get; set; }
        public string? DT_Code { get; set; }
        public int? DT_Type { get; set; }
        public string? Type_Name { get; set; }
        public int? DT_Category { get; set; }
        public string? Cat_Name { get; set; }
        public string? DT_Desc { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }

    }
    public class GetDiagno_TestById
    {
        public int DT_Id { get; set; }
        public string? DT_Code { get; set; }
        public int? DT_Type { get; set; }
        public string? Type_Name { get; set; }
        public int? DT_Category { get; set; }
        public string? Cat_Name { get; set; }
        public string? DT_Desc { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }

    }
    public class Diagno_TestDD
    {
        public int DT_Id { get; set; }
        public string? DT_Code { get; set; }
        public string? DT_Desc { get; set; }

    }
    public class ApproveDiagno_Test
    {
        public int DT_Id { get; set; }
        public int status { get; set; }

    }
}
