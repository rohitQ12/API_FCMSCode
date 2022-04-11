using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class Menus
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int M_Id { get; set; }
        [StringLength(100)]
        public string? M_label { get; set; }
        [StringLength(50)]
        public string? M_icon { get; set; }
        [StringLength(50)]
        public string? M_Title { get; set; }
        [StringLength(100)]
        public string? M_Redirect_URL { get; set; }
        public int M_DisplayOrder { get; set; }
        public int? Created_by { get; set; }
        public Nullable<System.DateTime> Created_date { get; set; }
        public int? Modified_by { get; set; }
        public Nullable<System.DateTime> Modified_date { get; set; }
        public int? Deleted_by { get; set; }
        public Nullable<System.DateTime> Deleted_date { get; set; }
        public bool Delete_flag { get; set; }
        public int Status { get; set; }
    }
    public class Menus_List
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int M_Id { get; set; }
        [StringLength(100)]
        public string? M_label { get; set; }
        [StringLength(50)]
        public string? M_icon { get; set; }
        [StringLength(50)]
        public string? M_Title { get; set; }
        [StringLength(100)]
        public string? link { get; set; }
        public int M_DisplayOrder { get; set; }
        public List<SubMenu_List> subItems { get; set; }
        public bool? ClaimValue { get; set; }
        public int? Created_by { get; set; }
        public Nullable<System.DateTime> Created_date { get; set; }
        public int? Modified_by { get; set; }
        public Nullable<System.DateTime> Modified_date { get; set; }
        public int? Deleted_by { get; set; }
        public Nullable<System.DateTime> Deleted_date { get; set; }
        public bool Delete_flag { get; set; }
        public int Status { get; set; }
    }
}
