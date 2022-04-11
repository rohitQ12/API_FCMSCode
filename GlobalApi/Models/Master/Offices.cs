using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Offices
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(150)]
        public string? OfficeName { get; set; }
        public int Off_Level { get; set; }
        [StringLength(500)]
        public string? Off_Address1 { get; set; }
        public int Off_District_Id_Fk { get; set; }
        public int Off_pincode { get; set; }
        [StringLength(500)]
        public string? Off_Address2 { get; set; }
        [StringLength(20)]
        public string? Off_Email { get; set; }
        public long Off_PhoneNumber { get; set; }
        [StringLength(20)]
        public string? Off_Landline { get;set; }
        [StringLength(3)]
        public char Inactive { get; set; }
        public int Off_UserId { get; set; }
        public DateTime Off_TS { get; set; }
        public int Off_LastEdited_UserId { get; set; }
        public DateTime Off_LastEdited_TS { get; set; }
        [StringLength(50)]
        public string? Off_OfficerName { get; set; }
        [StringLength(50)]
        public string? Off_Designation { get; set; }
        public int Created_by { get; set; }
        public Nullable<System.DateTime> Created_date { get; set; }
        public int Modified_by { get; set; }
        public Nullable<System.DateTime> Modified_date { get; set; }
        public int Deleted_by { get; set; }
        public Nullable<System.DateTime> Deleted_date { get; set; }
        public bool Delete_flag { get; set; }
        public int Status { get; set; }
    }
}
