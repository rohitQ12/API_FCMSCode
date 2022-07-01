using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Referrals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]

        public int Ref_Id { get; set; }
        public int? CON_Id { get; set; }
        public int? DO_Id { get; set; }
        
        [StringLength(50)]
        public string? Ref_Date { get; set; }

        [StringLength(50)]
        public string? SplObs { get; set; }
        public string? Remarks { get; set; }
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
    public class GetReferrals
    {
        public int Ref_Id { get; set; }
        public int? CON_Id { get; set; }
        public int? DO_Id { get; set; }
        public string? DO_Name { get; set; }
        public string? Ref_Date { get; set; }
        public string? SplObs { get; set; }
        public string? Remarks { get; set; }
        public int Created_by { get; set; }
        public DateTime Created_date { get; set; }
        public int? Modified_by { get; set; }
        public DateTime? Modified_date { get; set; }
        public int? Deleted_by { get; set; }
        public DateTime? Deleted_date { get; set; }
        public bool Delete_flag { get; set; }
        public int Status { get; set; }
        public string? sts_name { get; set; }
    }

    public class ApprvReferrals
    {
        public int Ref_Id { get; set; }
        public int Status { get; set; }
        public string? Select_FrmTime { get; set; }
        public string? Select_toTime { get; set; }

    }

}
