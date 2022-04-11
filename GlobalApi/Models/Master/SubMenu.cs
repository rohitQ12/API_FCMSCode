using GlobalApi.Models.AdminClaims;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class SubMenu
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SM_Id { get; set; }
        [StringLength(100)]
        public string? SM_label { get; set; }
        [StringLength(50)]
        public string? M_MenuName { get; set; }
        [StringLength(50)]
        public string? SM_icon { get; set; }
        [StringLength(50)]
        public string? SM_link { get; set; }
        [StringLength(50)]
        public string? SM_Title { get; set; }
        [StringLength(100)]
        public string? SM_Redirect_URL { get; set; }
        public int SM_DisplayOrder { get; set; }
        public int SM_M_Id_FK { get; set; }
        //[JsonIgnore]
        //[ForeignKey("SM_M_Id_FK")]
        //public virtual Menus? Menu { get; set; }
        public int? Created_by { get; set; }
        public Nullable<System.DateTime> Created_date { get; set; }
        public int? Modified_by { get; set; }
        public Nullable<System.DateTime> Modified_date { get; set; }
        public int? Deleted_by { get; set; }
        public Nullable<System.DateTime> Deleted_date { get; set; }
        public bool Delete_flag { get; set; }
        public int Status { get; set; }
    }
    public class SubMenu_List
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SM_Id { get; set; }
        [StringLength(100)]
        public string? SM_label { get; set; }
        [StringLength(50)]
        public string? SM_MenuName { get; set; }
        [StringLength(50)]
        public string? SM_icon { get; set; }
        [StringLength(50)]
        public string? SM_link { get; set; }
        [StringLength(50)]
        public string? SM_Title { get; set; }
        [StringLength(100)]
        public string? SM_Redirect_URL { get; set; }
        public bool? ClaimValue { get; set; }
        public int SM_DisplayOrder { get; set; }
        public int SM_M_Id_FK { get; set; }
        public List<SubMenuFunctions_List> subItemsList { get; set; }
        public List<ClaimsModels> SubMenuClaim { get; set; }
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
