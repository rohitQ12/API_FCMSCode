using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GlobalApi.GlobalClasses;

namespace GlobalApi.Models.Master
{
    //[ValidateModel]
    public class Network
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int NE_Id { get; set; }

        [StringLength(10)]
        //[ModuleValidation]
        public string? NE_Code { get; set; }

        [StringLength(50)]
        //[ModuleValidation]
        public string? NE_Description { get; set; }
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
        
        [StringLength(250)]
        public string? Remarks { get; set; }

    }
    public class Network_DD
    {
        public int NE_Id { get; set; }
        public string? NE_Code { get; set; }
        public string? NE_Description { get; set; }
    }
    public class NetworkById
    {
        public int NE_Id { get; set; }
        public string? NE_Code { get; set; }
        public string? NE_Description { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }

    }
    public class GetAllNetwork
    {
        public int NE_Id { get; set; }
        public string? NE_Code { get; set; }
        public string? NE_Description { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public string? sts_name { get; set; }
        public string? Remarks { get; set; }

    }
    public class ApproveNetwork
    {
        public int NE_Id { get; set; }
        public string? Remarks { get; set; }
    }

}
