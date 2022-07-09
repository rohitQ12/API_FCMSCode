using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class LAB_Description
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lab_DescId { get; set; }
        public int? Lab_Invt_Id { get; set; }
        public int? Lab_SubInvt_Id { get; set; }
        public string? Lab_Description { get; set; }

        [StringLength(50)]
        public string? Remarks { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetAllLAB_Desc
    {
        public int Lab_DescId { get; set; }
        public int? Lab_Invt_Id { get; set; }
        public string? Category { get; set; }
        public int? Lab_SubInvt_Id { get; set; }
        public string? Sub_Category { get; set; }
        public string? Lab_Description { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? Remarks { get; set; }

    }
    public class GetLabDesc_ById
    {
        public int Lab_DescId { get; set; }
        public int? Lab_Invt_Id { get; set; }
        public string? Category { get; set; }
        public int? Lab_SubInvt_Id { get; set; }
        public string? Sub_Category { get; set; }
        public string? Lab_Description { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? Remarks { get; set; }

    }
    public class LabDesc_DD
    {
        public int Lab_DescId { get; set; }
        //public int? Lab_Invt_Id { get; set; }
        public int? Lab_SubInvt_Id { get; set; }
        public string? Lab_Description { get; set; }

    }
    public class ApproveLab_Desc
    {
        public int Lab_DescId { get; set; }
        public string? Remarks { get; set; }
        public int status { get; set; }

    }

}
